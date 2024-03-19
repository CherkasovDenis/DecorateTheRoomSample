using System;
using BlackKakadu.DecorateTheRoom.Data;
using UnityEngine;
using UnityEngine.UI;

namespace BlackKakadu.DecorateTheRoom.Ui.Views
{
    public class MenuButton : MonoBehaviour
    {
        public event Action<MenuItemData> Clicked;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Image _menuItemIcon;

        [SerializeField]
        private GameObject _lock;

        private MenuItemData _menuItemData;

        private bool _locked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Initialize(MenuItemData menuItemData, bool locked)
        {
            _menuItemIcon.sprite = menuItemData.Sprite;
            _menuItemData = menuItemData;

            _locked = locked;
            ApplyLockedStatus();
        }

        private void OnClicked()
        {
            Clicked?.Invoke(_menuItemData);
        }

        public void Unlock()
        {
            if (!_locked)
            {
                return;
            }

            _locked = false;

            ApplyLockedStatus();
        }

        public void Lock()
        {
            if (_locked)
            {
                return;
            }

            _locked = true;

            ApplyLockedStatus();
        }

        private void ApplyLockedStatus()
        {
            _button.interactable = !_locked;
            _lock.SetActive(_locked);
        }
    }
}