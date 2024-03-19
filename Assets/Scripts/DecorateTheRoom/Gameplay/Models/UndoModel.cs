using System;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Models
{
    [UsedImplicitly]
    public class UndoModel
    {
        public event Action AddedAction;

        public event Action RemovedAllActions;

        private readonly UndoAction[] _actions = new UndoAction[20];

        private int _currentIndex = -1;

        public void Undo()
        {
            if (_currentIndex < 0)
            {
                return;
            }

            var lastAction = _actions[_currentIndex];

            lastAction.Undo?.Invoke();

            _currentIndex--;

            if (_currentIndex < 0)
            {
                RemovedAllActions?.Invoke();
            }
        }

        public void Add(Action undo) => Add(new UndoAction { Undo = undo });

        public void Add(Action undo, Action overflow) => Add(new UndoAction { Undo = undo, Overflow = overflow });

        private void Add(UndoAction action)
        {
            _currentIndex++;

            if (_actions.Length == _currentIndex)
            {
                var overflowAction = _actions[0];
                overflowAction.Overflow?.Invoke();

                _currentIndex--;

                for (var i = 0; i < _currentIndex; i++)
                {
                    _actions[i] = _actions[i + 1];
                }
            }

            _actions[_currentIndex] = action;

            AddedAction?.Invoke();
        }

        private class UndoAction
        {
            public Action Undo { get; set; }

            public Action Overflow { get; set; }
        }
    }
}