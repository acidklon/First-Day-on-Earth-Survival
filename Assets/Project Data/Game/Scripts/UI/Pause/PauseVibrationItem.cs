using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class PauseVibrationItem : PauseItem
    {
        [SerializeField] Image imageRef;

        [Space]
        [SerializeField] Sprite activeSprite;
        [SerializeField] Sprite disableSprite;

        private bool isActive = true;

        private void Start()
        {
            isActive = Haptic.IsActive;

            if (isActive)
                imageRef.sprite = activeSprite;
            else
                imageRef.sprite = disableSprite;
        }

        protected override void Click()
        {
            isActive = !isActive;

            if (isActive)
            {
                imageRef.sprite = activeSprite;

                Haptic.IsActive = true;
            }
            else
            {
                imageRef.sprite = disableSprite;

                Haptic.IsActive = false;
            }

            // Play button sound
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        }
    }
}
