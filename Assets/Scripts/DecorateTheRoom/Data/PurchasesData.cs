using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Data
{
    [CreateAssetMenu(fileName = "PurchasesData", menuName = "BlackKakadu/DecorateTheRoom/PurchasesData")]
    public class PurchasesData : ScriptableObject
    {
        public string PurchaseAllId => _purchaseAllId;

        public string PurchaseCategoryId => _purchaseCategoryId;

        [SerializeField]
        private string _purchaseAllId;

        [SerializeField]
        private string _purchaseCategoryId;
    }
}