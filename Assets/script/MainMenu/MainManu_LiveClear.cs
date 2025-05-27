using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManu_LiveClear : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;
    public MainMenu_SceneManager _mmSceneManager;

    public Image CoverImg;

    public Image LevelImage;
    public Sprite Easy;
    public Sprite Normal;
    public Sprite Hard;

    public Text SongName;

    public Text FinialTotleScore;
    public Text FinialMaxComboCount;

    public Text FinialPerfectCount;
    public Text FinialGreatCount;
    public Text FinialMissCount;
    public Text LevelNumber;

    public Image FullComboImage;
    public Image AllPerfectImage;

    public Button NextButton;
    public Button RetryButton;

    [SerializeField] private string LiveFinalResult;
    [SerializeField] private string Level;

    public MusicBgmListSO BgmPool;
    private MusicBgmSO PlayingBgm = null;

    public void OnEnterMenu()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        NextButton.onClick.AddListener(OnClickNextButton);
        RetryButton.onClick.AddListener(OnClickRetryButton);
        FullComboImage.gameObject.SetActive(false);
        AllPerfectImage.gameObject.SetActive(false);


        PlayingBgm = BgmPool.GetRendomBgm();
        _gameAudioManager.SetBgmMusic(PlayingBgm.Bgm, 0);
        print("LiveClearMenu Load!");

        ShowScores();
        _gameSceneManager.AddEconomyValueOnLiveClear();
    }
    public void OnExitMenu()
    {

    }

    public void OnClickNextButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _mmSceneManager.SwichMenu("SongMenu");
    }
    public void OnClickRetryButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _mmSceneManager.SwichMenu("inGameScene");
    }


    private void ShowScores()
    {
        SongName.text = $"{_gameSceneManager.GetPlayMusicSO().MusicDisplayName}";
        LiveFinalResult = _gameSceneManager.LiveFinalResult;
        Level = _gameSceneManager.Level;

        FinialTotleScore.text = $"{_gameSceneManager.FinialTotleScore}";
        FinialMaxComboCount.text = $"{_gameSceneManager.FinialMaxComboCount}";

        FinialPerfectCount.text = $"{_gameSceneManager.FinialPerfectCount}";
        FinialGreatCount.text = $"{_gameSceneManager.FinialGreatCount}";
        FinialMissCount.text = $"{_gameSceneManager.FinialMissCount}";

        FinialTotleScore.text = $"{_gameSceneManager.FinialTotleScore}";
        LevelNumber.text = $"{_gameSceneManager.LevelNumber}";

        UpdateUi();
    }

    private void UpdateUi()
    {
        CoverImg.sprite = _gameSceneManager.GetPlayMusicSO().Cover169;

        if (Level == "Easy")
        {
            LevelImage.sprite = Easy;
        }
        else if (Level == "Normal")
        {
            LevelImage.sprite = Normal;
        }
        else if (Level == "Hard")
        {
            LevelImage.sprite = Hard;
        }


        if (LiveFinalResult == "All_Perfect")
        {
            AllPerfectImage.gameObject.SetActive(true);
        }
        else if (LiveFinalResult == "Full_Combo")
        {
            FullComboImage.gameObject.SetActive(true);
        }
        else
        {
            FullComboImage.gameObject.SetActive(false);
            AllPerfectImage.gameObject.SetActive(false);
        }
    }


    private void OnDestroy()
    {
        NextButton.onClick.RemoveAllListeners();
        RetryButton.onClick.RemoveAllListeners();
    }
}
