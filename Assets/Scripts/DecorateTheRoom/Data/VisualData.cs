using BlackKakadu.DecorateTheRoom.Gameplay.Views;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using UnityEngine;
using UnityEngine.UI;

namespace BlackKakadu.DecorateTheRoom.Data
{
    [CreateAssetMenu(fileName = "VisualData", menuName = "BlackKakadu/DecorateTheRoom/VisualData")]
    public class VisualData : ScriptableObject
    {
        public DecorView DecorElementPrefab => _decorElementPrefab;

        public PageView PagePrefab => _pagePrefab;

        public CategoryToggle CategoryPrefab => _categoryPrefab;

        public ContentView ElementsContentPrefab => _elementsContentPrefab;

        public GridLayoutGroup MenuGroupGridPrefab => _menuGroupGridPrefab;

        public MenuButton MenuButtonPrefab => _menuButtonPrefab;

        public int CategoriesPerPage => _categoriesPerPage;

        public float MenuMoveDuration => _menuMoveDuration;

        public float MenuShowValue => _menuShowValue;

        public float MenuHideValue => _menuHideValue;

        [SerializeField]
        private DecorView _decorElementPrefab;

        [SerializeField]
        private PageView _pagePrefab;

        [SerializeField]
        private CategoryToggle _categoryPrefab;

        [SerializeField]
        private ContentView _elementsContentPrefab;

        [SerializeField]
        private GridLayoutGroup _menuGroupGridPrefab;

        [SerializeField]
        private MenuButton _menuButtonPrefab;

        [SerializeField]
        private int _categoriesPerPage;

        [SerializeField]
        private float _menuMoveDuration;

        [SerializeField]
        private float _menuShowValue;

        [SerializeField]
        private float _menuHideValue;
    }
}