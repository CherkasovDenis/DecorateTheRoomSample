using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.Ui.Creators;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom
{
    public class GameEntryPoint : IPostInitializable, IPostStartable
    {
        [Inject]
        private readonly SaveModel _saveModel;

        [Inject]
        private readonly PagesModel _pagesModel;

        [Inject]
        private readonly MenuCreator _menuCreator;

        [Inject]
        private readonly AdService _adService;

        [Inject]
        private readonly SoundService _soundService;

        public void PostInitialize()
        {
            _soundService.SfxEnabled = false;

            if (!_saveModel.CurrentSaveData.UnlockedAll)
            {
                _adService.ShowBanner();
                _adService.RestartAdPause();
            }

            _menuCreator.Create();
        }

        public void PostStart()
        {
            _pagesModel.Initialize();
            _pagesModel.CurrentPage = 0;
            _soundService.SfxEnabled = true;
        }
    }
}