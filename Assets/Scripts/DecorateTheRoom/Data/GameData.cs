using System.Collections.Generic;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "BlackKakadu/DecorateTheRoom/GameData")]
    public class GameData : ScriptableObject
    {
        public List<CategoryData> Categories => _categories;

        [SerializeField]
        private List<CategoryData> _categories;
    }
}