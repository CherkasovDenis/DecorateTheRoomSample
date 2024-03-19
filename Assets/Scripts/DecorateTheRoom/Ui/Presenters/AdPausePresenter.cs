using System;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class AdPausePresenter : IInitializable, IDisposable
    {
        [Inject]
        private readonly AdPauseModel _adPauseModel;

        [Inject]
        private readonly AdPauseView _adPauseView;

        public void Initialize()
        {
            _adPauseModel.StartShowingAdPause += ChangeAdPauseTimeCounter;
            _adPauseModel.InterstitialShown += Hide;

            _adPauseModel.IsShowing = false;
            _adPauseView.Hide();
        }

        public void Dispose()
        {
            _adPauseModel.StartShowingAdPause -= ChangeAdPauseTimeCounter;
            _adPauseModel.InterstitialShown -= Hide;
        }

        private void ChangeAdPauseTimeCounter(int secondsTillShow)
        {
            if (!_adPauseModel.IsShowing)
            {
                _adPauseView.Show();
                _adPauseModel.IsShowing = true;
            }

            _adPauseView.UpdateAdPauseTimer(secondsTillShow);
        }

        private void Hide()
        {
            _adPauseModel.IsShowing = false;
            _adPauseView.Hide();
        }
    }
}