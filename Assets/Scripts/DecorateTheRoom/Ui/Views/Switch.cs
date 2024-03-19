using System;
using UnityEngine;
using UnityEngine.UI;

namespace BlackKakadu.DecorateTheRoom.Ui.Views
{
    public class Switch : MonoBehaviour
    {
        public event Action<bool> Initialized;

        public event Action<bool> ChangedValue;

        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private Image _targetImage;

        [SerializeField]
        private Color _activeColor;

        [SerializeField]
        private GameObject _activeIcon;

        [SerializeField]
        private Color _inactiveColor;

        [SerializeField]
        private GameObject _inactiveIcon;

        private bool _initialized;

        private void Start()
        {
            SwitchToggle(_toggle.isOn);
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(SwitchToggle);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(SwitchToggle);
        }

        private void SwitchToggle(bool status)
        {
            _targetImage.color = status ? _activeColor : _inactiveColor;

            _activeIcon.SetActive(status);
            _inactiveIcon.SetActive(!status);

            if (!_initialized)
            {
                _initialized = true;
                Initialized?.Invoke(status);
                return;
            }

            ChangedValue?.Invoke(status);
        }
    }
}