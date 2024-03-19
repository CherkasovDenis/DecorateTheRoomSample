using UnityEngine;
using UnityEngine.UI;

namespace BlackKakadu.DecorateTheRoom.Ui.Views
{
    public class PageView : ContentView
    {
        public ToggleGroup ToggleGroup => _toggleGroup;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        [SerializeField]
        private AspectRatioFitter _aspectRatioFitter;

        private void Start()
        {
            _aspectRatioFitter.enabled = true;
        }
    }
}