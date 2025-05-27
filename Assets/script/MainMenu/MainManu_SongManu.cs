using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManu_SongManu : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;

    public MainMenu_SceneManager _mmSceneManager;
    public SongMenu_SongList songList;

    public Text SongName;
    public Text SongVocName;
    public Text HightScore;

    public Button EastMode;
    public Button NormalMode;
    public Button HardMode;
    public Text EastLevel;
    public Text NormalLevel;
    public Text HardLevel;

    public Image HintArrow;
    public RectTransform EasyArrowTransform;
    public RectTransform NormalArrowTransform;
    public RectTransform HardArrowTransform;

    public Image LevelPanel;
    public Sprite EasyPanel;
    public Sprite NormalPanel;
    public Sprite HardPanel;

    public Button StartGameButton;
    public Button BackButton;
    public Button GoToEditer;

    private MusicSO OnSelectMusicSo;

    private void Start()
    {
        
        BackButton.onClick.AddListener(OnClickBackButton);

        EastMode.onClick.AddListener(OnClickLevelEasy);
        NormalMode.onClick.AddListener(OnClickLevelNormal);
        HardMode.onClick.AddListener(OnClickLevelHard);

        StartGameButton.onClick.AddListener(OnClickStartGame);
        GoToEditer.onClick.AddListener(OnClickGoToEditer);
    }

    public void OnEnterMenu()
    {
        print("SongManu Load!"); 
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        songList.initialize();

    }
    public void OnExitMenu()
    {

    }

    public void OnClickBackButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _mmSceneManager.SwichMenu("DaskTable");
    }
    private void OnClickStartGame()
    {
        //µù¥U¿ï¾Üªº¦±¥Ø
        _gameAudioManager.PlayOnClickButtonSfx();
        _gameSceneManager.OnClickSwichScene("inGameScene");
    }
    private void OnClickGoToEditer()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _gameSceneManager.OnClickSwichScene("MusicSheetSetupScene");
    }

    public void OnClickLevelEasy()
    {
        _gameAudioManager.PlayOnClickButtonSfx(); 
        OnLevelChange("Easy"); 
    }
    public void OnClickLevelNormal()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        OnLevelChange("Normal");
    }
    public void OnClickLevelHard()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        OnLevelChange("Hard"); 
    }


    public void OnSelectSongChange(MusicSO musicSO)
    {
        OnSelectMusicSo = musicSO;
        ChackMusicSheetExistable(musicSO);
        OnLevelChange(_gameSceneManager.GetLevel());

        if(OnSelectMusicSo.MusicPreview != null)
        {
            _gameAudioManager.SetBgmImmediately(OnSelectMusicSo.MusicPreview, 0);
        }
        else
        {
            _gameAudioManager.StopBgm();
        }
        

        SongName.text = $"{musicSO.MusicDisplayName}";
        SongVocName.text = $"{musicSO.Vocal}";
        //HightScore.text = $"{musicSO.MusicDisplayName}";
    }

    public void OnLevelChange(string Level)
    {
        switch (Level)
        {
            case "Easy":
                LevelPanel.sprite = EasyPanel;
                HintArrow.transform.position = EasyArrowTransform.position;
                _gameSceneManager.SetLevel(Level);
                _gameSceneManager.SetLevelNumber(OnSelectMusicSo.EasyLevel);
                _gameSceneManager.SetSelectSongMusicSheet(OnSelectMusicSo.Easy);
                break;
            case "Normal":
                LevelPanel.sprite = NormalPanel;
                HintArrow.transform.position = NormalArrowTransform.position;
                _gameSceneManager.SetLevel(Level);
                _gameSceneManager.SetLevelNumber(OnSelectMusicSo.NormalLevel);
                _gameSceneManager.SetSelectSongMusicSheet(OnSelectMusicSo.Normal);
                break;
            case "Hard":
                LevelPanel.sprite = HardPanel;
                HintArrow.transform.position = HardArrowTransform.position;
                _gameSceneManager.SetLevel(Level);
                _gameSceneManager.SetLevelNumber(OnSelectMusicSo.HardLevel);
                _gameSceneManager.SetSelectSongMusicSheet(OnSelectMusicSo.Hard);
                break;
        }
        
    }

    private List<string> ChackMusicSheetExistable(MusicSO musicSO)
    {
        List<string> ExistableLevel = new List<string>();

        if (musicSO.EasyPlayable == true) 
        {
            EastMode.interactable = true;
            EastLevel.text = $"{OnSelectMusicSo.EasyLevel}";
            ExistableLevel.Add("Easy"); 
        }
        else {  EastMode.interactable = false; EastLevel.text = $""; }

        if (musicSO.NormalPlayable == true) 
        { 
            NormalMode.interactable = true;
            NormalLevel.text = $"{OnSelectMusicSo.NormalLevel}";
            ExistableLevel.Add("Normal"); 
        }
        else { NormalMode.interactable = false; NormalLevel.text = $""; }

        if (musicSO.HardPlayable == true) 
        { 
            HardMode.interactable = true;
            HardLevel.text = $"{OnSelectMusicSo.HardLevel}";
            ExistableLevel.Add("Hard"); 
        }
        else { HardMode.interactable = false; HardLevel.text = $""; }



        if (ExistableLevel.Count == 1)
        {
            OnLevelChange(ExistableLevel[0]);
        }
        /*
        if(_gameSceneManager.GetSelectSongMusicSheet() == null)
        {
            if (ExistableLevel.Count >= 1)
            {
                OnLevelChange(ExistableLevel[0]);
            }
        }
        */
        if (ExistableLevel.Count <= 0)
        {
            StartGameButton.interactable = false;
        }
        else
        {
            StartGameButton.interactable = true;
        }

        return ExistableLevel;
    }

    private void OnDestroy()
    {
        BackButton.onClick.RemoveAllListeners();

        EastMode.onClick.RemoveAllListeners();
        NormalMode.onClick.RemoveAllListeners();
        HardMode.onClick.RemoveAllListeners();

        StartGameButton.onClick.RemoveAllListeners();
        GoToEditer.onClick.RemoveAllListeners();
    }
}
