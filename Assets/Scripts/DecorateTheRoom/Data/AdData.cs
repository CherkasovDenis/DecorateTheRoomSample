using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Data
{
    [CreateAssetMenu(fileName = "AdData", menuName = "BlackKakadu/DecorateTheRoom/AdData")]
    public class AdData : ScriptableObject
    {
        public int InterstitialAdFrequencyTime => _interstitialAdFrequencyTime;

        public int InterstitialAdPauseDuration => _interstitialAdPauseDuration;

        [SerializeField]
        private int _interstitialAdFrequencyTime;

        [SerializeField]
        private int _interstitialAdPauseDuration;
    }
}