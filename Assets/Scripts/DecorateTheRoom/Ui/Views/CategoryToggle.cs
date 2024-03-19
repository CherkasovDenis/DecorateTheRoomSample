using System;
using UnityEngine;
using UnityEngine.UI;

namespace BlackKakadu.DecorateTheRoom.Ui.Views
{
    public class CategoryToggle : MonoBehaviour
    {
        public event Action<string> ActivatedCategory;

        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private string _categoryId;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);

            OnToggleValueChanged(_toggle.isOn);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        public void Initialize(ToggleGroup toggleGroup, Sprite icon, string categoryId)
        {
            _toggle.group = toggleGroup;
            _image.sprite = icon;
            _categoryId = categoryId;
        }

        private void OnToggleValueChanged(bool value)
        {
            if (value)
            {
                ActivatedCategory?.Invoke(_categoryId);
            }
        }
    }
}