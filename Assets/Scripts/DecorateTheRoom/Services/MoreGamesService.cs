using System;
using BlackKakadu.DecorateTheRoom.ThirdParty;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Services
{
    public class MoreGamesService : IInitializable, IDisposable
    {
        [Inject]
        private readonly Button _moreGamesButton;

        [Inject]
        private readonly DummySdk _sdk;

        public void Initialize()
        {
            _moreGamesButton.onClick.AddListener(OpenMoreGamesUrl);
        }

        public void Dispose()
        {
            _moreGamesButton.onClick.RemoveListener(OpenMoreGamesUrl);
        }

        private void OpenMoreGamesUrl()
        {
            _sdk.OpenMoreGamesUrl();
        }
    }
}