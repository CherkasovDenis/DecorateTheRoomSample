using System;
using System.Collections.Generic;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Ui.Models
{
    [UsedImplicitly]
    public class CategoriesModel
    {
        public event Action<string> ChangedCategory;

        public List<CategoryToggle> CategoryToggles { get; } = new List<CategoryToggle>();

        public CategoryInfo CurrentCategory => CategoryInfos[CurrentCategoryId];

        public string CurrentCategoryId { get; private set; }

        public Dictionary<string, CategoryInfo> CategoryInfos { get; } = new Dictionary<string, CategoryInfo>();

        public void OnChangedCategory(string categoryId)
        {
            CurrentCategoryId = categoryId;
            ChangedCategory?.Invoke(categoryId);
        }
    }

    public class CategoryInfo
    {
        public string CategoryId { get; }

        public bool UnlockByAd { get; }

        public bool UnlockByPurchase { get; }

        public List<string> ItemIds { get; }

        public ContentView ContentView { get; }

        public CategoryInfo(string categoryId, bool unlockByAd, bool unlockByPurchase, List<string> itemIds,
            ContentView contentView)
        {
            CategoryId = categoryId;
            UnlockByAd = unlockByAd;
            UnlockByPurchase = unlockByPurchase;
            ItemIds = itemIds;
            ContentView = contentView;
        }
    }
}