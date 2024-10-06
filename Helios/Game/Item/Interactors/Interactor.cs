using Helios.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Helios.Game
{
    public abstract class Interactor
    {
        #region Properties 

        public Item Item { get; }
        public virtual ITaskObject TaskObject { get; set; }

        #endregion

        #region Constructor

        protected Interactor(Item item)
        {
            Item = item;
        }

        #endregion

        #region Public methods

        public void SetExtraData(object jsonObject)
        {
            if (jsonObject is string)
                Item.Data.ExtraData = jsonObject.ToString();
            else
                Item.Data.ExtraData = JsonConvert.SerializeObject(jsonObject);
        }

        public virtual void WriteExtraData(IMessageComposer composer, bool inventoryView = false)
        {
            composer.Data.Add((int) ExtraDataType.StringData);
            composer.Data.Add(Item.Data.ExtraData);
        }

        public virtual T GetJsonObject<T>() where T : class { return JsonConvert.DeserializeObject<T>(Item.Data.ExtraData); }
        // public virtual void RefreshExtraData() { }
        public virtual void OnStop(IEntity entity) { }
        public virtual void OnInteract(IEntity entity, int requestData = 0) { }
        public virtual void OnPickup(IEntity entity) { TaskObject?.EventQueue.Clear(); }
        public virtual void OnPlace(IEntity entity) { TaskObject?.EventQueue.Clear(); }
        public virtual bool OnWalkRequest(IEntity entity, Position goal) { return false; }

        #endregion
    }

    public class QueuedEvent
    {
        #region Properties

        public string EventName { get; set; }
        public long TicksTimer { get; set; } 
        public Action<QueuedEvent> Action { get; set; }
        private Dictionary<object, object> Attributes { get; set; }

        #endregion

        #region Constructor

        public QueuedEvent(string eventName, Action<QueuedEvent> action, long ticksTimer, Dictionary<object, object> attributes)
        {
            EventName = eventName;
            Action = action;
            TicksTimer = ticksTimer;
            Attributes = attributes;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Has an attribute
        /// </summary>
        public bool HasAttribute(object key)
        {
            return Attributes.ContainsKey(key);
        }

        /// <summary>
        /// Get attribute by class it expects
        /// </summary>
        public T GetAttribute<T>(object key)
        {
            if (Attributes.ContainsKey(key))
            {
                return (T)Attributes[key];
            }

            return default(T);
        }

        #endregion
    }
}
