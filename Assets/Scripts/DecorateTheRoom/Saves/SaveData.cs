using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Saves
{
    [Serializable]
    public class SaveData
    {
        public Dictionary<string, MenuItemState> MenuItemStates { get; private set; } =
            new Dictionary<string, MenuItemState>();

        public List<PurchaseInfo> Purchases => _purchases;

        public bool UnlockedAll => _unlockedAll;

        [SerializeField]
        private List<MenuItemState> _menuItemStates = new List<MenuItemState>();

        [SerializeField]
        private List<PurchaseInfo> _purchases = new List<PurchaseInfo>();

        [SerializeField]
        private bool _unlockedAll;

        public void Initialize()
        {
            MenuItemStates = _menuItemStates.ToDictionary(x => x.Id);
        }

        public void CacheDataToList()
        {
            _menuItemStates.Clear();

            foreach (var lockedMenuItem in MenuItemStates)
            {
                _menuItemStates.Add(lockedMenuItem.Value);
            }
        }

        public void SetUnlockedAll(bool value)
        {
            _unlockedAll = value;
        }
    }
}