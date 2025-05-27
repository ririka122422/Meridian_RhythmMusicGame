using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManu_DaskTable : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    public MainMenu_SceneManager _mmSceneManager;
    private GameAudioManager _gameAudioManager;

    public Button SongMenu;
    public Button RaisingMenu;
    public Button Gachapon;
    public Button Setting;

    public GameObject SettingPanel;

    public Image SongMenuImage;
    public Image RaisingMenuImage;
    public Image GachaponImage;

    public Image illustration;

    public Image BackGround;
    public Image BackGroundWhith;
    public Image BackGroundCharectorLogo;
    public Image BackGroundCenter;

    public Animator Transition;

    [SerializeField] private CharacterListSO CharacterList;
    [SerializeField] private List<CharacterSO> CharacterSOList;

    public MusicBgmListSO BgmPool;
    private MusicBgmSO PlayingBgm = null;

    [Header("Debug")]
    public Button GoTestScene;

    private void Start()
    {

        SettingPanel.SetActive(false);

        SongMenu.onClick.AddListener(OnClickSongMenuButton);
        RaisingMenu.onClick.AddListener(OnClickRaisingMenuButton);
        Gachapon.onClick.AddListener(OnClickGachaponMenuButton);
        Setting.onClick.AddListener(OnclickSettingButton);

        GoTestScene.onClick.AddListener(() => _gameSceneManager.SwichScene("TestScene"));
    }

    public void OnEnterMenu()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        if(PlayingBgm == null)
        {
            PlayingBgm = BgmPool.GetRendomBgm();
            _gameAudioManager.SetBgmMusic(PlayingBgm.Bgm, 0);
        }
        else
        {
            _gameAudioManager.SetBgmMusic(PlayingBgm.Bgm, PlayingBgm.GetClipTime());
        }

        print("DaskTable Load!");
        RendomUiStyle();
        Transition.SetTrigger("SlideIn");

        _gameSceneManager.saveData();
        //_gameAudioManager.SetBgmMusic();
    }

    public void OnExitMenu()
    {
        
        float nowBgmTime = _gameAudioManager.GetPlayingAudioTime();
        PlayingBgm.SaveClipTime(nowBgmTime);
        print(nowBgmTime);
        _gameAudioManager.StopBgm();
    }

    public void RendomUiStyle()
    {
        CharacterSOList = CharacterList.CharacterList;
        int CharacterIndex = Random.Range(0, CharacterSOList.Count);
        CharacterSO charactor = CharacterSOList[CharacterIndex];

        illustration.sprite = charactor.Illustration;
        SongMenuImage.sprite = charactor.SongMenuImage;
        RaisingMenuImage.sprite = charactor.RaisingMenuImage;
        GachaponImage.sprite = charactor.GachaponImage;


        BackGround.sprite = charactor.BackGround;
        BackGroundWhith.sprite = charactor.BackGroundWhith;
        BackGroundCharectorLogo.sprite = charactor.BackGroundCharectorLogo;
        BackGroundCenter.sprite = charactor.BackGroundCenter;
    }

    public void OnclickSettingButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        SettingPanel.SetActive(true);
    }

    public void OnClickSongMenuButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _mmSceneManager.SwichMenu("SongMenu");
    }
    public void OnClickRaisingMenuButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _mmSceneManager.SwichMenu("RaisingMenu");
    }
    public void OnClickGachaponMenuButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _mmSceneManager.SwichMenu("Gachapon");
    }
    private void OnDestroy()
    {
        SongMenu.onClick.RemoveAllListeners();
        RaisingMenu.onClick.RemoveAllListeners();
        Gachapon.onClick.RemoveAllListeners();
        Setting.onClick.RemoveAllListeners();

        GoTestScene.onClick.RemoveAllListeners();
    }
}
