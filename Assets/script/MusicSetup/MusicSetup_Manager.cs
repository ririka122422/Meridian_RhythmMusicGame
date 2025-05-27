using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicSetup_Manager : MonoBehaviour
{
    public GameSceneManager _gameSceneManager;
    public GameSceneUiManager _gameSceneUiManager;

    public MusicSetup_Editer _msEditer;
    public MusicSetup_Ui _msSetupUi;
    public MusicSO MusicSO;
    AudioSource Audio;


    public int BeatIndex = 0;

    public float MusicLeng;
    public float BPM;
    public float beatleng;
    public int beatNum;//總拍數
    public int barNum;//小節數

    public int nowBeat;
    public int nowBar;
    public float RealtimeBeatLeng;
    public bool isPlaying;
    public bool isFinish;

    private void Start()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameSceneUiManager = GameObject.FindGameObjectWithTag("GameSceneUiManager").GetComponent<GameSceneUiManager>();
        Audio = GetComponent<AudioSource>();

        reloadMusicSO();
        Audio.clip = MusicSO.Music;
        _msSetupUi.initialize();
    }

    void reloadMusicSO()
    {
        MusicSO = _gameSceneManager.GetPlayMusicSO();

        initialize();
        setupParameter();

        _msEditer.SetEditFileName(_gameSceneManager.GetSelectSongMusicSheet());
        _msEditer.GenerateSheetMusic(beatNum);
        _msEditer.LoadSheetMusic();
    }

    void initialize()
    {
        Audio.Stop();
        isPlaying = false;
        isFinish = false;
        nowBeat = 0;
        nowBar = 0;
    }

    void setupParameter()
    {
        BPM = MusicSO.bpm;
        beatleng = 60 / BPM;
        beatNum = (int)(MusicSO.Music.length / beatleng) + MusicSO.BeatOffset - MusicSO.FinishBeatOffset;
        barNum = beatNum / MusicSO.meter;
        MusicLeng = MusicSO.Music.length - MusicSO.FinishBeatOffset*beatleng;

        print($"曲名 : {MusicSO.Music.name} / BPM : {BPM} / 拍長 : {beatleng} / 總拍數 : {beatNum} / 總小節數 : {barNum} ");
    }

    public void PauseMusic()
    {
        // 停止音樂並記錄進度
        Audio.Pause(); // 使用 Pause 而不是 Stop
        _msEditer.StopDisplaying();
        isPlaying = false;
        isFinish = false; // 停止播放，但不標記為完成
    }// Puse

    public void SetNowBeat(int nowBeat)//滑桿移動時跳至當前Beat
    {
        if (nowBeat <= 0)
        {
            nowBeat = 0;
        }
        else if (nowBeat >= beatNum)
        {
            nowBeat = beatNum;
        }
        PauseMusic();
        this.nowBeat = nowBeat;
        _msEditer.GoToNowBeat(nowBeat);
    }

    public async void StartAtNowBeat()
    {
        if (!isPlaying)
        {
            isPlaying = true;

            // 計算當前進度對應的播放時間
            float currentPlaybackTime = nowBeat * beatleng;

            // 將 AudioSource 的播放時間設置為進度對應的時間
            Audio.time = currentPlaybackTime;
            Audio.Play();

            // 啟動譜面顯示和計算拍點
            _msEditer.StartDisplayMoving(nowBeat);
            await CountBeat();
        }
    }// Play

    async Task CountBeat()
    {
        float counter = 0;

        // 從當前播放時間開始計算
        float startingTime = Audio.time;

        while (isPlaying && !isFinish)
        {
            counter += Time.deltaTime;

            // 如果達到一拍長度
            if (counter >= beatleng)
            {
                RealtimeBeatLeng = counter;
                counter -= beatleng;

                // 更新顯示和拍點
                _msEditer.DisplaySheetMusic(nowBeat);
                nowBeat += 1;
            }

            // 更新小節進度
            nowBar = nowBeat / MusicSO.meter;

            // 如果音樂結束，標記為完成
            if (nowBeat >= beatNum)
            {
                isFinish = true;
                isPlaying = false;
            }

            await Task.Yield();
        }

        //print("isFinish");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.mouseScrollDelta.y > 0)
        {
            nowBeat += 1;
            SetNowBeat(nowBeat);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.mouseScrollDelta.y < 0)
        {
            nowBeat -= 1;
            SetNowBeat(nowBeat);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q)) && isPlaying)
        {
            PauseMusic();
        }
        else if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q)) && !isPlaying)
        {
            StartAtNowBeat();
        }
    }


    private void OnDisable()
    {
        isFinish = true;
    }
}
