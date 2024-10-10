using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Watermelon
{
    public sealed class AdsRewardsHolder : RewardsHolder
    {
        [Group("Settings"), UniqueID]
        [SerializeField] string rewardID;

        [Group("Settings"), Space]
        [SerializeField] Button adsButton;

        [Group("Settings")]
        [SerializeField] bool disableAfterPurchase;

        private SimpleBoolSave save;

        private void Awake()
        {
            InitialiseComponents();

            save = SaverManagerMy.GetSaveObject<SimpleBoolSave>($"CurrencyProduct_{rewardID}");

            if (disableAfterPurchase && save.Value)
            {
                // Disable holder game object
                gameObject.SetActive(false);

                return;
            }

            // Check if holder needs to be disabled
            for (int i = 0; i < rewards.Length; i++)
            {
                if (rewards[i].CheckDisableState())
                {
                    // Disable holder game object
                    gameObject.SetActive(false);

                    return;
                }
            }

            adsButton.onClick.AddListener(OnPurchased);
        }

        private void OnPurchased()
        {
#if MODULE_HAPTIC
            Haptic.Play(Haptic.HAPTIC_LIGHT);
#endif

            AudioController.PlaySound(AudioController.AudioClips.buttonSound);

            YandexGame.RewVideoShow(0, rewarded);
            /*AdsManager.ShowRewardBasedVideo((reward) =>
            {
                if (reward)
                {
                    ApplyRewards();

                    save.Value = true;

                    if (disableAfterPurchase)
                    {
                        // Disable holder game object
                        gameObject.SetActive(false);
                    }

                    SaveController.MarkAsSaveIsRequired();
                }
            });*/
        }

        public void rewarded()
        {
            ApplyRewards();

            save.Value = true;

            if (disableAfterPurchase)
            {
                // Disable holder game object
                gameObject.SetActive(false);
            }

            SaveController.MarkAsSaveIsRequired();
        }
    }
}
