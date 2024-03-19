using System;
using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class MenuWindowPresenter : IInitializable, IDisposable
    {
        [Inject]
        private readonly VisualData _visualData;

        [Inject]
        private readonly MenuWindowModel _menuWindowModel;

        [Inject]
        private readonly MenuView _menuView;

        [Inject]
        private readonly Switch _menuButton;

        [Inject]
        private readonly SoundService _soundService;

        public void Initialize()
        {
            _menuButton.Initialized += ChangeMenuWindowState;
            _menuButton.ChangedValue += ChangeMenuWindowStateWithSound;
            _menuWindowModel.ChangedState += ChangeMenuWindowVisibility;
        }

        public void Dispose()
        {
            _menuButton.Initialized -= ChangeMenuWindowState;
            _menuButton.ChangedValue -= ChangeMenuWindowStateWithSound;
            _menuWindowModel.ChangedState -= ChangeMenuWindowVisibility;
        }

        private void ChangeMenuWindowStateWithSound(bool status)
        {
            _soundService.PlayClickSound();
            ChangeMenuWindowState(status);
        }

        private void ChangeMenuWindowState(bool status)
        {
            _menuWindowModel.IsShowing = status;
        }

        private void ChangeMenuWindowVisibility()
        {
            _menuView.Move(
                _menuWindowModel.IsShowing
                    ? _visualData.MenuShowValue
                    : _visualData.MenuHideValue,
                _visualData.MenuMoveDuration);
        }
    }
}