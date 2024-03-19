using Sirenix.OdinInspector;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Data
{
    [CreateAssetMenu(fileName = "New Decor Element", menuName = "BlackKakadu/DecorateTheRoom/DecorElementData")]
    public class DecorElementData : MenuItemData
    {
        public Vector3 Scale => _scale;

        [SerializeField, HideInInlineEditors]
        private Vector3 _scale = Vector3.one;
    }
}