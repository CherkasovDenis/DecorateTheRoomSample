using System;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Models
{
    [UsedImplicitly]
    public class AdPauseModel
    {
        public event Action<int> StartShowingAdPause;

        public event Action InterstitialShown;

        public bool IsShowing { get; set; }

        public void OnStartShowingAdPause(int secondsTillShow)
        {
            StartShowingAdPause?.Invoke(secondsTillShow);
        }

        public void OnInterstitialShown()
        {
            InterstitialShown?.Invoke();
        }
    }
}