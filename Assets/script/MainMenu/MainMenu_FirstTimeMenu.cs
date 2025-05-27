using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_FirstTimeMenu : MonoBehaviour
{
    //劇情系統須完全重作!!!
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;

    public MainMenu_SceneManager _mmSceneManager;

    public TextAsset dialogTexts;

    public Button NextDialog;
    public bool isClickNextSentence = false;

    public Text CharactorNameText;
    public Text DialogText;

    public Animator TransitionAnimator;
    public Animator ReiAnimator;

    public MusicBgmListSO BgmPool;
    private MusicBgmSO PlayingBgm = null;

    public string[] paragraphs;

    private void Start()
    {
        
    }

    public void OnEnterMenu()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        CharactorNameText.text = "";
        DialogText.text = "";

        NextDialog.onClick.AddListener(GetOnClickNextSentence);
        PlayingBgm = BgmPool.GetRendomBgm();
        _gameAudioManager.SetBgmMusic(PlayingBgm.Bgm, 0);

        LoadNewDialog(dialogTexts);
    }
    public void OnExitMenu()
    {

    }
    public async void LoadNewDialog(TextAsset dialogTexts)
    {
        // 指定用 UTF-8 讀取
        using (StreamReader reader = new StreamReader(new MemoryStream(dialogTexts.bytes), Encoding.UTF8))
        {
            string paragraphText = reader.ReadToEnd();
            paragraphText = paragraphText.Replace("\r", "");
            string[] paragraphSentences = paragraphText.Split("\n");
            print($"劇本:{dialogTexts.name}");

            await DisplayDialog(paragraphSentences);
        }
    }
    private async Task DisplayDialog(string[] paragraphs)
    {
        CharactorNameText.text = "";
        DialogText.text = "";

        for (int i = 0; i < paragraphs.Length; i++)
        {
            string paragraphType = paragraphs[i].Split("&")[0];

            switch (paragraphType)
            {
                case "Text":
                    await DisplayText(paragraphs[i].Split("&")[1]);
                    break;
                case "Cg":
                    
                    if (paragraphs[i].Split("&")[1] == "ReiEnter")
                    {
                        ReiAnimator.SetTrigger("Enter");
                    }
                    if (paragraphs[i].Split("&")[1] == "Release")
                    {
                        TransitionAnimator.SetTrigger("Release");
                        ReiAnimator.SetTrigger("Release");
                    }
                    break;
                case "Finish":
                    //_gameSceneManager.isFirstTimePlaying = false;
                    _mmSceneManager.SwichMenu("DaskTable");
                    break;
                default:
                    Debug.LogError($"No paragraph type name \"{paragraphType}\"");
                    break;
            }
        }
    }

    public async Task DisplayText(string TextParagraph)
    {
        isClickNextSentence = false;

        string[] split = TextParagraph.Split('_');
        string charactorName = split[0];
        string content = split[1];
        
        // 初始化句子
        DialogText.text = ""; 
        CharactorNameText.text = charactorName;

        int SplitIndex = 0;

        // 逐字顯示文字
        while (SplitIndex < content.Length && isClickNextSentence != true)
        {
            DialogText.text += content[SplitIndex];
            SplitIndex += 1;
            await Task.Delay(50);  // 控制打字速度
        }
        DialogText.text = content;
        isClickNextSentence = false;
        // 等待玩家點擊繼續
        while (isClickNextSentence == false)
        {
            await Task.Yield();
        }
        DialogText.text = "";
        isClickNextSentence = false; // 恢復未點擊狀態
    }

    private void GetOnClickNextSentence()
    {
        isClickNextSentence = true;
    }


    private void OnDestroy()
    {
        NextDialog.onClick.RemoveAllListeners();
    }
}
