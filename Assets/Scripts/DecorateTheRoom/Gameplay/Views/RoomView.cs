using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Views
{
    public class RoomView : MonoBehaviour
    {
        public SpriteRenderer Background => _background;

        [SerializeField]
        private SpriteRenderer _background;

        public void SetBackground(Sprite sprite)
        {
            _background.sprite = sprite;
        }
    }
}