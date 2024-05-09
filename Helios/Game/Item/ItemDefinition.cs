﻿using System;
using System.Collections.Generic;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Item;

namespace Helios.Game
{
    [Serializable]
    public class ItemDefinition
    {
        #region Properties

        public ItemDefinitionData Data { get; }
        public InteractorType InteractorType { get; }
        public List<ItemBehaviour> Behaviours { get; }
        public string Type
        {
            get
            {
                if (Behaviours.Contains(ItemBehaviour.WALL_ITEM))
                    return "i";

                if (Behaviours.Contains(ItemBehaviour.EFFECT))
                    return "e";

                return "s";
            }
        }

        #endregion

        #region Constructors

        public ItemDefinition(ItemDefinitionData data)
        {
            Data = data;
            Behaviours = new List<ItemBehaviour>();
            ParseBehaviours();

            try
            {
                InteractorType = (InteractorType)Enum.Parse(typeof(InteractorType), Data.Interactor.ToUpper());
            }
            catch (Exception ex)
            {
 
                Console.WriteLine("Could not parse interactor: " +  ex);
                InteractorType = InteractorType.DEFAULT;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Parse behaviour string
        /// </summary>
        private void ParseBehaviours()
        {
            bool recreateBehaviour = false;

            foreach (string behaviourData in Data.Behaviour.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    ItemBehaviour behaviour = (ItemBehaviour) Enum.Parse(typeof(ItemBehaviour), behaviourData.ToUpper());
                    Behaviours.Add(behaviour);
                }
                catch 
                {
                    //recreateBehaviour = true;
                    Console.WriteLine("Could not parse behaviour: " + Data.Id + " / " + behaviourData);
                }
            }

            if (recreateBehaviour)
            {
                List<string> behaviourList = new List<string>();

                foreach (ItemBehaviour behaviour in Enum.GetValues(typeof(ItemBehaviour)))
                {
                    if (HasBehaviour(behaviour))
                    {
                        behaviourList.Add(Enum.GetName(typeof(ItemBehaviour), behaviour).ToLower());
                    }
                }

                //behaviourList.Add("is_walkable");
                string behaviours = string.Join(",", behaviourList);

                this.Data.Behaviour = behaviours;
                //this.Data.Interactor = "one_way_gate";

                using (var context = new GameStorageContext())
                {
                    context.SaveDefinition(this.Data);
                }
            }
        }

        /// <summary>
        /// Get if definition has behaviour
        /// </summary>
        public bool HasBehaviour(ItemBehaviour behaviour)
        {
            return Behaviours.Contains(behaviour);
        }

        /// <summary>
        /// Get top height, but any top height below 0 will be returned as 0.
        /// </summary>
        /// <returns></returns>
        public double GetPositiveTopHeight()
        {
            if (Data.TopHeight < 0)
            {
                return 0;
            }

            return Data.TopHeight;
        }

        #endregion
    }
}
