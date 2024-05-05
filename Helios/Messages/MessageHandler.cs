﻿using Helios.Game;
using Helios.Messages.Headers;
using Helios.Network.Streams;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Helios.Messages
{
    public class MessageHandler : ILoadable
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly MessageHandler Instance = new MessageHandler();

        #endregion

        #region Properties

        private Dictionary<short, List<IMessageEvent>> Events { get; }
        private Dictionary<string, short> Composers { get; }


        #endregion

        #region Constructors

        public MessageHandler()
        {
            Events = new Dictionary<short, List<IMessageEvent>>();
            Composers = new Dictionary<string, short>();
        }

        public void Load()
        {
            ResolveMessages();
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
                        log.Error($"Event {packetType.Name} has no header defined");
                }

                if (typeof(IMessageComposer).IsAssignableFrom(packetType)) {
                    var composerField = outgoingEventType.GetField(packetType.Name);

                    if (composerField != null)
                        Composers[packetType.Name] = Convert.ToInt16(composerField.GetValue(null));
                    else
                        log.Error($"Composer {packetType.Name} has no header defined");
                }
                /**/
            }
        }

        /// <summary>
        /// Get composer id for type
        /// </summary>
        internal short? GetComposerId(IMessageComposer composer)
        {
            short header;

            if (Composers.TryGetValue(composer.GetType().Name, out header))
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
                if (Events.ContainsKey(request.Header))
                {
                    avatar.Log.Debug($"RECEIVED {Events[request.Header][0].GetType().Name}: {request.Header} / {request.MessageBody}");

                    foreach (IMessageEvent handler in Events[request.Header])
                    {
                        if (Events[request.Header].Count > 1)
                        {
                            var copyBuffer = request.Buffer.Copy();
                            handler.Handle(avatar, new Request(request.Length, request.Header, copyBuffer));
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
                    avatar.Log.Debug($"Unknown: {request.Header} / {request.MessageBody}");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error occurred: ", ex);
            }
        }

        #endregion
    }
}
