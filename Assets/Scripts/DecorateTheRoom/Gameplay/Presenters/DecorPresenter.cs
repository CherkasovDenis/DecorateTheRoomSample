using System;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Gameplay.Views;
using BlackKakadu.DecorateTheRoom.Services;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Presenters
{
    public class DecorPresenter : IInitializable, IDisposable
    {
        [Inject]
        private readonly RoomModel _roomModel;

        [Inject]
        private readonly UndoModel _undoModel;

        [Inject]
        private readonly SoundService _soundService;

        public void Initialize()
        {
            _roomModel.AddedDecor += InitializeDecor;
            _roomModel.RemovedDecor += DestroyDecor;
        }

        public void Dispose()
        {
            _roomModel.AddedDecor -= InitializeDecor;
            _roomModel.RemovedDecor -= DestroyDecor;
        }

        private void InitializeDecor(DecorView decorView)
        {
            decorView.Clicked += ClickedOnDecor;
            decorView.BeginDrag += CachePreviousPosition;
        }

        private void DestroyDecor(DecorView decorView)
        {
            decorView.Clicked -= ClickedOnDecor;
            decorView.BeginDrag -= CachePreviousPosition;
            Object.Destroy(decorView.gameObject);
        }

        private void CachePreviousPosition(DecorView decorView)
        {
            var previousPosition = decorView.transform.position;

            _undoModel.Add(() => decorView.transform.position = previousPosition);
        }

        private void ClickedOnDecor(DecorView decorView)
        {
            if (_roomModel.TrashEnabled)
            {
                TrashOut(decorView);
                return;
            }

            ChangeSortingOrder(decorView);
        }

        private void TrashOut(DecorView decorView)
        {
            _undoModel.Add(() => EnableView(decorView),
                () => _roomModel.RemoveDecor(decorView));

            DisableView(decorView);
            _soundService.PlayTrashSound();
        }

        private void ChangeSortingOrder(DecorView decorView)
        {
            var previousSortingOrder = decorView.SortingOrder;
            _undoModel.Add(() => decorView.SetSortingOrder(previousSortingOrder));

            decorView.SetSortingOrder(decorView.SortingOrder == _roomModel.CurrentFrontOrder
                ? _roomModel.GetBackSortingOrder()
                : _roomModel.GetFrontSortingOrder());
        }

        private void DisableView(DecorView decorView)
        {
            decorView.gameObject.SetActive(false);
            _roomModel.CurrentDecors.Remove(decorView);
        }

        private void EnableView(DecorView decorView)
        {
            decorView.gameObject.SetActive(true);
            _roomModel.CurrentDecors.Add(decorView);
        }
    }
}