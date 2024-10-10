using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;

namespace Watermelon
{
    public class LoadingGraphics : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI loadingText;
        [SerializeField] Image backgroundImage;
        [SerializeField] CanvasScaler canvasScaler;
        [SerializeField] Camera loadingCamera;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            canvasScaler.matchWidthOrHeight = UIUtils.IsWideScreen(loadingCamera) ? 1 : 0;

            
        }

        public void Start()
        {
            if (I2.Loc.LocalizationManager.CurrentLanguage == "English")
                OnLoading(0.0f, "Loading..");
            else if (I2.Loc.LocalizationManager.CurrentLanguage == "Russian")
                OnLoading(0.0f, "Загрузка..");
        }

        private void OnEnable()
        {
            GameLoading.OnLoading += OnLoading;
            GameLoading.OnLoadingFinished += OnLoadingFinished;
        }

        private void OnDisable()
        {
            GameLoading.OnLoading -= OnLoading;
            GameLoading.OnLoadingFinished -= OnLoadingFinished;
        }

        private void OnLoading(float state, string message)
        {
            loadingText.text = message;
        }

        private void OnLoadingFinished()
        {
            loadingText.DOFade(0.0f, 0.6f, unscaledTime: true);
            backgroundImage.DOFade(0.0f, 0.6f, unscaledTime: true).OnComplete(delegate
            {
                Destroy(gameObject);
                YandexGame.GameReadyAPI();
                //Debug.Log("YandexGame.GameReadyAPI();");
            });
        }
    }
}
