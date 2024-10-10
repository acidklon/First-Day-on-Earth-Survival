using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class UIMissionRewardPopUp : MonoBehaviour
    {
        [SerializeField] UIFadeAnimation fadeAnimation;
        [SerializeField] UIScaleAnimation panelBackScaleAnimation;

        [Space]
        [SerializeField] TMP_Text headerText;
        [SerializeField] Image previewIcon;
        [SerializeField] TMP_Text mainText;
        [SerializeField] TMP_Text buttonText;

        [Space]
        [SerializeField] Button fadeButton;
        [SerializeField] Button continueButton;

        private void Awake()
        {
            fadeButton.onClick.AddListener(ClosePanelButton);
            continueButton.onClick.AddListener(ClosePanelButton);
        }

        public void OnMissionFinished(Mission mission)
        {
            if (mission.RewardType == MissionRewardType.Tool)
            {
                Show(mission.ToolsReward.RewardInfo);
            }
            else if (mission.RewardType == MissionRewardType.Generic)
            {
                Show(mission.GenericReward.RewardInfo);
            }
        }

        public void Show(RewardInfo data)
        {
            gameObject.SetActive(true);

            fadeAnimation.Show();
            panelBackScaleAnimation.Show();

            headerText.text = data.HeaderText;
            previewIcon.sprite = data.PreviewIcon;
            mainText.text = data.MainText;
            buttonText.text = data.ButtonText;

            UIGamepadButton.DisableAllTags();
            UIGamepadButton.EnableTag(UIGamepadButtonTag.Popup);
        }

        public void Hide()
        {
            if (!gameObject.activeSelf)
                return;

            fadeAnimation.Hide();
            panelBackScaleAnimation.Hide(onCompleted: () =>
            {
                gameObject.SetActive(false);
            });

            UIGamepadButton.DisableAllTags();
            UIGamepadButton.EnableTag(UIGamepadButtonTag.Game);
        }

        private void ClosePanelButton()
        {
            MissionsController.CompleteMission();
        }

        [System.Serializable]
        public class RewardInfo
        {
            [SerializeField] string headerText = "YOU UNLOCKED";
            [SerializeField] string headerTextRu;
            public string HeaderText
            {
                get
                {
                    if (LocalizationManager.CurrentLanguage == "English")
                        return headerText;
                    else if (LocalizationManager.CurrentLanguage == "Russian")
                        return headerTextRu;
                    else
                        return headerText;
                }
            }

            [SerializeField] Sprite previewIcon;
            public Sprite PreviewIcon => previewIcon;

            [SerializeField] string mainText;
            [SerializeField] string mainTextRu;
            public string MainText
            {
                get
                {
                    if (LocalizationManager.CurrentLanguage == "English")
                        return mainText;
                    else if (LocalizationManager.CurrentLanguage == "Russian")
                        return mainTextRu;
                    else
                        return mainText;
                }
            }

            [SerializeField] string buttonText = "AWESOME";
            [SerializeField] string buttonTextRu;
            public string ButtonText
            {
                get
                {
                    if (LocalizationManager.CurrentLanguage == "English")
                        return buttonText;
                    else if (LocalizationManager.CurrentLanguage == "Russian")
                        return buttonTextRu;
                    else
                        return buttonText;
                }
            }
        }
    }
}