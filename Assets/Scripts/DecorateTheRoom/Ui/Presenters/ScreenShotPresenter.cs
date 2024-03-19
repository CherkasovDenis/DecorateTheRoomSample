using System;
using BlackKakadu.DecorateTheRoom.Services;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class ScreenShotPresenter : IInitializable, IDisposable
    {
        [Inject]
        private readonly Button _screenshotButton;

        [Inject]
        private readonly ScreenshotService _screenshotService;

        [Inject]
        private readonly SoundService _soundService;

        public void Initialize()
        {
            _screenshotButton.onClick.AddListener(TakeScreenshot);
        }

        public void Dispose()
        {
            _screenshotButton.onClick.RemoveListener(TakeScreenshot);
        }

        private void TakeScreenshot()
        {
            _soundService.PlayScreenshotSound();
            _screenshotService.TakeScreenshot();
        }
    }
}