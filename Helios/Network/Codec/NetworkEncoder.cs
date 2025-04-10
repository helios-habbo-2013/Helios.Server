using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Helios.Messages;
using Helios.Network.Session;
using Helios.Network.Streams;
using Helios.Network.Streams.Util;
using Serilog;

namespace Helios.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<IMessageComposer>
    {
        protected override void Encode(IChannelHandlerContext ctx, IMessageComposer composer, List<object> output)
        {
            ConnectionSession connection = ctx.Channel.GetAttribute(GameNetworkHandler.CONNECTION_KEY).Get();

            try
            {
                int? header = MessageHandler.Instance.GetComposerId(composer);

                if (header == null)
                {
                    Log.ForContext<NetworkEncoder>().Error($"No header found for composer class {composer.GetType().Name}");
                    return;
                }

                var buffer = Unpooled.Buffer();
                var response = new Response(header.Value, buffer);

                foreach (var objectData in composer.Data)
                {
                    if (objectData is string)
                        response.WriteString((string)objectData);

                    if (objectData is int || objectData is uint)
                        response.WriteInt((int)objectData);

                    if (objectData is bool)
                        response.WriteBool((bool)objectData);

                    if (objectData is TextEntry entry)
                        response.Write(entry.Value);

                    if (objectData is KeyValueEntry kve)
                    {
                        response.Write(kve.Key);
                        response.Write(kve.Delimiter);
                        response.Write(kve.Value);
                        response.Write((char)13);
                    }

                    if (objectData is ValueEntry tab)
                    {
                        response.Write(tab.Value);
                        response.Write(tab.Delimiter.ToString());
                    }
                }

                buffer.WriteByte((char)1);

                if (connection != null)
                    Log.ForContext<NetworkEncoder>().Debug($"SENT {composer.GetType().Name}: " + response.Header + " / " + response.MessageBody);

                output.Add(buffer);
            }
            catch (Exception ex)
            {
                Log.ForContext<NetworkEncoder>().Error(ex, "Error occurred while encoding packets ");
            }
        }
    }
}