using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManu_RaisingMenu : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;
    private GameSceneUiManager _gameSceneUiManager;

    public MainMenu_SceneManager _mmSceneManager;

    public CharacterSO OnSelectCharacter;
    public RaisingMenu_CharactorPanal OnSelectPanel;

    public Button BackButton;

    public Text MyStarNum;
    public Text MyExpNum;

    public GameObject CharaterListPanel;
    public GameObject CharaterDetialPanel;

    public Button CloseDetialPanelButton;

    public GameObject CharaterPanelPrefab;
    public List<Transform> PanelTransform;


    [Header("Detial Panel")]
    public Text CharacterName;
    public Text CardName;
    public Image CharacterIllustration;
    public Button ApplyExpValue;
    public Button ResetUpgeatValue;
    public Button AutoUpgreat;

    [Header("Level")]
    public Text CharaterLevelText;
    public Text CharaterNowLevelExpText;

    public int OnSelectExpNum = 0;
    public Text OnSelectExpNumText;
    public Button incOnSelectExp;
    public Button decOnSelectExp;


    [Header("Favorability Level")]
    public Text CharaterFavorabilityLevelText;
    public Text CharaterNowFavorabilityLevelExpText;

    public int OnSelectStarsNum = 0;
    public Text OnSelectStarsNumText;
    public Button incOnSelectStars;
    public Button decOnSelectStars;


    [Header("Menu Setting")]

    public MusicBgmListSO BgmPool;
    private MusicBgmSO PlayingBgm = null;

    private void Start()
    {
        BackButton.onClick.AddListener(OnClickBackButton);
        CloseDetialPanelButton.onClick.AddListener(OnClickCloseDetialPanelButton);

        incOnSelectExp.onClick.AddListener(() => SetOnSelectExpNum(100));
        decOnSelectExp.onClick.AddListener(() => SetOnSelectExpNum(-100));
        incOnSelectStars.onClick.AddListener(() => SetOnSelectStarNum(10));
        decOnSelectStars.onClick.AddListener(() => SetOnSelectStarNum(-10));

        ApplyExpValue.onClick.AddListener(OnClickUpgreatApple);
        ResetUpgeatValue.onClick.AddListener(OnClickResetUpgreat);
        AutoUpgreat.onClick.AddListener(OnClickAutoUpgrate);

        GenerateCharaterPanel();
        UpdateEconomyUi();

        CharaterListPanel.SetActive(true);
        CharaterDetialPanel.SetActive(false);

        OnSelectCharacter = null;
    }

    public void OnEnterMenu()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        PlayingBgm = BgmPool.GetRendomBgm();
        _gameAudioManager.SetBgmMusic(PlayingBgm.Bgm, 0);
        print("RaisingMenu Load!");
    }

    private void GenerateCharaterPanel()
    {
        PlayerGameData _gameData = _gameSceneManager._gameData;
        List<GameAvailableCharacter> MeAvailableCharacter = _gameData.GetAvailableCharacter();

        for (int i = 0; i < MeAvailableCharacter.Count; i++)
        {
            GameObject newCharaterPanelObject = Instantiate(CharaterPanelPrefab, PanelTransform[i]);

            RaisingMenu_CharactorPanal newCharaterPanel = newCharaterPanelObject.GetComponent<RaisingMenu_CharactorPanal>();
            newCharaterPanel._RaisingMenu = this;
            newCharaterPanel.AutoSetup(MeAvailableCharacter[i].Character);
        }
    }

    public void UpdateEconomyUi()
    {
        MyStarNum.text = $"{_gameSceneManager.Economy_Star}";
        MyExpNum.text = $"{_gameSceneManager.Economy_Exp}";
    }
    //當點進腳色詳細資料
    public void ShowCharacterDetails(RaisingMenu_CharactorPanal OnSelectPanel)
    {
        this.OnSelectPanel = OnSelectPanel;
        OnSelectCharacter = OnSelectPanel.CharacterSO;

        CharaterListPanel.SetActive(false);
        CharaterDetialPanel.SetActive(true);

        CharacterName.text = OnSelectCharacter.CharacterType.ToString();
        CardName.text = OnSelectCharacter.CharacterCardName;
        CharacterIllustration.sprite = OnSelectCharacter.Illustration;

        OnSelectExpNum = 0;
        OnSelectExpNumText.text = $"{0}";
        OnSelectStarsNum = 0;
        OnSelectStarsNumText.text = $"{0}";

        CharaterLevelText.text = $"Lv.  {OnSelectCharacter.Level}/{OnSelectCharacter.MaxLevel}";
        CharaterNowLevelExpText.text = $"{OnSelectCharacter.nowLevelExp}/{OnSelectCharacter.upgrateNeedExpNum}";
        CharaterFavorabilityLevelText.text = $"Lv. {OnSelectCharacter.FavorabilityLevel}/{OnSelectCharacter.MaxFavorabilityLevel}";
        CharaterNowFavorabilityLevelExpText.text = $"{OnSelectCharacter.nowFavorabilityExp}/{OnSelectCharacter.upgrateNeedFavorabilityExpNum}";

        print($"show {OnSelectCharacter.CharacterCardName} details");
    }

    public void OnClickBackButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        _mmSceneManager.SwichMenu("DaskTable");
    }

    public void OnClickCloseDetialPanelButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        CharaterListPanel.SetActive(true);
        CharaterDetialPanel.SetActive(false);
    }


    //點擊確認升級腳色
    public void OnClickUpgreatApple()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        OnSelectCharacter.AddExp(OnSelectExpNum);
        OnSelectCharacter.AddFavorability(OnSelectStarsNum);

        _gameSceneManager.Economy_Exp -= OnSelectExpNum;
        _gameSceneManager.Economy_Star -= OnSelectStarsNum;
        //歸零升級數量
        OnSelectExpNum = 0;
        OnSelectExpNumText.text = $"{OnSelectExpNum}";
        OnSelectStarsNum = 0;
        OnSelectStarsNumText.text = $"{OnSelectStarsNum}";
        //更新升級後的狀態
        CharaterLevelText.text = $"Lv. {OnSelectCharacter.Level}/{OnSelectCharacter.MaxLevel}";
        CharaterNowLevelExpText.text = $"{OnSelectCharacter.nowLevelExp}/{OnSelectCharacter.upgrateNeedExpNum}";
        CharaterFavorabilityLevelText.text = $"Lv. {OnSelectCharacter.FavorabilityLevel}/{OnSelectCharacter.MaxFavorabilityLevel}";
        CharaterNowFavorabilityLevelExpText.text = $"{OnSelectCharacter.nowFavorabilityExp}/{OnSelectCharacter.upgrateNeedFavorabilityExpNum}";

        OnSelectPanel.AutoSetup(OnSelectCharacter);
        UpdateEconomyUi();
        _gameSceneManager.saveData();
    }

    public void OnClickResetUpgreat()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        OnSelectExpNum = 0;
        OnSelectExpNumText.text = $"{OnSelectExpNum}";
        OnSelectStarsNum = 0;
        OnSelectStarsNumText.text = $"{OnSelectStarsNum}";
    }

    public void OnClickAutoUpgrate()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        int RemainingExp = OnSelectCharacter.GetRemainingExp();
        int RemainingFavorabilityExp = OnSelectCharacter.GetRemainingFavorabilityExp();

        SetOnSelectExpNum(RemainingExp);
        SetOnSelectStarNum(RemainingFavorabilityExp);
    }

    public void SetOnSelectExpNum(int addValue)
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        OnSelectExpNum += addValue;

        if (OnSelectExpNum < 0)
        {
            OnSelectExpNum = 0;
        }
        //避免付超過存款
        else if (OnSelectExpNum >= _gameSceneManager.Economy_Exp)
        {
            OnSelectExpNum = _gameSceneManager.Economy_Exp;
        }
        //避免經驗溢出被吃貨幣
        if (OnSelectExpNum >= OnSelectCharacter.GetRemainingExp())
        {
            OnSelectExpNum = OnSelectCharacter.GetRemainingExp();
        }

        OnSelectExpNumText.text = $"{OnSelectExpNum}";
    }
    public void SetOnSelectStarNum(int addValue)
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        OnSelectStarsNum += addValue;

        if (OnSelectStarsNum < 0)
        {
            OnSelectStarsNum = 0;
        }
        //避免付超過存款
        else if (OnSelectStarsNum >= _gameSceneManager.Economy_Star)
        {
            OnSelectStarsNum = _gameSceneManager.Economy_Star;
        }
        //避免經驗溢出被吃貨幣
        if (OnSelectStarsNum >= OnSelectCharacter.GetRemainingFavorabilityExp())
        {
            OnSelectStarsNum = OnSelectCharacter.GetRemainingFavorabilityExp();
        }

        OnSelectStarsNumText.text = $"{OnSelectStarsNum}";
    }

    public void OnExitMenu()
    {

    }

    private void OnDestroy()
    {
        BackButton.onClick.RemoveAllListeners();
        CloseDetialPanelButton.onClick.RemoveAllListeners();

        incOnSelectExp.onClick.RemoveAllListeners();
        decOnSelectExp.onClick.RemoveAllListeners();
        incOnSelectStars.onClick.RemoveAllListeners();
        decOnSelectStars.onClick.RemoveAllListeners();

        ApplyExpValue.onClick.RemoveAllListeners();
        ResetUpgeatValue.onClick.RemoveAllListeners();
        AutoUpgreat.onClick.RemoveAllListeners();
    }
}
