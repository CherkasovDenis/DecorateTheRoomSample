using UnityEngine;

namespace BlackKakadu.DecorateTheRoom.Services
{
    public class SoundService : MonoBehaviour
    {
        public bool SfxEnabled { get; set; }

        [SerializeField]
        private AudioSource _clickSound;

        [SerializeField]
        private AudioSource _trashSound;

        [SerializeField]
        private AudioSource _screenshotSound;

        public void PlayClickSound()
        {
            PlaySfx(_clickSound);
        }

        public void PlayTrashSound()
        {
            PlaySfx(_trashSound);
        }

        public void PlayScreenshotSound()
        {
            PlaySfx(_screenshotSound);
        }

        private void PlaySfx(AudioSource source)
        {
            if (!SfxEnabled)
            {
                return;
            }

            source.Play();
        }
    }
}