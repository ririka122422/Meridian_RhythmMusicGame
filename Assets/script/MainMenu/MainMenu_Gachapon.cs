using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Gachapon : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;
    private GameSceneUiManager _gameSceneUiManager;

    public MainMenu_SceneManager _mmSceneManager;

    public int gachaCost = 10000;

    public Button SoloGacha;
    public Button TenGacha;

    public Button SoloGachaAgain;
    public Button TenGachaAgain;

    public Button BackButton;
    public Button Back2GachaMenuButton;
    public Text Economy_ScNumText;

    public MusicBgmListSO BgmPool;
    private MusicBgmSO PlayingBgm = null;

    public GameObject GachaPanel;
    public GameObject GachaResultPanel;

    public List<Image> poolResultIcons;

    //分級未詳細設定，待改善
    public List<CharacterSO> SSR;
    public List<CharacterSO> SR;
    public List<CharacterSO> R;


    private void Start()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        BackButton.onClick.AddListener(() => _mmSceneManager.SwichMenu("DaskTable"));
        Back2GachaMenuButton.onClick.AddListener(BackToGachaMenu);

        SoloGacha.onClick.AddListener(() => StartGacha(1));
        TenGacha.onClick.AddListener(() => StartGacha(10));
        SoloGachaAgain.onClick.AddListener(() => StartGacha(1));
        TenGachaAgain.onClick.AddListener(() => StartGacha(10));

        GachaPanel.SetActive(true);
        GachaResultPanel.SetActive(false);
    }

    public void OnEnterMenu()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        PlayingBgm = BgmPool.GetRendomBgm();
        _gameAudioManager.SetBgmMusic(PlayingBgm.Bgm, 0);
        ChackIsScEnough();
        UpdateScUi();

        print("Gachapon Load!");
    }

    public void OnExitMenu()
    {

    }
    //R 79 SR 18 SSR 3

    public void StartGacha(int poolCount)
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        ShowGachaResult(GetGachaResult(poolCount));
        GachaPanel.SetActive(false);
        GachaResultPanel.SetActive(true);
        _gameSceneManager.Economy_ScNum -= gachaCost * poolCount;

        GachaPanel.SetActive(false);
        GachaResultPanel.SetActive(true);

        UpdateScUi();
        ChackIsScEnough();
        _gameSceneManager.saveData();
    }

    private void ChackIsScEnough()
    {
        if(_gameSceneManager.Economy_ScNum < gachaCost)
        {
            SoloGacha.interactable = false;
            TenGacha.interactable = false;
            SoloGachaAgain.interactable = false;
            TenGachaAgain.interactable=false;
        }
        else if (_gameSceneManager.Economy_ScNum < gachaCost * 10)
        {
            SoloGacha.interactable = true;
            TenGacha.interactable = false; 
            SoloGachaAgain.interactable = true;
            TenGachaAgain.interactable = false;
        }
        else
        {
            SoloGacha.interactable = true;
            TenGacha.interactable = true; 
            SoloGachaAgain.interactable = true;
            TenGachaAgain.interactable = true;
        }
    }

    private List<CharacterSO> GetGachaResult(int poolCount)
    {
        List<CharacterSO> poolCharactor = new List<CharacterSO>();
        for (int i = 0; i < poolCount; i++)
        {
            int num = Random.Range(1,101);
            if(num >= 1 && num <= 79)//R
            {
                int randomIndex = Random.Range(0, R.Count);
                poolCharactor.Add(R[randomIndex]);
            }
            if (num >= 80 && num <= 97)//SR
            {
                int randomIndex = Random.Range(0, SR.Count);
                poolCharactor.Add(SR[randomIndex]);
            }
            if (num >= 98 && num <= 100)//SSR
            {
                int randomIndex = Random.Range(0, SSR.Count);
                poolCharactor.Add(SSR[randomIndex]);
            }
        }
        return poolCharactor;
    }

    private void ShowGachaResult(List<CharacterSO> GachaResultList)
    {
        for(int i = 0;i < poolResultIcons.Count;i++)
        {
            poolResultIcons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < GachaResultList.Count; i++)
        {
            poolResultIcons[i].gameObject.SetActive(true);
            poolResultIcons[i].sprite = GachaResultList[i].icon;
        }
    }

    public void UpdateScUi()
    {
        Economy_ScNumText.text = $"{_gameSceneManager.Economy_ScNum}";
    }

    private void BackToGachaMenu()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        GachaPanel.SetActive(true);
        GachaResultPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        BackButton.onClick.RemoveAllListeners();
        Back2GachaMenuButton.onClick.RemoveAllListeners();

        SoloGacha.onClick.RemoveAllListeners();
        TenGacha.onClick.RemoveAllListeners();
        SoloGachaAgain.onClick.RemoveAllListeners();
        TenGachaAgain.onClick.RemoveAllListeners();

    }
}
