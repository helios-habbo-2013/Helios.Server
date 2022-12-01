﻿using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Helios.Network.Streams;
using Helios.Util;
using System.Collections.Generic;

namespace Helios.Network
{
    internal class NetworkDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext ctx, IByteBuffer buffer, List<object> output)
        {
            buffer.MarkReaderIndex();

            if (buffer.ReadableBytes < 6) 
            {
                // If the incoming data is less than 6 bytes, it's junk.
                return;
            }

            byte delimiter = buffer.ReadByte();
            buffer.ResetReaderIndex();

            if (delimiter == 60)
            {
                string policy = "<?xml version=\"1.0\"?>\r\n"
                        + "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n"
                        + "<cross-domain-policy>\r\n"
                        + "<allow-access-from domain=\"*\" to-ports=\"*\" />\r\n"
                        + "</cross-domain-policy>\0)";

                ctx.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(StringUtil.GetEncoding().GetBytes(policy)));
            }
            else
            {
                buffer.MarkReaderIndex();
                int length = buffer.ReadInt();

                if (buffer.ReadableBytes < length)
                {
                    buffer.ResetReaderIndex();
                    return;
                }

                if (length < 0)
                {
                    return;
                }

                var messageBuffer = buffer.ReadBytes(length);
                output.Add(new Request(length, messageBuffer.ReadShort(), messageBuffer));
            }
        }
    }
}