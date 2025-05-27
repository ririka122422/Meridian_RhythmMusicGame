using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class inGame_GameManager : MonoBehaviour
{
    public GameSceneManager _gameSceneManager;
    public GameSceneUiManager _gameSceneUiManager;
    public GameAudioManager _gameAudioManager;

    public inGame_MediaManager mediaManager;
    public inGame_uiManager uiManager;
    public inGame_ScoreManager ScoreManager;
    public inGame_Dropper dropper;

    private SheetMusic_Reader reader = new SheetMusic_Reader();
    public List<MusicSO> MusicList;

    public MusicSO DisplayingMusic;
    public TextAsset DisplayingMusicSheet;

    [HideInInspector]
    public int DelayMinSecond = 3000;
    private int DropSpeed;// delay time scal

    public float MusicLeng;
    public float BPM;
    public float beatleng;
    public int beatNum;
    public int barNum;
    public int SongMaxCombo;

    public int nowBeat;
    public int nowBar;
    public bool isPlaying;
    public bool isPause;
    public bool isDropping;
    public bool isPlayingMusic;
    public bool isFinish;

    public List<string> MusicSheet = new List<string>();//每格存放一列的譜，1/4拍中1到5軌的內容，0 = 空白、 1 = Tab、 2 = Tab 、 3 = Flick

    private void Start()
    {
        _gameSceneManager = GameObject.FindWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameSceneUiManager = GameObject.FindWithTag("GameSceneUiManager").GetComponent<GameSceneUiManager>();
        _gameAudioManager = GameObject.FindWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        _gameAudioManager.StopBgm();

        DisplayingMusic = _gameSceneManager.GetPlayMusicSO();
        DisplayingMusicSheet = _gameSceneManager.GetSelectSongMusicSheet();
        DropSpeed = _gameSceneManager.GetDropSpeed();
        LoadMusicSoData(DisplayingMusic, DisplayingMusicSheet);
        SetDropSpeed(DropSpeed);
    }

    private void LoadMusicSoData(MusicSO MusicSO, TextAsset MusicSheetData)
    {
        InitializeGameManager();
        SetupMusicParameter();
        LoadSheetMusic(MusicSheetData);
        
        mediaManager.SetMusicClip(MusicSO);
        mediaManager.SetMusicVolum(_gameSceneManager.MusicVolum);
        if(MusicSO.MV != null) 
        { mediaManager.SetVideoClip(MusicSO); };
    }

    private void LoadSheetMusic(TextAsset MusicSheetData)
    {
        MusicSheet.Clear();
        MusicSheet = reader.LoadSheetMusic(MusicSheetData);
    }
    private void InitializeGameManager()
    {
        isPlaying = false;
        isPause = false;
        isDropping = false;
        isFinish = false;
        nowBeat = 0;
        nowBar = 0;
        SongMaxCombo = reader.GetMaxNodeCount(DisplayingMusicSheet);
        ScoreManager.SongMaxComboCount = SongMaxCombo;
    }
    private void SetupMusicParameter()
    {
        BPM = DisplayingMusic.bpm;
        beatleng = 60 / BPM;
        beatNum = (int)(DisplayingMusic.Music.length / beatleng) + DisplayingMusic.BeatOffset - DisplayingMusic.FinishBeatOffset;
        barNum = beatNum / DisplayingMusic.meter;
        MusicLeng = DisplayingMusic.Music.length - DisplayingMusic.FinishBeatOffset * beatleng;

        print($"曲名 : {DisplayingMusic.Music.name} / BPM : {BPM} / 拍長 : {beatleng} / 總拍數 : {beatNum} / 總小節數 : {barNum} ");
    }

    //^ initialize setting ^
    //------------------------------------------------------------------------------------------------
    //v playing Action v

    private async void StartDisplay()
    {
        if ( isPlaying ) { return; }
        isPlaying = true; 
        isFinish = false;
        SetDropSpeed(DropSpeed);

        uiManager.initialize();
        ScoreManager.initialize();

        await Task.Delay(2000);//轉場後避免直接開始遊戲的冷卻時間

        StartDropNode();
        isDropping = true;
        await Task.Delay(DelayMinSecond / DropSpeed + (int)_gameSceneManager.MusicTimeOffset);//音符生成與撥放音樂時間差 + 耳機延遲偏移
        uiManager.SetPauseButton(true);
        mediaManager.StartMusic();
    }

    public void OnClickPuse()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        isPause = true;
        uiManager.ShowPausePanel(true);
        mediaManager.PauseMusic();
        Time.timeScale = 0;
    }
    public void OnclickContinew()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        isPause = false;
        uiManager.ShowPausePanel(false);
        mediaManager.ContinewMusic();
        Time.timeScale = 1;
    }
    public void OnClickExit()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        mediaManager.StopMusic();
        _gameSceneManager.SetNextMenu("SongMenu");
        _gameSceneManager.SwichScene("MainMenu");
    }
    public void OnClickRetry()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        mediaManager.StopMusic();
        _gameSceneManager.SwichScene("inGameScene");
    }


    /*
    public void OnCliclIncreaseSpeed()
    {
        StopDisplay();
        DropSpeed += 1;
        SetDropSpeed(DropSpeed);

        uiManager.initialize();
        ScoreManager.initialize();

    }
    public void OnClickDecreaseSpeed()
    {
        StopDisplay();
        DropSpeed -= 1;
        SetDropSpeed(DropSpeed);

        uiManager.initialize();
        ScoreManager.initialize();
    }
    */

    private async void StartDropNode()
    {
        isDropping = true;
        await ReadMusicSheet();
    }
    private async Task ReadMusicSheet()
    {
        int MusicSheetIndex = 0;
        float counter = 0;
        while (isDropping && !isFinish && MusicSheetIndex<MusicSheet.Count)
        {
            counter += Time.deltaTime;
            if (counter >= beatleng / 4)
            {
                dropper.DropNodes(MusicSheet[MusicSheetIndex]);
                MusicSheetIndex += 1;
                counter -= beatleng / 4;
                //print(MusicSheetIndex / 4);
            }
            await Task.Yield();
        }

        if (MusicSheetIndex >= MusicSheet.Count)//撥放到結束才算完成，不算中途退出
        {
            isFinish = true;
            OnFinishLive();
        }
    }
    private async void OnFinishLive()
    {
        string result = ScoreManager.GetLiveFinalResult();

        mediaManager.PlaySFX(result);
        await uiManager.DisplayLiveFinalResult(result);

        _gameSceneManager.LiveFinalResult = result;
        _gameSceneManager.SetFinialScore(ScoreManager);
        _gameSceneManager.SetNextMenu("LiveClear");
        _gameSceneManager.SwichScene("MainMenu");
    }

    private void Update()
    {
        if(_gameSceneUiManager.GetIsTransitionFinish() && !isPlaying && !isFinish )
        {
            StartDisplay();
        }

        if(!isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StartDisplay();
        }
    }

    public void SetDropSpeed(float dropSpeed)
    {
        if (dropSpeed >= 6)
        {
            DropSpeed = 6;
        }
        else if (dropSpeed <= 1)
        {
            DropSpeed = 1;
        }
        else
        {
            DropSpeed = (int)dropSpeed;
        }
        dropper.SetDropSpeed(DropSpeed);
    }
    public int GetDropSpeed()
    {
        return DropSpeed;
    }

    private void OnDisable()
    {
        isFinish = true;
    }
}
