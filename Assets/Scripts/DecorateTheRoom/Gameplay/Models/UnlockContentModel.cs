using System;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Models
{
    [UsedImplicitly]
    public class UnlockContentModel
    {
        public event Action OpenAll;

        public event Action<string> OpenCategory;

        public event Action UnlockedAll;

        public event Action<string> UnlockedCategory;

        public void OnOpenAll()
        {
            OpenAll?.Invoke();
        }

        public void OnOpenCategory(string categoryId)
        {
            OpenCategory?.Invoke(categoryId);
        }

        public void OnUnlockedAll()
        {
            UnlockedAll?.Invoke();
        }

        public void OnUnlockedCategory(string categoryId)
        {
            UnlockedCategory?.Invoke(categoryId);
        }
    }
}