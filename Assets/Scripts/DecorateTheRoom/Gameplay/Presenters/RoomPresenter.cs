using System;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Presenters
{
    public class RoomPresenter : IStartable, IDisposable
    {
        [Inject]
        private readonly RoomView _roomView;

        [Inject]
        private readonly RoomModel _roomModel;

        public void Start()
        {
            _roomModel.ReorderedDecors += UpdateSortingOrders;
            _roomModel.ChangedBackground += UpdateBackground;

            _roomModel.SetBackground(_roomView.Background.sprite);
        }

        public void Dispose()
        {
            _roomModel.ReorderedDecors -= UpdateSortingOrders;
            _roomModel.ChangedBackground -= UpdateBackground;
        }

        private void UpdateSortingOrders()
        {
            for (var i = 0; i < _roomModel.CurrentDecors.Count; i++)
            {
                _roomModel.CurrentDecors[i].SetSortingOrder(i);
            }
        }

        private void UpdateBackground(Sprite sprite)
        {
            _roomView.SetBackground(sprite);
        }
    }
}