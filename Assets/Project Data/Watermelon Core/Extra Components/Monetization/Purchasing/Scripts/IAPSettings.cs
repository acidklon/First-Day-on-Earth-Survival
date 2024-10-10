using UnityEngine;

namespace Watermelon
{
    [SetupTab("IAP", texture = "icon_iap")]
    [CreateAssetMenu(fileName = "IAP Settings", menuName = "Settings/IAP Settings")]
    public class IAPSettings : ScriptableObject
    {
        [Group("Settings")]
        [SerializeField] GameObject messagesCanvasPrefab;
        public GameObject MessagesCanvasPrefab => messagesCanvasPrefab;

        [SerializeField] IAPItem[] storeItems;
        public IAPItem[] StoreItems => storeItems;
    }
}