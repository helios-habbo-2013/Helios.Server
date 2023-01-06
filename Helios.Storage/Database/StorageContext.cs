using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Helios.Storage.Database.Data;
using Helios.Storage.Database.Access;
using System.Linq;
using System.Diagnostics;
using System;

namespace Helios.Storage.Database
{
    public class StorageContext : DbContext
    {

        #region Fields

        private static readonly StorageContext m_SessionFactoryBuilder = new StorageContext();

        public DbSet<PlayerData> PlayerData { get; set; }
        public DbSet<AuthenicationTicketData> AuthenicationTicketData { get; set; }
        public DbSet<SettingsData> SettingsData { get; set; }
        public DbSet<ItemData> ItemData { get; set; }
        public DbSet<ItemDefinitionData> ItemDefinitionData { get; set; }
        public DbSet<MessengerFriendData> MessengerFriendData { get; set; }
        public DbSet<MessengerRequestData> MessengerRequestData { get; set; }
        public DbSet<MessengerCategoryData> MessengerCategoryData { get; set; }
        public DbSet<PlayerSettingsData> PlayerSettingsData { get; set; }
        public DbSet<CataloguePageData> CataloguePageData { get; set; }
        public DbSet<CatalogueItemData> CatalogueItemData { get; set; }
        public DbSet<CatalogueDiscountData> CatalogueDiscountData { get; set; }
        public DbSet<CataloguePackageData> CataloguePackageData { get; set; }
        public DbSet<CatalogueSubscriptionData> CatalogueSubscriptionData { get; set; }
        public DbSet<MessengerChatData> MessengerChatData { get; set; }
        public DbSet<PublicItemData> PublicItemData { get; set; }
        public DbSet<NavigatorCategoryData> NavigatorCategoryData { get; set; }
        public DbSet<RoomData> RoomData { get; set; }
        public DbSet<RoomModelData> RoomModelData { get; set; }
        public DbSet<TagData> TagData { get; set; }
        public DbSet<CurrencyData> CurrencyData { get; set; }
        public DbSet<SubscriptionData> SubscriptionData { get; set; }
        public DbSet<SubscriptionGiftData> SubscriptionGiftData { get; set; }

        #endregion

        #region Properties

        public static StorageContext Instance
        {
            get { return m_SessionFactoryBuilder; }
        }

        #endregion

        #region Public methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseMySQL("server=localhost;database=helios;user=root;password=123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerData>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Name).HasColumnName("username");
                entity.Property(x => x.Figure).HasColumnName("figure").HasDefaultValue();
                entity.Property(x => x.Sex).HasColumnName("sex").HasDefaultValue();
                entity.Property(x => x.Rank).HasColumnName("rank").HasDefaultValue();
                entity.Property(x => x.Credits).HasColumnName("credits").HasDefaultValue();
                entity.Property(x => x.Motto).HasColumnName("motto").HasDefaultValue();
                entity.Property(x => x.JoinDate).HasColumnName("join_date").HasDefaultValue();
                entity.Property(x => x.LastOnline).HasColumnName("last_online").HasDefaultValue();

