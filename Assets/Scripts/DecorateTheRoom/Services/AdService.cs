using System;
using System.Threading;
using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.ThirdParty;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Services
{
    public class AdService : IInitializable, IDisposable
    {
        [Inject]
        private readonly AdData _adData;

        [Inject]
        private readonly AdPauseModel _adPauseModel;

        [Inject]
        private readonly SaveModel _saveModel;

        [Inject]
        private readonly UnlockContentModel _unlockContentModel;

        [Inject]
        private readonly PurchasesModel _purchasesModel;

        [Inject]
        private readonly DummySdk _sdk;

        private bool _rewarded;

        private CancellationTokenSource _adPauseCts;

        public void Initialize()
        {
            _sdk.onInterstitialShown += AutoRestartAdPause;

            _sdk.onRewardedAdReward += HandleReward;
            _sdk.onRewardedAdError += HandleRewardedError;
            _sdk.onRewardedAdClosed += HandleRewardedClosed;

            _purchasesModel.StartedPurchasing += CancelAdPauseTimer;
            _purchasesModel.EndedPurchasing += RestartAdPause;

            _unlockContentModel.UnlockedAll += CancelAdPauseTimer;
        }

        public void Dispose()
        {
            _sdk.onInterstitialShown -= AutoRestartAdPause;

            _sdk.onRewardedAdReward -= HandleReward;
            _sdk.onRewardedAdError -= HandleRewardedError;
            _sdk.onRewardedAdClosed -= HandleRewardedClosed;

            _purchasesModel.StartedPurchasing -= CancelAdPauseTimer;
            _purchasesModel.EndedPurchasing -= RestartAdPause;

            _unlockContentModel.UnlockedAll -= CancelAdPauseTimer;
        }

        public void ShowBanner()
        {
            _sdk.ShowBannerAd();
        }

        public void RestartAdPause()
        {
            if (_saveModel.CurrentSaveData.UnlockedAll)
            {
                CancelAdPauseTimer();
                return;
            }

            ShowAdPauseAsync().Forget();
        }

        public void ShowRewardedAd(string categoryId)
        {
            CancelAdPauseTimer();
            _sdk.ShowRewardedAd(categoryId);
        }

        private void HandleReward(string categoryId)
        {
            _rewarded = true;
        }

        private void HandleRewardedError(string categoryId)
        {
            ResetRewardedStatus();
        }

        private void HandleRewardedClosed(string categoryId)
        {
            if (_rewarded)
            {
                _unlockContentModel.OnOpenCategory(categoryId);
            }

            ResetRewardedStatus();

            RestartAdPause();
        }

        private void ResetRewardedStatus()
        {
            _rewarded = false;
        }

        private void AutoRestartAdPause()
        {
            _adPauseModel.OnInterstitialShown();

            RestartAdPause();
        }

        private async UniTaskVoid ShowAdPauseAsync()
        {
            CancelAdPauseTimer();

            _adPauseCts = new CancellationTokenSource();

            var isCancelled = await UniTask.Delay(TimeSpan.FromSeconds(_adData.InterstitialAdFrequencyTime),
                    cancellationToken: _adPauseCts.Token)
                .SuppressCancellationThrow();

            if (isCancelled)
            {
                return;
            }

            for (var secondsTillShow = _adData.InterstitialAdPauseDuration; secondsTillShow >= 0; secondsTillShow--)
            {
                if (_saveModel.CurrentSaveData.UnlockedAll)
                {
                    return;
                }

                _adPauseModel.OnStartShowingAdPause(secondsTillShow);
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }

            _sdk.ShowInterstitialAd();
        }

        private void CancelAdPauseTimer()
        {
            if (_adPauseCts == null)
            {
                return;
            }

            _adPauseCts.Cancel();
            _adPauseCts.Dispose();
            _adPauseCts = null;
        }
    }
}