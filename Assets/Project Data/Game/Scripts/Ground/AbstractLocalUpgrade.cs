using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.GlobalUpgrades;

namespace Watermelon
{
    // Usint it as a marker for now
    public abstract class AbstractLocalUpgrade: MonoBehaviour, IUpgrade
    {
        [SerializeField] protected string title;
        [SerializeField] protected string titleRu;
        public string Title
        { 
            get
            {
                if (LocalizationManager.CurrentLanguage == "English")
                    return title;
                else if (LocalizationManager.CurrentLanguage == "Russian")
                    return titleRu;
                else
                    return title;
            }
        }

        [SerializeField] protected Sprite icon;
        public Sprite Icon => icon;

        [SerializeField] string descriptionFormat;
        [SerializeField] string descriptionFormatRu;
        public string DescriptionFormat
        {
            get
            {
                if (LocalizationManager.CurrentLanguage == "English")
                    return descriptionFormat;
                else if (LocalizationManager.CurrentLanguage == "Russian")
                    return descriptionFormatRu;
                else
                    return descriptionFormat;
            }
        }

        public event SimpleCallback OnUpgraded;

        private SimpleIntSave save;

        public int UpgradeIndex
        {
            get => save.Value;
            protected set
            {
                save.Value = value;
                OnUpgraded?.Invoke();
            }
        }

        public int UpgradeLevel => UpgradeIndex + 1;

        public abstract int UpgradesCount { get; }
        public bool IsMaxedOut => UpgradeIndex + 1 >= UpgradesCount;

        public UpgradeType Type => UpgradeType.Local;

        public abstract Resource GetUpgradeCost(int level);
        public abstract void UpgradeStage();
        public abstract string GetUpgradeDescription(int stageId);

        public bool IsHighlighted { get; set; }

        public void Init(string saveName)
        {
            save = SaverManagerMy.GetSaveObject<SimpleIntSave>(saveName);
        }
    }
}