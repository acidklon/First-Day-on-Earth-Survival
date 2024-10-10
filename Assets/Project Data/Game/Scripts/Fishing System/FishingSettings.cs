using UnityEngine;

namespace Watermelon
{
    [CreateAssetMenu(fileName = "Fishing Settings", menuName = "Content/Fishing Settings")]
    public class FishingSettings : ScriptableObject
    {
        [Slider(0.0f, 1.0f)]
        [SerializeField] float spawnPercent = 0.5f;
        public float SpawnPercent => spawnPercent;
    }
}