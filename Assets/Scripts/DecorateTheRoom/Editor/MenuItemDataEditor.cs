#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using BlackKakadu.DecorateTheRoom.Data;
using Sirenix.OdinInspector.Editor;
using Object = UnityEngine.Object;

namespace BlackKakadu.DecorateTheRoom.Editor
{
    [CustomEditor(typeof(MenuItemData), true)]
    [CanEditMultipleObjects]
    public class MenuItemDataEditor : OdinEditor
    {
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            var targetData = (MenuItemData)target;

            if (!CheckMenuItemData(targetData))
            {
                return null;
            }

            var previewTexture = new Texture2D(width, height);
            EditorUtility.CopySerialized(targetData.Sprite.texture, previewTexture);

            return previewTexture;
        }

        public override bool HasPreviewGUI() => CheckMenuItemData((MenuItemData)target);

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            var targetData = (MenuItemData)target;

            GUI.DrawTexture(r, targetData.Sprite.texture, ScaleMode.ScaleToFit);
        }

        private static bool CheckMenuItemData(MenuItemData targetData)
        {
            return targetData != null && targetData.Sprite != null;
        }
    }
}

#endif