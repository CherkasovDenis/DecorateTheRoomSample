#if UNITY_EDITOR

using System.IO;
using BlackKakadu.DecorateTheRoom.Data;
using UnityEngine;
using UnityEditor;

namespace BlackKakadu.DecorateTheRoom.Editor
{
    public class MenuItemDataCreator : UnityEditor.Editor
    {
        private const string MenuPath = "Assets/BlackKakadu/DecorateTheRoom/";
        private const string DataFolderName = "Data";

        [MenuItem(MenuPath + nameof(CreateDecorElementData), true)]
        [MenuItem(MenuPath + nameof(CreateRoomBackgroundData), true)]
        private static bool ValidateCreateDecorElementData()
        {
            var selectedSprites = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

            return selectedSprites.Length != 0;
        }

        [MenuItem(MenuPath + nameof(CreateDecorElementData), false)]
        private static void CreateDecorElementData() => CreateMenuItemData<DecorElementData>();

        [MenuItem(MenuPath + nameof(CreateRoomBackgroundData), false)]
        private static void CreateRoomBackgroundData() => CreateMenuItemData<RoomBackgroundData>();

        private static void CreateMenuItemData<T>() where T : MenuItemData
        {
            var selectedTexture2D = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

            var currentPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedTexture2D[0]));

            if (currentPath == null)
            {
                Debug.LogError("Current path is null");
                return;
            }

            var dataPath = Path.Combine(currentPath, DataFolderName);

            if (!Directory.Exists(dataPath))
            {
                Debug.Log($"Creating folder: {dataPath}");
                Directory.CreateDirectory(dataPath);
                AssetDatabase.Refresh();
            }

            foreach (var selectedTexture in selectedTexture2D)
            {
                var menuItemData = CreateInstance<T>();

                var spritePath = AssetDatabase.GetAssetPath(selectedTexture);
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                var id = Path.GetFileNameWithoutExtension(spritePath);

                menuItemData.Initialize(sprite, id);

                AssetDatabase.CreateAsset(menuItemData, Path.Combine(dataPath, $"{id}.asset"));
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Created {selectedTexture2D.Length} {typeof(T)}!");
        }
    }
}

#endif