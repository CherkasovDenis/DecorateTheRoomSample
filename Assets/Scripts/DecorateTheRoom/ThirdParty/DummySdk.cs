using System;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.ThirdParty
{
    public class DummySdk
    {
        public event Action onInterstitialShown;
        public event Action<string> onRewardedAdReward;
        public event Action<string> onRewardedAdError;
        public event Action<string> onRewardedAdClosed;
        public event Action<string, string> onSuccessPurchase;
        public event Action<string, string> onUnhandledPurchase;
        public event Action<string, string, string> onErrorPurchase;

        public void ShowBannerAd()
        {
            Debug.Log("ShowBannerAd");
        }

        public void HideBannerAd()
        {
            Debug.Log("HideBannerAd");
        }

        public void ShowRewardedAd(string categoryId)
        {
            onRewardedAdReward?.Invoke(categoryId);
            onRewardedAdClosed?.Invoke(categoryId);
        }

        public void ShowInterstitialAd()
        {
            onInterstitialShown?.Invoke();
        }

        public void PurchaseConsumableItem(string id, string additionalData)
        {
            onSuccessPurchase?.Invoke(id, additionalData);
        }

        public void HandleConsumableItem(string id, string additionalData)
        {
            Debug.Log($"HandleConsumableItem {id} {additionalData}");
        }

        public void CheckAnyUnhandledPurchases()
        {
            Debug.Log("CheckAnyUnhandledPurchases");
        }

        public void OpenMoreGamesUrl()
        {
            Debug.Log("OpenMoreGamesUrl");
        }

        public T GetSaveData<T>() where T : new()
        {
            return PlayerPrefs.HasKey("Save")
                ? JsonUtility.FromJson<T>(PlayerPrefs.GetString("Save"))
                : new T();
        }

        public void SaveData<T>(T saveData) where T : new()
        {
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(saveData, true));
        }
    }
}