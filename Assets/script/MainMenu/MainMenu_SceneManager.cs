using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu_SceneManager : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    public GameAudioManager _gameAudioManager;

    public GameObject NowShowMenu;
    private string nowShowMenuName;

    public MainMenu_FirstTimeMenu _FirstTimeMenu;
    public MainManu_DaskTable _DaskTable;
    public MainManu_SongManu _MusicSelect;
    public MainMenu_Gachapon _Gachapon;
    public MainManu_LiveClear _LiveClear;
    public MainManu_RaisingMenu _RaisingMenu;
    public GameObject ManuTransitionObj;

    public Animator ManuTransition;
    public AnimationClip ManuTransitionEnterAnimation;

    private float SwichMenuCd = 0;
    private void Start()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        initialize();

        _gameAudioManager.SetBgmVolum(_gameSceneManager.MusicVolum);
        _gameAudioManager.SetSfxVolum(_gameSceneManager.SfxVolume);

        ChackNextMenuOnLoad();
    }
    private void initialize()
    {
        ManuTransitionObj.SetActive(true);
        NowShowMenu = null;
        _FirstTimeMenu.gameObject.SetActive(false);
        _DaskTable.gameObject.SetActive(false);
        _MusicSelect.gameObject.SetActive(false);
        _Gachapon.gameObject.SetActive(false);
        _LiveClear.gameObject.SetActive(false);
        _RaisingMenu.gameObject.SetActive(false);
    }

    private void ChackNextMenuOnLoad()
    {
        string nextMenu = _gameSceneManager.GetNextMenu();
        if (nextMenu == "null")
        {
            SwichMenu("DaskTable");//預設為桌面
            return;
        }
        else
        {
            SwichMenu(nextMenu);
        }
        _gameSceneManager.SetNextMenu("null");
    }

    public async void SwichMenu(string menuName)
    {
        if(SwichMenuCd > 0)
        {
            return;
        }
        SwichMenuCd = 2;

        ManuTransition.SetTrigger("Enter"); 
        //離開場景執行
        if (NowShowMenu != null)
        {
            switch (nowShowMenuName)
            {
                case "FirstTimeMenu":
                    _FirstTimeMenu.OnExitMenu();
                    break;
                case "DaskTable":
                    _DaskTable.OnExitMenu();
                    break;
                case "SongMenu":
                    _MusicSelect.OnExitMenu();
                    break;
                case "Gachapon":
                    _Gachapon.OnExitMenu();
                    break;
                case "LiveClear":
                    _LiveClear.OnExitMenu();
                    break;
                case "RaisingMenu":
                    _RaisingMenu.OnExitMenu();
                    break;
            }
        }
        
        await Task.Delay((int)(ManuTransitionEnterAnimation.length * 1000));
        if(NowShowMenu != null)
        { 
            NowShowMenu.SetActive(false);
            nowShowMenuName = menuName;
        }
        //進入場景執行
        switch (menuName)
        {
            case "FirstTimeMenu":
                NowShowMenu = _FirstTimeMenu.gameObject;
                NowShowMenu.SetActive(true);
                _FirstTimeMenu.OnEnterMenu();
                break;
            case "DaskTable":
                NowShowMenu = _DaskTable.gameObject;
                NowShowMenu.SetActive(true);
                _DaskTable.OnEnterMenu();
                break;
            case "SongMenu":
                NowShowMenu = _MusicSelect.gameObject;
                NowShowMenu.SetActive(true);
                _MusicSelect.OnEnterMenu();
                break;
            case "Gachapon":
                NowShowMenu = _Gachapon.gameObject;
                NowShowMenu.SetActive(true);
                _Gachapon.OnEnterMenu();
                break;
            case "LiveClear":
                NowShowMenu = _LiveClear.gameObject;
                NowShowMenu.SetActive(true);
                _LiveClear.OnEnterMenu();
                break;
            case "RaisingMenu":
                NowShowMenu = _RaisingMenu.gameObject;
                NowShowMenu.SetActive(true);
                _RaisingMenu.OnEnterMenu();
                break;
        }

        ManuTransition.SetTrigger("Release");

        SwichMenuCd = 0;
    }
}
