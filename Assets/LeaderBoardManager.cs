using I2.Loc;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;

    public string Localization;

    [SerializeField]
    private LeaderboardYG leaderboardYG;

    public void Awake()
    {
        if (LeaderBoardManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Localization = LocalizationManager.CurrentLanguage;
    }

    public void PauseSound()
    {
        AudioListener.volume = 0f;
        Time.timeScale = 0f;
    }

    public void ResumeSound()
    {
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
    }

    /*public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            PauseMusic();
        if (Input.GetKeyDown(KeyCode.P))
            ResumeMusic();
    }*/

    public void NewScore(int score)
    {
        if (SavesYG.GetInt("LeaderBoardScoreMy") < score)
        {
            SavesYG.SetInt("LeaderBoardScoreMy", score);
            YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, score);
        }
    }

    
    public void PauseMusic()
    {
        try
        {
            GameObject.Find("World").GetComponent<AudioSource>().Stop();
        }
        catch (System.Exception e)
        {

        }
        
    }

    public void ResumeMusic()
    {
        try
        {
            GameObject.Find("World").GetComponent<AudioSource>().Play();
        }
        catch (System.Exception e)
        {

        }
    }
}