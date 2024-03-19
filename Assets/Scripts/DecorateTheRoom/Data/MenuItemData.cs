using Sirenix.OdinInspector;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Data
{
    public abstract class MenuItemData : ScriptableObject
    {
        [ShowInInspector]
        public string ItemId => _itemId;

        [ShowInInspector]
        public bool LockedAtStart => _lockedAtStart;

        public Sprite Sprite => _sprite;

        [SerializeField]
        [HideInInlineEditors]
        protected string _itemId;

        [SerializeField]
        [HideInInlineEditors]
        protected bool _lockedAtStart;

        [SerializeField]
        [HideInInlineEditors]
        protected Sprite _sprite;

#if UNITY_EDITOR
        public void Initialize(Sprite sprite, string id)
        {
            _sprite = sprite;
            _itemId = id;
        }
#endif
    }
}