using System;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Saves
{
    [UsedImplicitly]
    public class SaveModel
    {
        public event Action Save;

        public SaveData CurrentSaveData { get; private set; }

        public void InitializeSaveData(SaveData saveData)
        {
            CurrentSaveData = saveData;
            CurrentSaveData.Initialize();
        }

        public void AddPurchase(PurchaseInfo purchaseInfo)
        {
            if (CurrentSaveData.Purchases.Contains(purchaseInfo))
            {
                return;
            }

            CurrentSaveData.Purchases.Add(purchaseInfo);
        }

        public void InvokeSave()
        {
            Save?.Invoke();
        }
    }
}