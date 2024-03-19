using System;
using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.ThirdParty;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Services
{
    public class PurchasesService : IInitializable, IPostStartable, IDisposable
    {
        [Inject]
        private readonly PurchasesData _purchasesData;

        [Inject]
        private readonly SaveModel _saveModel;

        [Inject]
        private readonly UnlockContentModel _unlockContentModel;

        [Inject]
        private readonly PurchasesModel _purchasesModel;

        [Inject]
        private readonly DummySdk _sdk;

        public void Initialize()
        {
            _sdk.onSuccessPurchase += HandlePurchase;
            _sdk.onUnhandledPurchase += HandlePurchase;

            _sdk.onErrorPurchase += _purchasesModel.OnEndedPurchasing;
            _sdk.onSuccessPurchase += _purchasesModel.OnEndedPurchasing;
        }

        public void PostStart()
        {
            _sdk.CheckAnyUnhandledPurchases();
        }

        public void Dispose()
        {
            _sdk.onSuccessPurchase -= HandlePurchase;
            _sdk.onUnhandledPurchase -= HandlePurchase;

            _sdk.onErrorPurchase -= _purchasesModel.OnEndedPurchasing;
            _sdk.onSuccessPurchase -= _purchasesModel.OnEndedPurchasing;
        }

        public void PurchaseAll()
        {
            _purchasesModel.OnStartedPurchasing();
            _sdk.PurchaseConsumableItem(_purchasesData.PurchaseAllId, "");
        }

        public void PurchaseCategory(string categoryId)
        {
            _purchasesModel.OnStartedPurchasing();
            _sdk.PurchaseConsumableItem(_purchasesData.PurchaseCategoryId, categoryId);
        }

        private void HandlePurchase(string id, string additionalData)
        {
            var purchaseInfo = new PurchaseInfo(id, additionalData);

            if (id == _purchasesData.PurchaseAllId)
            {
                if (_saveModel.CurrentSaveData.UnlockedAll)
                {
                    return;
                }

                _sdk.HideBannerAd();
                _unlockContentModel.OnOpenAll();

                return;
            }

            if (id == _purchasesData.PurchaseCategoryId)
            {
                if (_saveModel.CurrentSaveData.Purchases.Contains(purchaseInfo))
                {
                    _sdk.HandleConsumableItem(id, additionalData);
                    return;
                }

                _saveModel.AddPurchase(purchaseInfo);
                _saveModel.InvokeSave();

                _sdk.HandleConsumableItem(id, additionalData);

                _unlockContentModel.OnOpenCategory(additionalData);
            }
        }
    }
}