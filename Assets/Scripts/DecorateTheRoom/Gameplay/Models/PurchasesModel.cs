using System;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Models
{
    [UsedImplicitly]
    public class PurchasesModel
    {
        public event Action StartedPurchasing;

        public event Action EndedPurchasing;

        public void OnStartedPurchasing()
        {
            StartedPurchasing?.Invoke();
        }

        public void OnEndedPurchasing(string id, string developerPayload, string error) => OnEndedPurchasing();

        public void OnEndedPurchasing(string id, string developerPayload) => OnEndedPurchasing();

        public void OnEndedPurchasing()
        {
            EndedPurchasing?.Invoke();
        }
    }
}