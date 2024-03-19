using System;
using System.Collections.Generic;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using JetBrains.Annotations;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Ui.Models
{
    [UsedImplicitly]
    public class PagesModel
    {
        public event Action ChangedCurrentPage;

        public List<PageView> Pages { get; } = new List<PageView>();

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                var newValue = Mathf.Clamp(value, MinPageIndex, MaxPageIndex);

                if (newValue == _currentPage)
                {
                    return;
                }

                _currentPage = value;
                ChangedCurrentPage?.Invoke();
            }
        }

        public int TotalPageCount { get; private set; }

        public int MinPageIndex { get; private set; }

        public int MaxPageIndex { get; private set; }

        private int _currentPage = -1;

        public void Initialize()
        {
            TotalPageCount = Pages.Count;
            MinPageIndex = 0;
            MaxPageIndex = TotalPageCount - 1;
        }
    }
}