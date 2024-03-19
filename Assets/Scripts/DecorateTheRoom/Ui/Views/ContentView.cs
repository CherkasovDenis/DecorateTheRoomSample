using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Ui.Views
{
    public class ContentView : MonoBehaviour
    {
        public RectTransform RectTransform => _rectTransform;

        public bool Hided { get; private set; }

        [SerializeField]
        private RectTransform _rectTransform;

        public void Show()
        {
            if (!Hided)
            {
                return;
            }

            gameObject.SetActive(true);

            Hided = false;
        }

        public void Hide()
        {
            if (Hided)
            {
                return;
            }

            gameObject.SetActive(false);

            Hided = true;
        }
    }
}