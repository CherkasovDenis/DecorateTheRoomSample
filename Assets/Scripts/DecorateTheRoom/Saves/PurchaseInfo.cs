using System;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Saves
{
    [Serializable]
    public class PurchaseInfo : IEquatable<PurchaseInfo>
    {
        public string Id => _id;

        public string AdditionalData => _additionalData;

        [SerializeField]
        private string _id;

        [SerializeField]
        private string _additionalData;

        public PurchaseInfo(string id, string additionalData)
        {
            _id = id;
            _additionalData = additionalData;
        }

        public bool Equals(PurchaseInfo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _id == other._id && _additionalData == other._additionalData;
        }
    }
}