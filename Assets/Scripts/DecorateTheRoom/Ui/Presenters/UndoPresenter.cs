using System;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Services;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom.Ui.Presenters
{
    public class UndoPresenter : IInitializable, IDisposable
    {
        [Inject]
        private readonly Button _undoButton;

        [Inject]
        private readonly UndoModel _undoModel;

        [Inject]
        private readonly SoundService _soundService;

        public void Initialize()
        {
            _undoModel.AddedAction += EnableUndoButton;
            _undoModel.RemovedAllActions += DisableUndoButton;

            _undoButton.onClick.AddListener(Undo);

            DisableUndoButton();
        }

        public void Dispose()
        {
            _undoModel.AddedAction -= EnableUndoButton;
            _undoModel.RemovedAllActions -= DisableUndoButton;

            _undoButton.onClick.RemoveListener(Undo);
        }

        private void Undo()
        {
            _soundService.PlayClickSound();
            _undoModel.Undo();
        }

        private void EnableUndoButton()
        {
            _undoButton.interactable = true;
        }

        private void DisableUndoButton()
        {
            _undoButton.interactable = false;
        }
    }
}