using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Gameplay.Views;
using UnityEngine;
using VContainer;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Factories
{
    public class DecorFactory
    {
        [Inject]
        private readonly VisualData _visualData;

        [Inject]
        private readonly Transform _decorParent;

        public DecorView CreateDecorElement(DecorElementData decorElementData, int sortingOrder)
        {
            var decorView = Object.Instantiate(_visualData.DecorElementPrefab, _decorParent, false);

            decorView.SetSprite(decorElementData.Sprite);

            decorView.transform.localScale = decorElementData.Scale;

            decorView.SetSortingOrder(sortingOrder);

            return decorView;
        }
    }
}