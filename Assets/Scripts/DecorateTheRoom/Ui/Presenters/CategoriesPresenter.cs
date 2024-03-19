using System;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class CategoriesPresenter : IStartable, IDisposable
    {
        [Inject]
        private readonly MenuView _menuView;

        [Inject]
        private readonly CategoriesModel _categoriesModel;

        [Inject]
        private readonly SoundService _soundService;

        public void Start()
        {
            foreach (var categoryToggle in _categoriesModel.CategoryToggles)
            {
                categoryToggle.ActivatedCategory += ChangeCategory;
            }
        }

        public void Dispose()
        {
            foreach (var categoryToggle in _categoriesModel.CategoryToggles)
            {
                categoryToggle.ActivatedCategory -= ChangeCategory;
            }
        }

        private void ChangeCategory(string categoryId)
        {
            foreach (var categoryInfo in _categoriesModel.CategoryInfos.Values)
            {
                categoryInfo.ContentView.Hide();
            }

            var currentContentUiView = _categoriesModel.CategoryInfos[categoryId].ContentView;

            currentContentUiView.Show();
            _menuView.Scroll.content = currentContentUiView.RectTransform;

            _categoriesModel.OnChangedCategory(categoryId);

            _soundService.PlayClickSound();
        }
    }
}