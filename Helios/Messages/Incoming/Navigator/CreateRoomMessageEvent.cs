using Helios.Game;
using Helios.Network.Streams;
using System.Linq;
using Helios.Util.Extensions;
using Helios.Messages.Outgoing;
using Helios.Storage.Access;
using Helios.Storage.Models.Room;
using Helios.Storage;

namespace Helios.Messages.Incoming
{
    public class CreateRoomMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            //  CreateRoomMessageEvent: 9 / [0][4]test[0][7]model_t
            string name = request.ReadString().FilterInput(true);
            string model = request.ReadString();

            var roomModel = RoomManager.Instance.RoomModels.FirstOrDefault(x => x.Data.Model == model);

            if (roomModel == null)
                return;

            string modelType = roomModel.Data.Model.Replace("model_", "");

            if (modelType != "a" &&
                    modelType != "b" &&
                    modelType != "c" &&
                    modelType != "d" &&
                    modelType != "e" &&
                    modelType != "f" &&
                    modelType != "i" &&
                    modelType != "j" &&
                    modelType != "k" &&
                    modelType != "l" &&
                    modelType != "m" &&
                    modelType != "n" &&
                    !avatar.IsSubscribed)
            {
                return; // Fuck off, scripter.
            }

            RoomData roomData = new RoomData
            {
                OwnerId = avatar.Details.Id,
                Name = name,
                ModelId = roomModel.Data.Id,
                Description = string.Empty
            };

            using (var context = new StorageContext())
            {
                context.NewRoom(roomData);
            }

            avatar.Send(new FlatCreatedComposer(roomData.Id, roomData.Name));
        }

        public int HeaderId => -1;
    }
}
