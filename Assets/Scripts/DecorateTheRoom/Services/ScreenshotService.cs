using System;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Services
{
    public class ScreenshotService : MonoBehaviour
    {
        [SerializeField]
        private Camera _screenshotCamera;

        public void TakeScreenshot()
        {
            TakeScreenshotAsync().Forget();
        }

        public async UniTaskVoid TakeScreenshotAsync()
        {
            await UniTask.WaitForEndOfFrame(this);

            var width = Screen.width;
            var height = Screen.height;

            var rect = new Rect(0, 0, Screen.width, Screen.height);
            var renderTexture = new RenderTexture(width, height, 24);
            var screenShot = new Texture2D(width, height, TextureFormat.RGBA32, false);

            _screenshotCamera.targetTexture = renderTexture;
            _screenshotCamera.Render();

            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(rect, 0, 0);

            _screenshotCamera.targetTexture = null;
            RenderTexture.active = null;

            var bytes = ImageConversion.EncodeArrayToPNG(screenShot.GetRawTextureData(), screenShot.graphicsFormat,
                (uint)width, (uint)height);

            Destroy(renderTexture);

            var fileName = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss") + ".png";

#if UNITY_EDITOR
            System.IO.File.WriteAllBytes(Application.dataPath + "/" + fileName, bytes);
#elif UNITY_WEBGL
            DownloadScreenshot(bytes, bytes.Length, fileName);
#endif
        }

        [DllImport("__Internal")]
        private static extern string DownloadScreenshot(byte[] array, int byteLength, string fileName);
    }
}