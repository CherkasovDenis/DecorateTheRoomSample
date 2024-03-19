using System;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class TrashPresenter : IInitializable, IDisposable
    {
        [Inject]
        private readonly RoomModel _roomModel;

        [Inject]
        private readonly Switch _trashToggleView;

        [Inject]
        private readonly SoundService _soundService;

        public void Initialize()
        {
            _trashToggleView.Initialized += TrashToggleChanged;
            _trashToggleView.ChangedValue += TrashToggleChangedWithSound;
        }

        public void Dispose()
        {
            _trashToggleView.Initialized -= TrashToggleChanged;
            _trashToggleView.ChangedValue -= TrashToggleChangedWithSound;
        }

        private void TrashToggleChanged(bool status) => TrashToggleChanged(status, false);

        private void TrashToggleChangedWithSound(bool status) => TrashToggleChanged(status, true);

        private void TrashToggleChanged(bool status, bool playSound)
        {
            if (status == _roomModel.TrashEnabled)
            {
                return;
            }

            if (playSound)
            {
                _soundService.PlayClickSound();
            }

            _roomModel.SetTrashStatus(status);
        }
    }
}