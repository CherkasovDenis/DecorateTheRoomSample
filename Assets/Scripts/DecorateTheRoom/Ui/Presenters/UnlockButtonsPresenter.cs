using System;
using System.Linq;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class UnlockButtonsPresenter : IStartable, IDisposable
    {
        [Inject]
        private readonly MenuView _menuView;

        [Inject]
        private readonly CategoriesModel _categoriesModel;

        [Inject]
        private readonly SaveModel _saveModel;

        [Inject]
        private readonly UnlockContentModel _unlockContentModel;

        [Inject]
        private readonly PurchasesService _purchasesService;

        [Inject]
        private readonly AdService _adService;

        public void Start()
        {
            _categoriesModel.ChangedCategory += UpdateButtonsVisible;

            _unlockContentModel.UnlockedAll += UpdateButtonsVisible;
            _unlockContentModel.UnlockedCategory += UpdateButtonsVisible;

            _menuView.UnlockAllButton.onClick.AddListener(PurchaseAll);
            _menuView.UnlockByPurchaseButton.onClick.AddListener(PurchaseCurrentCategory);
            _menuView.UnlockByAdButton.onClick.AddListener(ShowRewardedAdCurrentCategory);
        }

        public void Dispose()
        {
            _categoriesModel.ChangedCategory -= UpdateButtonsVisible;

            _unlockContentModel.UnlockedAll -= UpdateButtonsVisible;
            _unlockContentModel.UnlockedCategory -= UpdateButtonsVisible;

            _menuView.UnlockAllButton.onClick.RemoveListener(PurchaseAll);
            _menuView.UnlockByPurchaseButton.onClick.RemoveListener(PurchaseCurrentCategory);
            _menuView.UnlockByAdButton.onClick.RemoveListener(ShowRewardedAdCurrentCategory);
        }

        private void UpdateButtonsVisible(string _) => UpdateButtonsVisible();

        private void UpdateButtonsVisible()
        {
            if (_saveModel.CurrentSaveData.UnlockedAll)
            {
                _menuView.UnlockCategoryButtonsParent.gameObject.SetActive(false);
                _menuView.UnlockAllButton.gameObject.SetActive(false);
                return;
            }

            _menuView.UnlockAllButton.gameObject.SetActive(true);

            var categoryInfo = _categoriesModel.CurrentCategory;

            var itemIds = categoryInfo.ItemIds;

            var hasLockedItems = itemIds.Any(itemId => _saveModel.CurrentSaveData.MenuItemStates[itemId].Locked);

            var showAdButton = hasLockedItems && categoryInfo.UnlockByAd;
            var showPurchaseButton = hasLockedItems && categoryInfo.UnlockByPurchase;

            _menuView.UnlockCategoryButtonsParent.gameObject.SetActive(showAdButton || showPurchaseButton);
            _menuView.UnlockByAdButton.gameObject.SetActive(showAdButton);
            _menuView.UnlockByPurchaseButton.gameObject.SetActive(showPurchaseButton);
        }

        private void PurchaseAll()
        {
            _purchasesService.PurchaseAll();
        }

        private void PurchaseCurrentCategory()
        {
            _purchasesService.PurchaseCategory(_categoriesModel.CurrentCategory.CategoryId);
        }

        private void ShowRewardedAdCurrentCategory()
        {
            _adService.ShowRewardedAd(_categoriesModel.CurrentCategory.CategoryId);
        }
    }
}