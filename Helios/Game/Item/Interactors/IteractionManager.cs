﻿using Helios.Messages;
using System;
using System.Collections.Generic;

namespace Helios.Game
{
    public class InteractionManager : ILoadable
    {
        #region Fields

        public static readonly InteractionManager Instance = new InteractionManager();

        #endregion

        #region Properties

        public Dictionary<InteractorType, Type> Interactors { get; set; }

        #endregion

        #region Constructors

        public void Load()
        {
            Interactors = new Dictionary<InteractorType, Type>();
            Interactors[InteractorType.DEFAULT] = typeof(DefaultInteractor);
            Interactors[InteractorType.GATE] = typeof(DefaultInteractor);
            Interactors[InteractorType.BED] = typeof(BedInteractor);
            Interactors[InteractorType.CHAIR] = typeof(ChairInteractor);
            Interactors[InteractorType.POST_IT] = typeof(StickieInteractor);
            Interactors[InteractorType.TROPHY] = typeof(TrophyInteractor);
            Interactors[InteractorType.ROOMDIMMER] = typeof(MoodlightInteractor);
            Interactors[InteractorType.TELEPORTER] = typeof(TeleporterInteractor);
            Interactors[InteractorType.ROLLER] = typeof(RollerInteractor);
            Interactors[InteractorType.DICE] = typeof(DiceInteractor);
            Interactors[InteractorType.MANNEQUIN] = typeof(MannequinInteractor);
            Interactors[InteractorType.GUILD] = typeof(GuildInteractor);
            Interactors[InteractorType.GUILD_GATE] = typeof(GuildInteractor);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Create interactor instance for item
        /// </summary>
        public Interactor CreateInteractor(Item item)
        {
            if (item.Definition == null)
                return null;

            Type type;

            if (!Interactors.TryGetValue(item.Definition.InteractorType, out type))
                type = Interactors[InteractorType.DEFAULT];

            return (Interactor)Activator.CreateInstance(type, item);
        }

        /*
        /// <summary>
        /// Write the relevant extra data to the packet
        /// </summary>
        public void WriteExtraData(IMessageComposer composer, Item item, bool inventoryView = false)
        {
            var interactor = item.Interactor;
            switch (interactor.ExtraDataType)
            {
                case ExtraDataType.StringData:
                    composer.Data.Add((int)interactor.ExtraDataType);
                    composer.Data.Add((string)interactor.GetExtraData(inventoryView));
                    break;
                case ExtraDataType.GuildData:
                    composer.Data.Add((int)interactor.ExtraDataType);
                    composer.Data.Add((string)interactor.GetExtraData(inventoryView));
                    break;
                case ExtraDataType.StringArrayData:
                    var values = (Dictionary<string, string>)interactor.GetExtraData(inventoryView);
                    composer.Data.Add(values.Count);

                    foreach (var kvp in values)
                    {
                        composer.Data.Add(kvp.Key);
                        composer.Data.Add(kvp.Value);
                    }

                    break;
            }
        }*/

        #endregion
    }
}
