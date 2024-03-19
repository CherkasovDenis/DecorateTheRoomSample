using TMPro;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Ui.Views
{
    public class AdPauseView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _adPauseCover;

        [SerializeField]
        private TMP_Text _showAdTimerText;

        public void Show()
        {
            _adPauseCover.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _adPauseCover.gameObject.SetActive(false);
        }

        public void UpdateAdPauseTimer(int secondsTillShow)
        {
            _showAdTimerText.text = $"{secondsTillShow}...";
        }
    }
}