using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SongMenu_SongList : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;
    public MainManu_SongManu _SongMenu;

    [SerializeField] MusicListSO musicListSO;

    //存放位置
    public List<MusicSO> musicList;

    public List <Transform> FreamTransform;//畫框位置
    public List <SongMenu_Song> SongCards;//所有的歌曲
    public List <SongMenu_Song> OnShowSongCards;//遊戲畫面顯示中的歌曲

    public GameObject SongPanelPrefab;
    public Transform SongPanalCollection;

    private int nowSongIndex;

    public void initialize()
    {
        _gameSceneManager = GameObject.FindWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        musicList = musicListSO.GetMusicSO_List();
        GenerateSongPanel();
        nowSongIndex = _gameSceneManager.GetSelectSongIndex();
        SetOnShowFream(nowSongIndex);
    }

    private void GenerateSongPanel()
    {
        for (int i = 0; i < musicList.Count; i++)
        {
            MusicSO musicSO = musicList[i];
            GameObject newSongPanal = Instantiate(SongPanelPrefab, SongPanalCollection);
            SongMenu_Song SongMenu_Song = newSongPanal.GetComponent<SongMenu_Song>();

            SongMenu_Song.SetupPanel(musicSO);
            SongCards.Add(SongMenu_Song);

            newSongPanal.name = $"SongPanal_{musicSO.name}";
            newSongPanal.SetActive(false);
        }
    }
    public async void OnClickShowNextSongCards()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        nowSongIndex -= 1;
        if (nowSongIndex < 0)
        {
            nowSongIndex = SongCards.Count - 1;
        }
        await RotaRollList("RollingNext");
    }
    public async void OnClickShowPreviousSongCards()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        nowSongIndex += 1;
        if (nowSongIndex >= SongCards.Count)
        {
            nowSongIndex = 0;
        }
        await RotaRollList("RollingPrevious");
    }
    private async Task RotaRollList(string duration)
    {
        float targetAngle = duration == "RollingNext" ? -45f : 45f;
        float rotationX = 0f;

        while (Mathf.Abs(rotationX) < Mathf.Abs(targetAngle))
        {
            rotationX += targetAngle * Time.deltaTime * 10;
            gameObject.transform.rotation = Quaternion.Euler(rotationX, 0, 0);  // 使用Euler角旋轉
            await Task.Yield();
        }

        // 最後保證精確歸位，避免誤差
        SetOnShowFream(nowSongIndex);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void SetOnShowFream(int index)
    {
        _gameSceneManager.SetSelectSongIndex(nowSongIndex);
        _gameSceneManager.SetPlayMusicSO(GetOnSelectMusicSo());
        _SongMenu.OnSelectSongChange(GetOnSelectMusicSo());

        // 隱藏當前顯示的歌曲卡片
        for (int i = 0; i < OnShowSongCards.Count; i++)
        {
            OnShowSongCards[i].ReplaceToSongCardCollection(SongPanalCollection);
            OnShowSongCards[i].gameObject.SetActive(false);
        }
        OnShowSongCards.Clear();

        // 顯示新的卡片（前後各2個）
        for (int i = index - 2; i <= index + 2; i++)
        {
            int songIndex = (i + SongCards.Count) % SongCards.Count; // 避免索引越界
            SongCards[songIndex].gameObject.SetActive(true);
            OnShowSongCards.Add(SongCards[songIndex]);  // 更新正在顯示的列表
        }
        for (int i = 0; i < OnShowSongCards.Count; i++)
        {
            OnShowSongCards[i].SetFream(FreamTransform[i]);
        }
        print($"Song index {index} {SongCards[index].musicSO.MusicDisplayName}");


    }

    public MusicSO GetOnSelectMusicSo()
    {
        return SongCards[nowSongIndex].musicSO;
    }

}