                entity.Ignore(x => x.AchievementPoints);
                entity.Ignore(x => x.RealName);
                entity.Ignore(x => x.PreviousLastOnline);
            });

            modelBuilder.Entity<AuthenicationTicketData>(entity =>
            {
                entity.ToTable("authentication_ticket");
                entity.HasKey(x => new { x.Ticket, x.UserId });

                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.Ticket).HasColumnName("sso_ticket");
                entity.Property(x => x.ExpireDate).HasColumnName("expires_at");
            });

            modelBuilder.Entity<AuthenicationTicketData>()
                .HasOne(e => e.PlayerData)
                .WithMany(c => c.Tickets)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<SettingsData>(entity =>
            {
                entity.ToTable("server_settings");
                entity.HasKey(x => x.Key);
                entity.Property(x => x.Key).HasColumnName("setting");
                entity.Property(x => x.Value).HasColumnName("value");
            });

            modelBuilder.Entity<ItemData>(entity =>
            {
                entity.ToTable("item");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.OrderId).HasColumnName("order_id");
                entity.Property(x => x.OwnerId).HasColumnName("user_id");
                entity.Property(x => x.RoomId).HasColumnName("room_id").HasDefaultValue();
                entity.Property(x => x.DefinitionId).HasColumnName("definition_id");
                entity.Property(x => x.X).HasColumnName("x").HasDefaultValue();
                entity.Property(x => x.Y).HasColumnName("y").HasDefaultValue();
                entity.Property(x => x.Z).HasColumnName("z").HasDefaultValue();
                entity.Property(x => x.WallPosition).HasColumnName("wall_position").HasDefaultValue();
                entity.Property(x => x.Rotation).HasColumnName("rotation").HasDefaultValue();
                entity.Property(x => x.ExtraData).HasColumnName("custom_data").HasDefaultValue();

                entity.HasOne(e => e.OwnerData)
                    .WithMany(p => p.Items)
                    .HasForeignKey(x => x.OwnerId);
            });

            modelBuilder.Entity<ItemDefinitionData>(entity =>
            {
                entity.ToTable("item_definitions");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Sprite).HasColumnName("sprite");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.SpriteId).HasColumnName("sprite_id");
                entity.Property(x => x.Length).HasColumnName("length");
                entity.Property(x => x.Width).HasColumnName("width");
                entity.Property(x => x.TopHeight).HasColumnName("top_height");
                entity.Property(x => x.MaxStatus).HasColumnName("max_status");
                entity.Property(x => x.Behaviour).HasColumnName("behaviour");
                entity.Property(x => x.Interactor).HasColumnName("interactor");
                entity.Property(x => x.IsTradable).HasColumnName("is_tradable");
                entity.Property(x => x.IsRecyclable).HasColumnName("is_recyclable");
                entity.Property(x => x.IsStackable).HasColumnName("is_stackable");
                entity.Property(x => x.IsSellable).HasColumnName("is_sellable");
                entity.Property(x => x.DrinkIds).HasColumnName("drink_ids");
            });

            modelBuilder.Entity<MessengerFriendData>(entity =>
            {
                entity.ToTable("messenger_friend");
                entity.HasKey(x => new { x.UserId, x.FriendId });

                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.FriendId).HasColumnName("friend_id");

                entity.HasOne(e => e.FriendData)
                    .WithMany(p => p.Friends)
                    .HasForeignKey(x => x.FriendId);
            });


            modelBuilder.Entity<MessengerRequestData>(entity =>
            {
                entity.ToTable("messenger_request");
                entity.HasKey(x => new { x.UserId, x.FriendId });

                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.FriendId).HasColumnName("friend_id");

                entity.HasOne(e => e.FriendData)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(x => x.FriendId);
            });

            modelBuilder.Entity<MessengerCategoryData>(entity =>
            {
                entity.ToTable("messenger_category");
                entity.HasKey(x => new { x.UserId, x.Label });

                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.Label).HasColumnName("label");
            });

            modelBuilder.Entity<MessengerChatData>(entity =>
            {
                entity.ToTable("messenger_chat_history");
                entity.HasKey(x => new { x.UserId, x.FriendId, x.Message });

                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.FriendId).HasColumnName("friend_id");
                entity.Property(x => x.Message).HasColumnName("message");
                entity.Property(x => x.IsRead).HasColumnName("has_read").HasDefaultValue();
                entity.Property(x => x.MessagedAt).HasColumnName("messaged_at").HasDefaultValue();
            });


            modelBuilder.Entity<PlayerSettingsData>(entity =>
            {
                entity.ToTable("user_settings");
                entity.HasKey(x => x.UserId);
                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.Respect).HasColumnName("respect_points").HasDefaultValue();
                entity.Property(x => x.DailyRespectPoints).HasColumnName("daily_respect_points").HasDefaultValue();
                entity.Property(x => x.DailyPetRespectPoints).HasColumnName("daily_respect_pet_points").HasDefaultValue();
                entity.Property(x => x.FriendRequestsEnabled).HasColumnName("friend_requests_enabled").HasDefaultValue();
                entity.Property(x => x.FollowingEnabled).HasColumnName("following_enabled").HasDefaultValue();
                entity.Property(x => x.OnlineTime).HasColumnName("online_time").HasDefaultValue();

                entity.HasOne(x => x.PlayerData)
                    .WithOne(x => x.Settings)
                    .HasForeignKey<PlayerSettingsData>(x => x.UserId);

            });

            modelBuilder.Entity<CataloguePageData>(entity =>
            {
                entity.ToTable("catalogue_pages");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.ParentId).HasColumnName("parent_id");
                entity.Property(x => x.OrderId).HasColumnName("order_id");
                entity.Property(x => x.Caption).HasColumnName("caption");
                entity.Property(x => x.PageLink).HasColumnName("page_link");
                entity.Property(x => x.MinRank).HasColumnName("min_rank");
                entity.Property(x => x.IconColour).HasColumnName("icon_colour");
                entity.Property(x => x.IconImage).HasColumnName("icon_image");
                entity.Property(x => x.IsNavigatable).HasColumnName("is_navigatable");
                entity.Property(x => x.IsEnabled).HasColumnName("is_enabled");
                entity.Property(x => x.IsClubOnly).HasColumnName("is_club_only");
                entity.Property(x => x.Layout).HasColumnName("layout");
                entity.Property(x => x.ImagesData).HasColumnName("images");
                entity.Property(x => x.TextsData).HasColumnName("texts");
            });

            modelBuilder.Entity<CatalogueItemData>(entity =>
            {
                entity.ToTable("catalogue_items");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.SaleCode).HasColumnName("sale_code");
                entity.Property(x => x.PageId).HasColumnName("page_id");
                entity.Property(x => x.OrderId).HasColumnName("order_id");
                entity.Property(x => x.PriceCoins).HasColumnName("price_coins");
                entity.Property(x => x.PriceSeasonal).HasColumnName("price_seasonal");
                entity.Property(x => x.SeasonalType).HasColumnName("seasonal_type").HasConversion(
                    v => v.ToString(),
                    v => (SeasonalCurrencyType)Enum.Parse(typeof(SeasonalCurrencyType), v));
                entity.Property(x => x.IsHidden).HasColumnName("hidden");
                entity.Property(x => x.Amount).HasColumnName("amount");
                entity.Property(x => x.DefinitionId).HasColumnName("definition_id");
                entity.Property(x => x.SpecialSpriteId).HasColumnName("item_specialspriteid");
                entity.Property(x => x.IsPackage).HasColumnName("is_package");
                entity.Property(x => x.AllowBulkPurchase).HasColumnName("allow_bulk_purchase");
            });

            modelBuilder.Entity<CataloguePackageData>(entity =>
            {
                entity.ToTable("catalogue_packages");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.SaleCode).HasColumnName("salecode");
                entity.Property(x => x.DefinitionId).HasColumnName("definition_id");
                entity.Property(x => x.SpecialSpriteId).HasColumnName("special_sprite_id");
                entity.Property(x => x.Amount).HasColumnName("amount");
            });

            modelBuilder.Entity<CatalogueDiscountData>(entity =>
            {
                entity.ToTable("catalogue_discounts");
                entity.HasKey(x => x.PageId);
                entity.Property(x => x.PageId).HasColumnName("page_id");
                entity.Property(x => x.PurchaseLimit).HasColumnName("purchase_limit");
                entity.Property(x => x.DiscountBatchSize).HasColumnName("item_count_required");
                entity.Property(x => x.DiscountAmountPerBatch).HasColumnName("item_count_free");
                entity.Property(x => x.ExpireDate).HasColumnName("expire_at");
                entity.Property(x => x.MinimumDiscountForBonus).HasColumnName("minimum_required");
            });

            modelBuilder.Entity<CatalogueSubscriptionData>(entity =>
            {
                entity.ToTable("catalogue_subscriptions");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.PageId).HasColumnName("page_id");
                entity.Property(x => x.PriceCoins).HasColumnName("price_coins");
                entity.Property(x => x.PriceSeasonal).HasColumnName("price_seasonal");
                entity.Property(x => x.SeasonalType).HasColumnName("seasonal_type")
                    .HasConversion(
                        v => v.ToString(),
                        v => (SeasonalCurrencyType)Enum.Parse(typeof(SeasonalCurrencyType), v));

                entity.Property(x => x.Months).HasColumnName("months");
            });

            //public DbSet<PublicItemData> PublicItemData { get; set; }
            //public DbSet<NavigatorCategoryData> NavigatorCategoryData { get; set; }

            modelBuilder.Entity<PublicItemData>(entity =>
            {
                entity.ToTable("navigator_official_rooms");
                entity.HasKey(x => x.BannerId);
                entity.Property(x => x.BannerId).HasColumnName("banner_id");
                entity.Property(x => x.ParentId).HasColumnName("parent_id");
                entity.Property(x => x.BannerType).HasColumnName("banner_type").HasConversion(
                    v => v.ToString(),
                    v => (BannerType)Enum.Parse(typeof(BannerType), v));
                entity.Property(x => x.RoomId).HasColumnName("room_id");
                entity.Property(x => x.ImageType).HasColumnName("image_type").HasConversion(
                    v => v.ToString(),
                    v => (ImageType)Enum.Parse(typeof(ImageType), v));
                entity.Property(x => x.Label).HasColumnName("label");
                entity.Property(x => x.Description).HasColumnName("description");
                entity.Property(x => x.DescriptionEntry).HasColumnName("description_entry");
                entity.Property(x => x.Image).HasColumnName("image_url");

                entity.HasOne(x => x.Room)
                    .WithOne(x => x.PublicItem)
                    .HasForeignKey<RoomData>(x => x.Id)
                    .IsRequired(false);

            });


            modelBuilder.Entity<NavigatorCategoryData>(entity =>
            {
                entity.ToTable("room_category");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Caption).HasColumnName("caption");
                entity.Property(x => x.IsEnabled).HasColumnName("enabled");
                entity.Property(x => x.MinimumRank).HasColumnName("min_rank");
            });

            modelBuilder.Entity<RoomData>(entity =>
            {
                entity.ToTable("room");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.OwnerId).HasColumnName("owner_id");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.Description).HasColumnName("description");
                entity.Property(x => x.CategoryId).HasColumnName("category_id").HasDefaultValue();
                entity.Property(x => x.UsersNow).HasColumnName("visitors_now").HasDefaultValue();
                entity.Property(x => x.UsersMax).HasColumnName("visitors_max").HasDefaultValue();
                entity.Property(x => x.Status).HasColumnName("status").HasDefaultValue().HasConversion(
                    v => v.ToString(),
                    v => (RoomStatus)Enum.Parse(typeof(RoomStatus), v));

                entity.Property(x => x.Password).HasColumnName("password").HasDefaultValue();
                entity.Property(x => x.ModelId).HasColumnName("model_id");
                entity.Property(x => x.CCTs).HasColumnName("ccts").HasDefaultValue();
                entity.Property(x => x.Wallpaper).HasColumnName("wallpaper").HasDefaultValue();
                entity.Property(x => x.Floor).HasColumnName("floor").HasDefaultValue();
                entity.Property(x => x.Landscape).HasColumnName("landscape").HasDefaultValue();
                entity.Property(x => x.AllowPets).HasColumnName("allow_pets").HasDefaultValue();
                entity.Property(x => x.AllowPetsEat).HasColumnName("allow_pets_eat").HasDefaultValue();
                entity.Property(x => x.AllowWalkthrough).HasColumnName("allow_walkthrough").HasDefaultValue();
                entity.Property(x => x.IsHidingWall).HasColumnName("hidewall").HasDefaultValue();
                entity.Property(x => x.WallThickness).HasColumnName("wall_thickness").HasDefaultValue();
                entity.Property(x => x.FloorThickness).HasColumnName("floor_thickness").HasDefaultValue();
                entity.Property(x => x.Rating).HasColumnName("rating").HasDefaultValue();
                entity.Property(x => x.IsOwnerHidden).HasColumnName("is_owner_hidden").HasDefaultValue();
                entity.Property(x => x.TradeSetting).HasColumnName("trade_setting").HasDefaultValue();
                entity.Property(x => x.IsMuted).HasColumnName("is_muted").HasDefaultValue();
                entity.Property(x => x.WhoCanBan).HasColumnName("who_can_ban").HasDefaultValue().HasConversion(
                    v => v.ToString(),
                    v => (RoomBanSetting)Enum.Parse(typeof(RoomBanSetting), v));

                entity.Property(x => x.WhoCanKick).HasColumnName("who_can_kick").HasDefaultValue().HasConversion(
                    v => v.ToString(),
                    v => (RoomKickSetting)Enum.Parse(typeof(RoomKickSetting), v));

                entity.Property(x => x.WhoCanMute).HasColumnName("who_can_mute").HasDefaultValue()
                .HasConversion(
                    v => v.ToString(),
                    v => (RoomMuteSetting)Enum.Parse(typeof(RoomMuteSetting), v));

                entity.HasOne(e => e.OwnerData)
                    .WithMany(c => c.Rooms)
                    .HasForeignKey(x => x.OwnerId)
                    .IsRequired(false);


                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Rooms)
                    .HasForeignKey(x => x.CategoryId);

            });

            modelBuilder.Entity<TagData>(entity =>
            {
                entity.ToTable("tags");
                entity.HasKey(x => new { x.UserId, x.RoomId, x.Text });
                entity.Property(x => x.UserId).HasColumnName("user_id").HasDefaultValue();
                entity.Property(x => x.RoomId).HasColumnName("room_id").HasDefaultValue();
                entity.Property(x => x.Text).HasColumnName("text").HasDefaultValue();

                entity.HasOne(x => x.RoomData)
                    .WithMany(x => x.Tags)
                    .HasForeignKey(x => x.RoomId);
            });

            modelBuilder.Entity<RoomModelData>(entity =>
            {
                entity.ToTable("room_model");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Model).HasColumnName("model");
                entity.Property(x => x.DoorX).HasColumnName("door_x");
                entity.Property(x => x.DoorY).HasColumnName("door_y");
                entity.Property(x => x.DoorZ).HasColumnName("door_z");
                entity.Property(x => x.DoorDirection).HasColumnName("door_dir");
                entity.Property(x => x.Heightmap).HasColumnName("heightmap");
                entity.Property(x => x.IsClubOnly).HasColumnName("is_club_only");
            });

            modelBuilder.Entity<CurrencyData>(entity =>
            {
                entity.ToTable("user_seasonal_currencies");
                entity.HasKey(x => new { x.UserId, x.SeasonalType });
                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.SeasonalType).HasColumnName("seasonal_type")
                .HasConversion(
                    v => v.ToString(),
                    v => (SeasonalCurrencyType)Enum.Parse(typeof(SeasonalCurrencyType), v));

                entity.Property(x => x.Balance).HasColumnName("balance");
            });

            modelBuilder.Entity<SubscriptionData>(entity =>
            {
                entity.ToTable("user_subscriptions");
                entity.HasKey(x => x.UserId);
                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.SubscribedDate).HasColumnName("subscribed_at");
                entity.Property(x => x.ExpireDate).HasColumnName("expire_at");
                entity.Property(x => x.GiftDate).HasColumnName("gift_at");
                entity.Property(x => x.GiftsRedeemable).HasColumnName("gifts_redeemable");
                entity.Property(x => x.SubscriptionAge).HasColumnName("subscription_age");
                entity.Property(x => x.SubscriptionAgeLastUpdated).HasColumnName("subscription_age_last_updated");
            });

            modelBuilder.Entity<SubscriptionGiftData>(entity =>
            {
                entity.ToTable("subscription_gifts");
                entity.HasKey(x => x.SaleCode);
                entity.Property(x => x.SaleCode).HasColumnName("sale_code");
                entity.Property(x => x.DurationRequirement).HasColumnName("duration_requirement");
            });
        }



        public void Init()
        {
            using (var context = new StorageContext())
            {
                context.Database.EnsureCreated();
                //var t = NavigatorDao.GetPublicItems();
            }

        }
    }

    #endregion
}
