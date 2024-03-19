using System;
using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.ThirdParty;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Services
{
    public class SaveService : IInitializable, IDisposable
    {
        [Inject]
        private readonly SaveModel _saveModel;

        [Inject]
        private readonly GameData _gameData;

        [Inject]
        private readonly DummySdk _sdk;

        public void Initialize()
        {
            _saveModel.Save += Save;

            ValidateSaveData();
        }

        public void Dispose()
        {
            _saveModel.Save -= Save;
        }

        private void ValidateSaveData()
        {
            var changedSaveData = false;

            _saveModel.InitializeSaveData(_sdk.GetSaveData<SaveData>());

            var saveData = _saveModel.CurrentSaveData;

            foreach (var category in _gameData.Categories)
            {
                foreach (var group in category.Groups)
                {
                    foreach (var menuItem in group.MenuItems)
                    {
                        var menuItemKey = menuItem.ItemId;

                        if (!saveData.MenuItemStates.TryGetValue(menuItemKey, out var menuItemState))
                        {
                            menuItemState = new MenuItemState(menuItemKey, menuItem.LockedAtStart);

                            saveData.MenuItemStates.Add(menuItemKey, menuItemState);

                            changedSaveData = true;
                        }
                    }
                }
            }

            if (changedSaveData)
            {
                Save();
            }
        }

        private void Save()
        {
            _saveModel.CurrentSaveData.CacheDataToList();
            _sdk.SaveData(_saveModel.CurrentSaveData);
        }
    }
}