using UnityEngine;
using UnityEngine.Events;
using Watermelon;
//using static UnityEngine.InputManagerEntry;
//using static Watermelon.CurrencyReward;

namespace YG.Example
{
    [HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#10e7dfffefdc42ec93b39be0c78e77cb")]
    public class ReceivingPurchaseExample : MonoBehaviour
    {
        public static ReceivingPurchaseExample Instance;

        [SerializeField] UnityEvent successPurchased;
        [SerializeField] UnityEvent failedPurchased;

        private SimpleBoolSave save;

        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += SuccessPurchased;
            YandexGame.PurchaseFailedEvent += FailedPurchased;
        }

        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
            YandexGame.PurchaseFailedEvent -= FailedPurchased;
        }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //SavesYG.DeleteAll();
            //YandexGame.ResetSaveProgress();
        }

        //private void Start()
        //{
        //SoundManager.SetVolumeMusic(0f);
        //MonoSingleton<PlayerDataManager>.Instance.IsOnSoundBGM = false;
        //MonoSingleton<PlayerDataManager>.Instance.SaveOptionSound();
        //YandexGame.ConsumePurchases();
        //}

        void SuccessPurchased(string id)
        {
            successPurchased?.Invoke();

            Debug.Log("GetBuyMy = " + id);
            

            switch (id)
            {
                case "StarterPack":
                    save = SaverManagerMy.GetSaveObject<SimpleBoolSave>($"IAPProduct_StarterPack");

                    CurrenciesController.Add(CurrencyType.Coins, 500);
                    CurrenciesController.Add(CurrencyType.Wood, 100);
                    CurrenciesController.Add(CurrencyType.Stone, 100);
                    CurrenciesController.Add(CurrencyType.Berries, 100);
                    
                    

                    save.Value = true;

                    save = SaverManagerMy.GetSaveObject<SimpleBoolSave>($"IAPProduct_PlayerSkin1");

                    FindObjectOfType<PlayerSkinReward>().ApplyReward();

                    save.Value = true;

                    GameObject.Find("IAP Offer Starter Pack").SetActive(false);
                    break;
                case "GoldSmall":
                    save = SaverManagerMy.GetSaveObject<SimpleBoolSave>($"IAPProduct_GoldSmall");

                    CurrenciesController.Add(CurrencyType.Coins, 150);

                    save.Value = true;
                    break;
                case "GoldMedium":
                    save = SaverManagerMy.GetSaveObject<SimpleBoolSave>($"IAPProduct_GoldMedium");

                    CurrenciesController.Add(CurrencyType.Coins, 450);

                    save.Value = true;
                    break;
                case "GoldBig":
                    save = SaverManagerMy.GetSaveObject<SimpleBoolSave>($"IAPProduct_GoldBig");

                    CurrenciesController.Add(CurrencyType.Coins, 1000);

                    save.Value = true;
                    break;
                case "PlayerSkin1":
                    save = SaverManagerMy.GetSaveObject<SimpleBoolSave>($"IAPProduct_PlayerSkin1");

                    FindObjectOfType<PlayerSkinReward>().ApplyReward();

                    save.Value = true;
                    break;
            }
            

            SaveController.MarkAsSaveIsRequired();
            SaverManagerMy.Instance.Save();

            YandexGame.SaveProgress();
            // Ваш код для обработки покупки. Например:
            //if (id == "50")
            //    YandexGame.savesData.money += 50;
            //else if (id == "250")
            //    YandexGame.savesData.money += 250;
            //else if (id == "1500")
            //    YandexGame.savesData.money += 1500;
            //YandexGame.SaveProgress();
        }

        void FailedPurchased(string id)
        {
            failedPurchased?.Invoke();
        }
    }
}