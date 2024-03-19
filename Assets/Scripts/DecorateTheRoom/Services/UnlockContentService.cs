using System;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Services
{
    public class UnlockContentService : IInitializable, IDisposable
    {
        [Inject]
        private readonly UnlockContentModel _unlockContentModel;

        [Inject]
        private readonly SaveModel _saveModel;

        [Inject]
        private readonly CategoriesModel _categoriesModel;

        public void Initialize()
        {
            _unlockContentModel.OpenAll += OpenAll;
            _unlockContentModel.OpenCategory += OpenCategory;
        }

        public void Dispose()
        {
            _unlockContentModel.OpenAll -= OpenAll;
            _unlockContentModel.OpenCategory -= OpenCategory;
        }

        private void OpenAll()
        {
            _saveModel.CurrentSaveData.SetUnlockedAll(true);
            _saveModel.InvokeSave();

            _unlockContentModel.OnUnlockedAll();
        }

        private void OpenCategory(string categoryId)
        {
            foreach (var itemId in _categoriesModel.CategoryInfos[categoryId].ItemIds)
            {
                _saveModel.CurrentSaveData.MenuItemStates[itemId].Locked = false;
            }

            _saveModel.InvokeSave();

            _unlockContentModel.OnUnlockedCategory(categoryId);
        }
    }
}