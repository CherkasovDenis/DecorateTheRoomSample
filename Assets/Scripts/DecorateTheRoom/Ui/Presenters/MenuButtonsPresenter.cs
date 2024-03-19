using System;
using System.Linq;
using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Gameplay.Views;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class MenuButtonsPresenter : IStartable, IDisposable
    {
        [Inject]
        private readonly RoomModel _roomModel;

        [Inject]
        private readonly UndoModel _undoModel;

        [Inject]
        private readonly MenuButtonsModel _menuButtonsModel;

        [Inject]
        private readonly Func<DecorElementData, int, DecorView> _decorFactory;

        [Inject]
        private readonly SaveModel _saveModel;

        [Inject]
        private readonly UnlockContentModel _unlockContentModel;

        [Inject]
        private readonly CategoriesModel _categoriesModel;

        [Inject]
        private readonly SoundService _soundService;

        public void Start()
        {
            foreach (var menuButton in _menuButtonsModel.MenuButtons)
            {
                menuButton.Value.Clicked += ClickedOnMenuButton;
            }

            _unlockContentModel.UnlockedCategory += UnlockItemsInCategory;
            _unlockContentModel.UnlockedAll += UnlockAllItems;
        }

        public void Dispose()
        {
            foreach (var menuButton in _menuButtonsModel.MenuButtons)
            {
                menuButton.Value.Clicked -= ClickedOnMenuButton;
            }

            _unlockContentModel.UnlockedCategory -= UnlockItemsInCategory;
            _unlockContentModel.UnlockedAll -= UnlockAllItems;
        }

        private void ClickedOnMenuButton(MenuItemData menuItemData)
        {
            var menuItemKey = menuItemData.ItemId;

            if (!_saveModel.CurrentSaveData.UnlockedAll &&
                _saveModel.CurrentSaveData.MenuItemStates[menuItemKey].Locked)
            {
                return;
            }

            switch (menuItemData)
            {
                case DecorElementData decorElementData:
                    var decorView = _decorFactory(decorElementData, _roomModel.GetFrontSortingOrder());
                    _undoModel.Add(() => _roomModel.RemoveDecor(decorView));

                    _roomModel.AddDecor(decorView);
                    _roomModel.ReorderDecors();
                    break;
                case RoomBackgroundData roomBackgroundData:
                    var previousBackground = _roomModel.CurrentBackground;
                    _undoModel.Add(() => _roomModel.SetBackground(previousBackground));

                    _roomModel.SetBackground(roomBackgroundData.Sprite);
                    break;
            }

            _soundService.PlayClickSound();
        }

        private void UnlockAllItems()
        {
            foreach (var categoryInfo in _categoriesModel.CategoryInfos.Values)
            {
                foreach (var itemId in categoryInfo.ItemIds)
                {
                    _menuButtonsModel.MenuButtons[itemId].Unlock();
                }
            }
        }

        private void UnlockItemsInCategory(string categoryId)
        {
            var itemsInCategory = _categoriesModel.CategoryInfos[categoryId].ItemIds;

            foreach (var itemId in itemsInCategory.Where(itemId =>
                         !_saveModel.CurrentSaveData.MenuItemStates[itemId].Locked))
            {
                _menuButtonsModel.MenuButtons[itemId].Unlock();
            }
        }
    }
}