using Helios.Messages.Outgoing;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Catalogue;
using Helios.Storage.Models.Effect;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Item;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Game
{
    public class CatalogueManager : ILoadable
    {
        #region Fields

        public static readonly CatalogueManager Instance = new CatalogueManager();

        #endregion

        #region Properties

        public List<CataloguePage> Pages;
        public List<CatalogueItem> Items;
        public List<CataloguePackage> Packages;
        public List<CatalogueDiscountData> Discounts;
        public Dictionary<int, EffectType> Effects;

        #endregion

        #region Constructors

        public void Load()
        { 
            Log.ForContext<CatalogueManager>().Information("Loading Catalogue data");
            
            using (var context = new StorageContext())
            {
                Pages = context.GetPages().Select(x => new CataloguePage(x)).ToList();
                Items = context.GetItems().Select(x => new CatalogueItem(x)).ToList();
                Packages = context.GetPackages().Select(i => new CataloguePackage(i, Items.FirstOrDefault(x => x.Data.SaleCode == i.SaleCode))).ToList();
                Effects = context.GetEffectSettings().ToDictionary(x => x.EffectId, x => new EffectType(x));
                Discounts = context.GetDiscounts();
            }

            DeserialisePageData();

            Log.ForContext<CatalogueManager>().Information("Loaded {Count} of Catalogue items", Pages.Count);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Handle item purchase
        /// </summary>
        public void Purchase(int avatarId, int itemId, int amount, string extraData, long datePurchase, bool isClubGift = false)
        {
            CatalogueItem catalogueItem = Items.FirstOrDefault(x => x.Data.Id == itemId);

            if (catalogueItem == null)
                return;

            if (catalogueItem.Definition != null && catalogueItem.Definition.HasBehaviour(ItemBehaviour.EFFECT))
            {
                PurchaseEffect(avatarId, catalogueItem, amount);
                return;
            }

            List<ItemData> purchaseQueue = new List<ItemData>();

            for (int i = 0; i < amount; i++)
            {
                foreach (var cataloguePackage in catalogueItem.Packages)
                {
                    var dataList = GenerateItemData(avatarId, cataloguePackage, extraData, datePurchase);

                    if (!dataList.Any())
                        continue;

                    purchaseQueue.AddRange(dataList);
                }
            }

            // Bulk create items - ignore teleporters because they were already created
            using (var context = new StorageContext())
            {
                context.CreateItems(purchaseQueue);
            }

            // Convert item data to item instance
            List<Item> items = purchaseQueue.Select(x => new Item(x)).ToList();

            var avatar = AvatarManager.Instance.GetAvatarById(avatarId);

            if (avatar == null)
                return;

            foreach (var item in items)
                avatar.Inventory.AddItem(item);

            if (isClubGift)
                avatar.Send(new ClubGiftReceivedComposer(catalogueItem));
            else
                avatar.Send(new PurchaseOKComposer(catalogueItem));

            avatar.Send(new FurniListNotificationComposer(items));
            avatar.Send(new FurniListUpdateComposer());
        }

        /// <summary>
        /// Purchase effect handler
        /// </summary>
        private void PurchaseEffect(int AvatarId, CatalogueItem catalogueItem, int amount)
        {
            using (var context = new StorageContext())
            {
                List<EffectData> purchaseEffectsQueue = new List<EffectData>();
                var existingEffects = context.GetUserEffects(AvatarId);

                for (int i = 0; i < amount; i++)
                {
                    foreach (var cataloguePackage in catalogueItem.Packages)
                    {
                        var dataList = GenerateEffectData(AvatarId, cataloguePackage, existingEffects);

                        if (!dataList.Any())
                            continue;

                        purchaseEffectsQueue.AddRange(dataList);
                    }
                }

                // Bulk create items
                context.SaveEffects(purchaseEffectsQueue);

                var avatar = AvatarManager.Instance.GetAvatarById(AvatarId);

                if (avatar == null)
                    return;

                avatar.Send(new PurchaseOKComposer(catalogueItem));
                purchaseEffectsQueue.ForEach(avatar.EffectManager.AddEffect);
            }
        }

        /// <summary>
        /// Generate effects queue data.
        /// </summary>
        private List<EffectData> GenerateEffectData(int AvatarId, CataloguePackage cataloguePackage, List<EffectData> existingEffects)
        {
            var effects = new List<EffectData>();
            var itemsToGenerate = cataloguePackage.Data.Amount;

            for (int i = 0; i < itemsToGenerate; i++)
            {
                EffectData itemData = existingEffects.Where(x => x.EffectId == cataloguePackage.Definition.Data.SpriteId).FirstOrDefault();

                if (itemData != null)
                {
                    itemData.Quantity++;
                }
                else
                {
                    itemData = new EffectData();
                    itemData.AvatarId = AvatarId;
                    itemData.EffectId = cataloguePackage.Definition.Data.SpriteId;
                }

                effects.Add(itemData);
            }

            return effects;
        }

        /// <summary>
        /// Generate item data for purchasing item
        /// </summary>
        private List<ItemData> GenerateItemData(int avatarId, CataloguePackage cataloguePackage, string userInputMessage, long datePurchase)
        {
            var definition = cataloguePackage.Definition;

            if (definition == null)
                return null;

            var items = new List<ItemData>();
            var itemsToGenerate = cataloguePackage.Data.Amount;

            int? itemGroupId = null;

            object serializeable = null;

            switch (definition.InteractorType)
            {
                case InteractorType.GUILD:
                case InteractorType.GUILD_GATE:
                    {
                        if (int.TryParse(userInputMessage, out int groupId))
                        {
                            var groupList = GroupManager.Instance.GetGroupsByMembership(avatarId, GroupMembershipType.ADMIN, GroupMembershipType.MEMBER);

                            if (groupList.Any(x => x.Data.Id == groupId))
                            {
                                itemGroupId = groupId;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }

                        break;
                    }
                case InteractorType.TROPHY:
                    {
                        serializeable = new TrophyExtraData
                        {
                            AvatarId = avatarId,
                            Message = userInputMessage,
                            Date = datePurchase
                        };
                    }
                    break;
                case InteractorType.MANNEQUIN:
                    {
                        //  { "Gender":"M","Figure":"lg-275-64.cc-260-62.ca-1806-73.ch-3030-62.hd-180-1.hr-100-61","OutfitName":"Default Mannequin"}

                        serializeable = new MannequinExtraData();

                        /*
                        {
                            //"ch-3030-66.lg-275-64.ca-1806-73.cc-260-1408",
                            //"ch-875-1331-1331.lg-280-91.sh-295-1331.wa-3211-110-110.cc-3007-1331-1331"
                            //"hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80"
                            Figure = "lg-275-64.cc-260-62.ca-1806-73.ch-3030-62.hd-180-1.hr-100-61",
                            Gender = "M",
                            OutfitName = "Default Mannequin"
                        };
                        */
                    }
                    break;
            }

            var extraData = string.Empty;

            if (serializeable != null)
                extraData = JsonConvert.SerializeObject(serializeable);

            if (!string.IsNullOrEmpty(cataloguePackage.Data.SpecialSpriteId))
                extraData = cataloguePackage.Data.SpecialSpriteId;

            if (definition.InteractorType == InteractorType.TELEPORTER)
            {
                ItemData firstTeleporter = new ItemData();
                firstTeleporter.OwnerId = avatarId;
                firstTeleporter.DefinitionId = cataloguePackage.Definition.Data.Id;

                ItemData secondTeleporter = new ItemData();
                secondTeleporter.OwnerId = avatarId;
                secondTeleporter.DefinitionId = cataloguePackage.Definition.Data.Id;

                firstTeleporter.ExtraData = JsonConvert.SerializeObject(new TeleporterExtraData
                {
                    LinkedItem = secondTeleporter.Id.ToString()
                });

                secondTeleporter.ExtraData = JsonConvert.SerializeObject(new TeleporterExtraData
                {
                    LinkedItem = firstTeleporter.Id.ToString()
                });

                items.Add(firstTeleporter);
                items.Add(secondTeleporter);
            }
            else
            {
                for (int i = 0; i < itemsToGenerate; i++)
                {
                    ItemData itemData = new ItemData();
                    itemData.OwnerId = avatarId;
                    itemData.DefinitionId = cataloguePackage.Definition.Data.Id;
                    itemData.ExtraData = extraData;

                    if (itemGroupId != null)
                    {
                        itemData.GroupId = itemGroupId;
                    }

                    items.Add(itemData);
                }
            }
            
            return items;
        }

        /// <summary>
        /// Convert JSON arrays to list of images and strings
        /// </summary>
        public void DeserialisePageData()
        {
            foreach (var page in Pages)
            {
                page.Images = JsonConvert.DeserializeObject<List<string>>(page.Data.ImagesData);
                page.Texts = JsonConvert.DeserializeObject<List<string>>(page.Data.TextsData);

                TryGetBestDiscount(page.Data.Id, out var bestDiscount);

                if (bestDiscount == null)
                {
                    foreach (var item in GetItems(page.Data.Id))
                        item.AllowBulkPurchase = false;
                }
            }
        }

        /// <summary>
        /// Get applicable pages for parent id
        /// </summary>
        public List<CataloguePage> GetPages(int parentId, int rank, bool hasClub)
        {
            var pages = Pages.Where(x => x.Data.ParentId == parentId && x.Data.IsEnabled && rank >= x.Data.MinRank).ToList();

            if (!hasClub)
                pages = pages.Where(x => !x.Data.IsClubOnly).ToList();

            return pages.OrderBy(x => x.Data.OrderId).ToList();
        }

        /// <summary>
        /// Get page by page id
        /// </summary>
        public CataloguePage GetPage(int pageId, int rank = 7, bool hasClub = true)
        {
            var page = Pages.Where(x => x.Data.Id == pageId && x.Data.IsEnabled && x.Data.IsNavigatable && rank >= x.Data.MinRank).SingleOrDefault();

            if (page == null)
                return null;

            if (page.Data.IsClubOnly && !hasClub)
                return null;

            return page;
        }

        /// <summary>
        /// Get applicable items for page id
        /// </summary>
        public List<CatalogueItem> GetItems(int pageId)
        {
            return Items.Where(x => x.PageIds.Contains(pageId) && !x.Data.IsHidden).OrderBy(x => x.Data.OrderId).ToList();
        }

        /// <summary>
        /// Get item by sale code
        /// </summary>
        public CatalogueItem GetItem(string saleCode)
        {
            return Items.Where(x => x.Data.SaleCode != null && x.Data.SaleCode == saleCode).SingleOrDefault();
        }

        /// <summary>
        /// Get the best discount in the list of discounts by page id
        /// </summary>
        public void TryGetBestDiscount(int pageId, out CatalogueDiscountData catalogueDiscountData)
        {
            catalogueDiscountData = null;

            var discounts = Discounts.Where(x => x.PageId == pageId && (x.ExpireDate > DateTime.Now || x.ExpireDate == null)).ToList();

            if (!discounts.Any())
                return;

            catalogueDiscountData = discounts
                .Where(x => x.DiscountBatchSize > 0 && x.DiscountAmountPerBatch > 0)
                .OrderByDescending(x => x.DiscountAmountPerBatch / x.DiscountBatchSize)
                .FirstOrDefault();
        }

        /// <summary>
        /// Get package by catalogue item sale code
        /// </summary>
        public List<CataloguePackage> GetPackages(string saleCode)
        {
            return Packages.Where(x => x.Data.SaleCode == saleCode).ToList();
        }

        /// <summary>
        /// Get effect settings
        /// </summary>
        public EffectType GetEffectSetting(int effectId)
        {
            return Effects[effectId];
        }

        #endregion
    }
}
