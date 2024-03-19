using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Data
{
    [CreateAssetMenu(fileName = "CategoryData", menuName = "BlackKakadu/DecorateTheRoom/CategoryData")]
    public class CategoryData : ScriptableObject
    {
        public string CategoryId => _categoryId;

        public Sprite CategoryIcon => _categoryIcon;

        public bool UnlockByAd => _unlockByAd;

        public bool UnlockByPurchase => _unlockByPurchase;

        public List<Group> Groups => _groups;

        [SerializeField]
        private string _categoryId;

        [SerializeField]
        private Sprite _categoryIcon;

        [SerializeField]
        private bool _unlockByAd;

        [SerializeField]
        private bool _unlockByPurchase;

        [SerializeField]
        private List<Group> _groups = new List<Group>();
    }

    [Serializable]
    public class Group
    {
        public Vector2 CellSize => _cellSize;

        public List<MenuItemData> MenuItems => _menuItems;

        [SerializeField]
        private Vector2 _cellSize = new Vector2(100, 100);

        [SerializeField, InlineEditor(Expanded = true)]
        private List<MenuItemData> _menuItems = new List<MenuItemData>();
    }
}