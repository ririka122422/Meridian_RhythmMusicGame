using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public PlayerGameData _gameData;

    public GameAudioManager _gameAudioManager;
    public GameSceneUiManager gameSceneUiManager;
    private AsyncOperation LoadScene = new AsyncOperation();

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameSceneManager");

        GetPlayerData();
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public bool isFirstTimePlaying;

    public void GetPlayerData()
    {
        if (_gameData.isFirstTimePlaying) 
        { 
            SetNextMenu("FirstTimeMenu");
            isFirstTimePlaying = _gameData.isFirstTimePlaying;
        }

        Economy_Exp = _gameData.ExpNum;
        Economy_Star = _gameData.StarNum;
        Economy_ScNum = _gameData.ScNum;

        MusicVolum = _gameData.MusicVolum;
        SfxVolume = _gameData.SfxVolum;
        DropSpeed = _gameData.DropSpeed;
        MusicTimeOffset = _gameData.MusicTimeOffset;
        isMvOn = _gameData.isMvOn;
    }
    //------------------------------Scene Swich---------------------------------


    public void OnClickSwichScene(string SceneName)
    {
        SwichScene(SceneName);
    }
    public async void SwichScene(string SceneName)
    {
        Time.timeScale = 1;
        gameSceneUiManager.PlayLoadSceneTransitionAnimation();
        while (!gameSceneUiManager.GetIsOcclusion())
        {
            await Task.Yield();
        }
        LoadSceneAsync(SceneName);
        _gameAudioManager.StopBgm();
    }

    



    private void LoadSceneAsync(string SceneName)
    {
        LoadScene = SceneManager.LoadSceneAsync(SceneName);
    }
    public bool GetLoadSceneFinish()
    {
        return LoadScene.isDone;
    }
    //------------------------------Menu Swich---------------------------------

    [SerializeField] private string NextMenu = "null";

    public void SetNextMenu(string nextMenu)
    {
        NextMenu = nextMenu;
    }
    public string GetNextMenu()
    {
        return NextMenu;
    }


    //------------------------------------------------------------------------------------------------
    [Header("setting")]
    public int MusicVolum = 3;
    public int SfxVolume = 3;
    public int DropSpeed = 3;
    public float MusicTimeOffset = 0;//minisecond 音樂提早撥放
    public bool isMvOn;


    //------------------------------------------------------------------------------------------------

    [Header("inGame")]

    public bool isLiveClear;

    public int FinialTotleScore;
    public int FinialMaxComboCount;

    public int FinialPerfectCount;
    public int FinialGreatCount;
    public int FinialMissCount;

    public int SongMaxCombo;

    public string LiveFinalResult;

    public void SetFinialScore(inGame_ScoreManager scoreManager)
    {
        SongMaxCombo = scoreManager.SongMaxComboCount;
        FinialTotleScore = scoreManager.GetTotleScore();
        FinialMaxComboCount = scoreManager.GetMaxComboCount();
        FinialPerfectCount = scoreManager.GetPerfectCount();
        FinialGreatCount = scoreManager.GetGreatCount();
        FinialMissCount = scoreManager.GetMissCount();

        ExpCount = scoreManager.ExpCount;
        HoldCount = scoreManager.HoldCount;
        StarCount = scoreManager.StarCount;
        ScCount_Level1 = scoreManager.ScCount_Level1;
        ScCount_Level2 = scoreManager.ScCount_Level2;
        ScCount_Level3 = scoreManager.ScCount_Level3;
        ScCount_Level4 = scoreManager.ScCount_Level4;
        ScCount_Level5 = scoreManager.ScCount_Level5;
        ScCount_Level6 = scoreManager.ScCount_Level6;
        ScCount_Level7 = scoreManager.ScCount_Level1;
    }

    //------------------------------------------------------------------------------------------------

    [Header("SongMenu")]
    [SerializeField] private int SelectSongIndex = 0;//選譜畫面用
    [SerializeField] private MusicSO PlayMusicSO;
    [SerializeField] public string Level;
    [SerializeField] public int LevelNumber;
    [SerializeField] TextAsset SelectSongMusicSheet;
    public float DelayOffset;

    public void SetSelectSongIndex(int selectSongIndex)
    {
        SelectSongIndex = selectSongIndex;
    }
    public int GetSelectSongIndex()
    {
        return SelectSongIndex;
    }
    public void SetPlayMusicSO(MusicSO playMusicSO)
    {
        PlayMusicSO = playMusicSO;
    }
    public MusicSO GetPlayMusicSO()
    {
        return PlayMusicSO;
    }
    public void SetLevel(string level)
    {
        Level = level;
    }
    public string GetLevel()
    {
        return Level;
    }
    public void SetLevelNumber(int levelNumber)
    {
        LevelNumber = levelNumber;
    }
    public int GetLevelNumber()
    {
        return LevelNumber;
    }
    public void SetSelectSongMusicSheet(TextAsset selectSongMusicSheet)
    {
        SelectSongMusicSheet = selectSongMusicSheet;
    }
    public TextAsset GetSelectSongMusicSheet()
    {
        return SelectSongMusicSheet;
    }
    public void SetMvOn(bool statue)
    {
        isMvOn = statue;
    }
    public bool GetMvStatue()
    {
        return isMvOn;
    }
    public void SetDropSpeed(int dropSpeed)
    {
        DropSpeed = dropSpeed;
    }
    public int GetDropSpeed()
    {
        return DropSpeed;
    }





    //------------------------------------------------------------------------------------------------
    [Header("Live Clear Menu")]

    public int ExpCount;
    public int HoldCount;
    public int StarCount;
    public int ScCount_Level1;
    public int ScCount_Level2;
    public int ScCount_Level3;
    public int ScCount_Level4;
    public int ScCount_Level5;
    public int ScCount_Level6;
    public int ScCount_Level7;
    public int TrashCount;//未使用

    public void AddEconomyValueOnLiveClear()
    {
        int addExp = 0;
        addExp += ExpCount;
        addExp += HoldCount*2;

        int addStar = 0;
        addStar += StarCount;

        int addScCount = 0;
        addScCount += ScCount_Level1 * 15;
        addScCount += ScCount_Level2 * 30;
        addScCount += ScCount_Level3 * 75;
        addScCount += ScCount_Level3 * 75;
        addScCount += ScCount_Level4 * 150;
        addScCount += ScCount_Level5 * 300;
        addScCount += ScCount_Level6 * 750;
        addScCount += ScCount_Level7 * 1500;

        Economy_Exp += addExp;
        Economy_Star += addStar;
        Economy_ScNum += addScCount;
        /*
        ExpCount = 0;
        HoldCount = 0;
        StarCount = 0;
        ScCount_Level1 = 0;
        ScCount_Level2 = 0;
        ScCount_Level3 = 0;
        ScCount_Level4 = 0;
        ScCount_Level5 = 0;
        ScCount_Level6 = 0;
        ScCount_Level7 = 0;
        */
    }

    [Header("Rasing Menu")]

    public int Economy_Exp;
    public int Economy_Star;


    [Header("Gachapon Menu")]
    public int Economy_ScNum;


    public void saveData()
    {
        _gameData.SetData(this);
    }
    public void ResetPlayerData()
    {
        Economy_Exp = 10000000;
        Economy_ScNum = 1000000000;
        Economy_Star = 100000000;
    }


    private void OnDestroy()
    {
        saveData();
    }
}
