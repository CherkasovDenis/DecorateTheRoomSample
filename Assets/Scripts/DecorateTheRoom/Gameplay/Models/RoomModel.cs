using System;
using System.Collections.Generic;
using System.Linq;
using BlackKakadu.DecorateTheRoom.Gameplay.Views;
using JetBrains.Annotations;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Models
{
    [UsedImplicitly]
    public class RoomModel
    {
        public event Action<Sprite> ChangedBackground;

        public event Action<DecorView> AddedDecor;

        public event Action<DecorView> RemovedDecor;

        public event Action ReorderedDecors;

        public List<DecorView> CurrentDecors { get; private set; } = new List<DecorView>();

        public bool TrashEnabled { get; private set; }

        public Sprite CurrentBackground { get; private set; }

        public int CurrentFrontOrder { get; private set; }

        public int CurrentBackOrder { get; private set; }

        public void SetBackground(Sprite sprite)
        {
            CurrentBackground = sprite;
            ChangedBackground?.Invoke(sprite);
        }

        public void AddDecor(DecorView decorView)
        {
            CurrentDecors.Add(decorView);
            AddedDecor?.Invoke(decorView);
        }

        public void RemoveDecor(DecorView decorView)
        {
            CurrentDecors.Remove(decorView);
            RemovedDecor?.Invoke(decorView);
        }

        public int GetFrontSortingOrder()
        {
            CurrentFrontOrder++;
            return CurrentFrontOrder;
        }

        public int GetBackSortingOrder()
        {
            CurrentBackOrder--;
            return CurrentBackOrder;
        }

        public void ReorderDecors()
        {
            CurrentDecors = CurrentDecors.OrderBy(decorModel => decorModel.SortingOrder).ToList();
            CurrentBackOrder = 0;
            CurrentFrontOrder = CurrentDecors.Count;
            ReorderedDecors?.Invoke();
        }

        public void SetTrashStatus(bool status)
        {
            TrashEnabled = status;
        }
    }
}