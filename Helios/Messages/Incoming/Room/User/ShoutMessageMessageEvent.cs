using Helios.Util.Extensions;
using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class ShoutMessageMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            string message = request.ReadString().FilterInput(true);
            int colourId = request.ReadInt();

            if (colourId >= 24)//HabboHotel.ColourChatCrash)
                return;

            if (colourId == 1 || colourId == 2)
                return;

            /*if (colourId == 23 && !session.getHabbo().hasFuse("fuse_mod"))
            {
                return;
            }*/

            player.RoomUser.Talk(ChatMessageType.SHOUT, message, colourId);

        }
    }
}
