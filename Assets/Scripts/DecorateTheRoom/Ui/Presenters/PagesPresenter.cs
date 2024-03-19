using System;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class PagesPresenter : IStartable, IDisposable
    {
        [Inject]
        private readonly MenuView _menuView;

        [Inject]
        private readonly PagesModel _pagesModel;

        [Inject]
        private readonly SoundService _soundService;

        public void Start()
        {
            _pagesModel.ChangedCurrentPage += ChangedPage;

            _menuView.NextCategoryPageButton.onClick.AddListener(NextPage);
            _menuView.PreviousCategoryPageButton.onClick.AddListener(PreviousPage);
        }

        public void Dispose()
        {
            _pagesModel.ChangedCurrentPage -= ChangedPage;

            _menuView.NextCategoryPageButton.onClick.RemoveListener(NextPage);
            _menuView.PreviousCategoryPageButton.onClick.RemoveListener(PreviousPage);
        }

        private void ChangedPage()
        {
            for (var i = 0; i < _pagesModel.Pages.Count; i++)
            {
                var page = _pagesModel.Pages[i];

                if (i == _pagesModel.CurrentPage)
                {
                    page.Show();
                }
                else
                {
                    page.Hide();
                }
            }

            if (_pagesModel.CurrentPage == _pagesModel.MinPageIndex)
            {
                _menuView.HidePreviousButton();
            }
            else
            {
                _menuView.ShowPreviousButton();
            }

            if (_pagesModel.CurrentPage == _pagesModel.MaxPageIndex)
            {
                _menuView.HideNextButton();
            }
            else
            {
                _menuView.ShowNextButton();
            }

            _menuView.SetPageNumber(_pagesModel.CurrentPage + 1, _pagesModel.TotalPageCount);
        }

        private void NextPage()
        {
            _soundService.PlayClickSound();
            _pagesModel.CurrentPage++;
        }

        private void PreviousPage()
        {
            _soundService.PlayClickSound();
            _pagesModel.CurrentPage--;
        }
    }
}