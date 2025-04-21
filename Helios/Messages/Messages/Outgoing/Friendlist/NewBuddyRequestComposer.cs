using Helios.Game;
using Helios.Storage.Models.Avatar;

namespace Helios.Messages.Outgoing
{
    class NewBuddyRequestComposer : IMessageComposer
    {
        private AvatarData _avatarData;

        public NewBuddyRequestComposer(AvatarData avatarData)
        {
            _avatarData = avatarData;
        }

        public override void Write()
        {
            _data.Add(_avatarData.Id);
            _data.Add(_avatarData.Name);
            _data.Add(_avatarData.Figure);
        }


        public override int HeaderId => 132;
    }
}