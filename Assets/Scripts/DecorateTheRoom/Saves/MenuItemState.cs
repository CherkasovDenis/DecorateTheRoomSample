using System;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Saves
{
    [Serializable]
    public class MenuItemState
    {
        public string Id => _id;

        public bool Locked
        {
            get => _locked;
            set => _locked = value;
        }

        [SerializeField]
        private string _id;

        [SerializeField]
        private bool _locked;

        public MenuItemState(string id, bool locked)
        {
            _id = id;
            _locked = locked;
        }
    }
}