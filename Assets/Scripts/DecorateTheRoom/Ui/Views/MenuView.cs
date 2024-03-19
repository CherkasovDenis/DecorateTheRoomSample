using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace BlackKakadu.DecorateTheRoom.Ui.Views
{
    public class MenuView : MonoBehaviour
    {
        public Button NextCategoryPageButton => _nextCategoryPageButton;

        public Button PreviousCategoryPageButton => _previousCategoryPageButton;

        public Transform CategoryPageSpawnParent => _categoryPageSpawnParent;

        public ScrollRect Scroll => _scroll;

        public Transform MenuItemsSpawnParent => _menuItemsSpawnParent;

        public GameObject UnlockCategoryButtonsParent => _unlockCategoryButtonsParent;

        public Button UnlockByAdButton => _unlockByAdButton;

        public Button UnlockByPurchaseButton => _unlockByPurchaseButton;

        public Button UnlockAllButton => _unlockAllButton;

        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private Button _nextCategoryPageButton;

        [SerializeField]
        private Button _previousCategoryPageButton;

        [SerializeField]
        private Transform _categoryPageSpawnParent;

        [SerializeField]
        private ScrollRect _scroll;

        [SerializeField]
        private Transform _menuItemsSpawnParent;

        [SerializeField]
        private TMP_Text _pageNumberText;

        [SerializeField]
        private GameObject _unlockCategoryButtonsParent;

        [SerializeField]
        private Button _unlockByAdButton;

        [SerializeField]
        private Button _unlockByPurchaseButton;

        [SerializeField]
        private Button _unlockAllButton;

        private Tween _moveTween;

        public void Move(float endValue, float duration)
        {
            if (_moveTween != null)
            {
                _moveTween.Kill();
            }

            _moveTween = _rectTransform.DOAnchorPosX(endValue, duration);
        }

        public void SetPageNumber(int number, int total)
        {
            _pageNumberText.text = $"{number}/{total}";
        }

        public void ShowNextButton() => ShowButton(_nextCategoryPageButton);

        public void HideNextButton() => HideButton(_nextCategoryPageButton);

        public void ShowPreviousButton() => ShowButton(_previousCategoryPageButton);

        public void HidePreviousButton() => HideButton(_previousCategoryPageButton);

        private void ShowButton(Button button)
        {
            button.gameObject.SetActive(true);
        }

        private void HideButton(Button button)
        {
            button.gameObject.SetActive(false);
        }
    }
}