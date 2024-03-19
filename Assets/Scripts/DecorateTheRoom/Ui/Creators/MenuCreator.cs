using System.Collections.Generic;
using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using UnityEngine;
using VContainer;

namespace BlackKakadu.DecorateTheRoom.Ui.Creators
{
    public class MenuCreator
    {
        [Inject]
        private readonly GameData _gameData;

        [Inject]
        private readonly VisualData _visualData;

        [Inject]
        private readonly MenuView _menuView;

        [Inject]
        private readonly MenuButtonsModel _menuButtonsModel;

        [Inject]
        private readonly PagesModel _pagesModel;

        [Inject]
        private readonly CategoriesModel _categoriesModel;

        [Inject]
        private readonly SaveModel _saveModel;

        public void Create()
        {
            var saveData = _saveModel.CurrentSaveData;
            var unlockedAll = saveData.UnlockedAll;

            var currentCategoriesAtPage = 0;

            var page = CreatePage();

            foreach (var category in _gameData.Categories)
            {
                CreateCategoryToggle(ref currentCategoriesAtPage, ref page, category);

                var contentView = Object.Instantiate(_visualData.ElementsContentPrefab,
                    _menuView.MenuItemsSpawnParent, false);

                var categoryItemIds = new List<string>();

                foreach (var group in category.Groups)
                {
                    var menuGroupGrid = Object.Instantiate(_visualData.MenuGroupGridPrefab,
                        contentView.transform, false);

                    menuGroupGrid.cellSize = group.CellSize;

                    foreach (var menuItem in group.MenuItems)
                    {
                        var menuButton = Object.Instantiate(_visualData.MenuButtonPrefab,
                            menuGroupGrid.transform, false);

                        var menuItemKey = menuItem.ItemId;

                        if (!saveData.MenuItemStates.TryGetValue(menuItemKey, out var menuItemState))
                        {
                            Debug.LogError($"No valid item with key {menuItemKey} in save data!");
                            return;
                        }

                        menuButton.Initialize(menuItem, !unlockedAll && menuItemState.Locked);

                        categoryItemIds.Add(menuItemKey);

                        _menuButtonsModel.Add(menuItemKey, menuButton);
                    }
                }

                _categoriesModel.CategoryInfos.Add(category.CategoryId,
                    new CategoryInfo(category.CategoryId, category.UnlockByAd,
                        category.UnlockByPurchase, categoryItemIds, contentView));
            }
        }

        private PageView CreatePage()
        {
            var page = Object.Instantiate(_visualData.PagePrefab, _menuView.CategoryPageSpawnParent, false);

            page.Hide();

            _pagesModel.Pages.Add(page);

            return page;
        }

        private void CreateCategoryToggle(ref int currentCategoriesAtPage, ref PageView page,
            CategoryData category)
        {
            if (++currentCategoriesAtPage > _visualData.CategoriesPerPage)
            {
                page = CreatePage();
                currentCategoriesAtPage = 1;
            }

            var categoryToggle = Object.Instantiate(_visualData.CategoryPrefab, page.transform, false);

            categoryToggle.Initialize(page.ToggleGroup, category.CategoryIcon, category.CategoryId);

            _categoriesModel.CategoryToggles.Add(categoryToggle);
        }
    }
}