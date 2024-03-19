using System;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Ui.Models
{
    [UsedImplicitly]
    public class MenuWindowModel
    {
        public event Action ChangedState;

        public bool IsShowing
        {
            get => _isShowing;
            set
            {
                if (_isShowing == value)
                {
                    return;
                }

                _isShowing = value;
                ChangedState?.Invoke();
            }
        }

        private bool _isShowing;
    }
}