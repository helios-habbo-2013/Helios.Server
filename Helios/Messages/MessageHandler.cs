using Helios.Game;
using Helios.Messages.Headers;
using Helios.Network.Streams;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Helios.Messages
{
    public class MessageHandler : ILoadable
    {
        #region Fields

        public static readonly MessageHandler Instance = new MessageHandler();

        #endregion

        #region Properties

        private Dictionary<int, List<IMessageEvent>> Events { get; }
        private Dictionary<string, int> Composers { get; }


        #endregion

        #region Constructors

        public MessageHandler()
        {
            Events = new Dictionary<int, List<IMessageEvent>>();
            Composers = new Dictionary<string, int>();
        }

        public void Load()
        {
            Log.ForContext<MessageHandler>().Information("Loading message handler");

            ResolveMessages();

            Log.ForContext<MessageHandler>().Information("Loaded {EventCount} events and {ComposerCount} composers", Events.Count, Composers.Count);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Resolve events, instead of assigning to every event file, associate by file name instead
        /// </summary>
        public void ResolveMessages()
        {
            Type incomingEventType = typeof(IncomingEvents);
            Type outgoingEventType = typeof(OutgoingEvents);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => 
                (typeof(IMessageEvent).IsAssignableFrom(p) && p != typeof(IMessageEvent)) || 
                (typeof(IMessageComposer).IsAssignableFrom(p) && p != typeof(IMessageComposer)));

            foreach (var packetType in types)
            {
                if (typeof(IMessageEvent).IsAssignableFrom(packetType)) {
                    var incomingField = incomingEventType.GetField(packetType.Name);

                    if (incomingField != null)
                    {
                        short header = Convert.ToInt16(incomingField.GetValue(null));

                        if (!Events.ContainsKey(header))
                            Events.Add(header, new List<IMessageEvent>());

                        Events[header].Add((IMessageEvent)Activator.CreateInstance(packetType));
                    }

                    else
                        Log.ForContext<MessageHandler>().Error($"Event {packetType.Name} has no header defined");
                }

                if (typeof(IMessageComposer).IsAssignableFrom(packetType)) {
                    var composerField = outgoingEventType.GetField(packetType.Name);

                    if (composerField != null)
                        Composers[packetType.Name] = Convert.ToInt16(composerField.GetValue(null));
                    else
                        Log.ForContext<MessageHandler>().Error($"Composer {packetType.Name} has no header defined");
                }
                /**/
            }
        }

        /// <summary>
        /// Get composer id for type
        /// </summary>
        internal int? GetComposerId(IMessageComposer composer)
        {

            if (Composers.TryGetValue(composer.GetType().Name, out int header))
                return header;

            return null;
        }

        /// <summary>
        /// Handler for incoming message
        /// </summary>
        /// <param name="avatar"></param>
        /// <param name="request"></param>
        public void HandleMesage(Avatar avatar, Request request)
        {
            try
            {
                if (Events.TryGetValue(request.HeaderId, out List<IMessageEvent> value))
                {
                    Log.ForContext<MessageHandler>().Debug($"RECEIVED {value[0].GetType().Name}: {request.Header} / {request.MessageBody}");

                    foreach (IMessageEvent handler in value)
                    {
                        if (Events[request.HeaderId].Count > 1)
                        {
                            var copyBuffer = request.Buffer.Copy();
                            handler.Handle(avatar, new Request(copyBuffer)); 
                            copyBuffer.Release();
                        }
                        else
                        {
                            handler.Handle(avatar, request);
                        }
                    }

                    request.Buffer.Release();
                } 
                else
                {
                    Log.ForContext<MessageHandler>().Debug($"Unknown: [{request.HeaderId}] {request.Header} / {request.MessageBody}");
                }
            }
            catch (Exception ex)
            {
                Log.ForContext<MessageHandler>().Error(ex, "Error occurred in MessageHander");
            }
        }

        #endregion
    }
}
