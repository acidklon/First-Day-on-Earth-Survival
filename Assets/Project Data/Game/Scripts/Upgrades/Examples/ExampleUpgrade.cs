using UnityEngine;

namespace Watermelon
{
    using GlobalUpgrades;

    [CreateAssetMenu(menuName = "Content/Upgrades/Example Upgrade", fileName = "Example Upgrade")]
    public class ExampleUpgrade : GlobalUpgrade<ExampleUpgrade.Stage>
    {
        public override void Initialise()
        {

        }

        public override string GetUpgradeDescription(int stageId)
        {
            try
            {
                var prevValue = GetStage(stageId).Value;
                var value = GetStage(stageId + 1).Value;

                return string.Format(DescriptionFormat, prevValue, value);
            }
            catch
            {
                return "";
            }
        }

        [System.Serializable]
        public class Stage: GlobalUpgradeStage
        {
            [SerializeField] float value;
            public float Value => value;
        }
    }
}

