using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class inGame_uiManager : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;
    public inGame_ScoreManager scoreManager;
    public inGame_GameManager gameManager;
    public inGame_MediaManager mediaManager;

    private Coroutine ShowResult;

    public Text Speed;

    [Header("inGameSetting")]
    public Image PausePanel;

    public Text Text_MusicVolum;
    public Button incMusicVolum;
    public Button decMusicVolum;

    public Text Text_SfxVolum;
    public Button incSfxVolum;
    public Button decSfxVolum;

    public Button PauseButton;
    public Button ExitButton;
    public Button ContinewButton;
    public Button RetryButton;

    [Header("Determination")]
    public Text DeterminationFastOrLateText;
    public Image DeterminationResultImg;
    public Image DeterminationFastOrLateImg;
    public Animator DeterminationResultAnimation;
    public Animator DeterminationFastOrLateImgAnimation;


    [Header("Score")]
    public Text score;
    public Text combo;

    public Image LiveFinalResult;
    public Sprite AllPerfect;
    public Sprite FullCombo;
    public Sprite LiveSuccess;


    [Header("Debug")]
    public Text debugNodeCount;
    
    public Image ComboText;
    public Image Thousands;
    public Image Hundreds;
    public Image Tenth;
    public Image single;
    public List<Sprite> Numbers;
    
    public List<RectTransform> SingleTransform;
    public List<RectTransform> TenthTransform;
    public List<RectTransform> HundredsTransform;
    public List<RectTransform> ThousandsTransform;
    

    private void Start()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();


        incMusicVolum.onClick.AddListener(() => OnClickSetInGameMusicVolum(1));
        decMusicVolum.onClick.AddListener(() => OnClickSetInGameMusicVolum(-1));

        incSfxVolum.onClick.AddListener(() => OnClickSetSfxVolum(1));
        decSfxVolum.onClick.AddListener(() => OnClickSetSfxVolum(-1));

        PauseButton.onClick.AddListener(gameManager.OnClickPuse);
        ExitButton.onClick.AddListener(gameManager.OnClickExit);
        ContinewButton.onClick.AddListener(gameManager.OnclickContinew);
        RetryButton.onClick.AddListener(gameManager.OnClickRetry);

        initialize();
    }

    public void initialize()
    {
        LiveFinalResult.gameObject.SetActive(false);
        PausePanel.gameObject.SetActive(false);

        DeterminationFastOrLateText.text = "";
        ShowResult = null;
        SetPauseButton(false);
        GetSpeed();//DB
        SetInGameMusicVolum(0);
        SetSfxVolum(0);
        ShowDeterminationResult("");
    }

    private void Update()
    {
        UpdateScoreUI();
        debugNodeCount.text =
            $"HaveDrop: {gameManager.dropper.HaveDropNode} \n" +
            $"NowGet: {scoreManager.PerfectCount + scoreManager.GreatCount + scoreManager.MissCount} \n" +
            $"Perfect :{scoreManager.PerfectCount} \n" +
            $"Great :{scoreManager.GreatCount} \n" +
            $"Miss :{scoreManager.MissCount} \n" +
            $"FullCombo :{scoreManager.SongMaxComboCount}";
    }

    public void SetPauseButton(bool statue)
    {
        PauseButton.interactable = statue;
    }

    private void GetSpeed()
    {
        Speed.text = $"Speed {gameManager.GetDropSpeed()}" ;
    }

    private void UpdateScoreUI()
    {
        int ComboCount = scoreManager.ComboCount;
        score.text = $"分數 : {scoreManager.TotleScore}";

        if (ComboCount <= 0)
        {
            ComboText.gameObject.SetActive(false);
            single.gameObject.SetActive(false);
            Tenth.gameObject.SetActive(false);
            Hundreds.gameObject.SetActive(false);
            Thousands.gameObject.SetActive(false);
        }
        else
        {
            ComboText.gameObject.SetActive(true);
            single.gameObject.SetActive(true);

            int singleIndex = ComboCount % 10;
            int tenthIndex = (ComboCount / 10) % 10;
            int hundredIndex = (ComboCount / 100) % 10;
            int thousandIndex = (ComboCount / 1000) % 10;

            single.sprite = Numbers[Mathf.Clamp(singleIndex, 0, Numbers.Count - 1)];
            Tenth.sprite = Numbers[Mathf.Clamp(tenthIndex, 0, Numbers.Count - 1)];
            Hundreds.sprite = Numbers[Mathf.Clamp(hundredIndex, 0, Numbers.Count - 1)];
            Thousands.sprite = Numbers[Mathf.Clamp(thousandIndex, 0, Numbers.Count - 1)];

            // 根據Combo數字長度顯示對應的UI
            if (ComboCount < 10)
            {
                Tenth.gameObject.SetActive(false);
                Hundreds.gameObject.SetActive(false);
                Thousands.gameObject.SetActive(false);

                single.transform.position = SingleTransform[0].position;
            }
            else if (ComboCount < 100)
            {
                Tenth.gameObject.SetActive(true);
                Hundreds.gameObject.SetActive(false);
                Thousands.gameObject.SetActive(false);

                single.transform.position = SingleTransform[1].position;
                Tenth.transform.position = TenthTransform[0].position;
            }
            else if (ComboCount < 1000)
            {
                Tenth.gameObject.SetActive(true);
                Hundreds.gameObject.SetActive(true);
                Thousands.gameObject.SetActive(false);

                single.transform.position = SingleTransform[2].position;
                Tenth.transform.position = TenthTransform[1].position;
                Hundreds.transform.position = HundredsTransform[0].position;
            }
            else
            {
                Tenth.gameObject.SetActive(true);
                Hundreds.gameObject.SetActive(true);
                Thousands.gameObject.SetActive(true);

                single.transform.position = SingleTransform[3].position;
                Tenth.transform.position = TenthTransform[2].position;
                Hundreds.transform.position = HundredsTransform[1].position;
                Thousands.transform.position = ThousandsTransform[0].position;
            }
        }

        // 顯示Combo文字
        combo.text = ComboCount > 0 ? $"Combo\n{ComboCount}" : "";
    }

    public void ShowPausePanel(bool statue)
    {
        PausePanel.gameObject.SetActive(statue);
    }

    public void ShowDeterminationResult(string Result)
    {
        if (ShowResult != null)
        {
            StopCoroutine(ShowResult);
            ShowResult = null;
        }
        ShowResult = StartCoroutine(DisplayResult(Result));
    }
    private IEnumerator DisplayResult(string Result)
    {
        switch (Result)
        {
            case "Great_Fast":
                DeterminationResultAnimation.SetTrigger("Great");
                DeterminationFastOrLateText.text = "<color=#1BB1C1> FAST </color>";
                break;
            case "Perfect":
                DeterminationResultAnimation.SetTrigger("Perfect");
                DeterminationFastOrLateText.text = "";
                //DeterminationFastOrLateImgAnimation.SetTrigger("null");
                break;
            case "Great_Late":
                DeterminationResultAnimation.SetTrigger("Great");
                DeterminationFastOrLateText.text = "<color=#E07780> LATE </color>";
                break;
            case "Miss":
                DeterminationResultAnimation.SetTrigger("Miss");
                DeterminationFastOrLateText.text = "";
                break;
            default:
                DeterminationResultImg.sprite = null;
                DeterminationFastOrLateImg.sprite = null;
                break;
        }

        yield return new WaitForSeconds(0.5f);
        DeterminationFastOrLateText.text = "";

        DeterminationResultImg.sprite = null;
        DeterminationFastOrLateImg.sprite = null;
    }

    public async Task DisplayLiveFinalResult(string liveFinalResult)
    {
        switch (liveFinalResult)
        {
            case "All_Perfect":
                LiveFinalResult.sprite = AllPerfect;
                break;
            case "Full_Combo":
                LiveFinalResult.sprite = FullCombo;
                break;
            case "Live_Success":
                LiveFinalResult.sprite = LiveSuccess;
                break;
        }
        await displayLiveFinalResult();
    }
    private async Task displayLiveFinalResult()
    {
        LiveFinalResult.gameObject.SetActive(true);
        await Task.Delay(2000);
        LiveFinalResult.gameObject.SetActive(false);
    }

    public void OnClickSetSfxVolum(int value)
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        SetSfxVolum(value);
    }
    public void OnClickSetInGameMusicVolum(int value)
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        SetInGameMusicVolum(value);
    }

    public void SetSfxVolum(int addValue)
    {
        int Volum = _gameSceneManager.SfxVolume;
        Volum += addValue;
        if (Volum < 0)
        {
            Volum = 10;
        }
        else if (Volum > 10)
        {
            Volum = 0;
        }
        _gameSceneManager.SfxVolume = Volum;
        _gameAudioManager.SetSfxVolum(Volum);

        Text_SfxVolum.text = $"{_gameSceneManager.SfxVolume}";
    }
    public void SetInGameMusicVolum(int addValue)
    {
        int Volum = _gameSceneManager.MusicVolum;
        Volum += addValue;
        if (Volum < 0)
        {
            Volum = 10;
        }
        else if (Volum > 10)
        {
            Volum = 0;
        }
        _gameSceneManager.MusicVolum = Volum;
        mediaManager.SetMusicVolum(Volum);

        Text_MusicVolum.text = $"{_gameSceneManager.MusicVolum}";
    }


    private void OnDestroy()
    {
        incMusicVolum.onClick.RemoveAllListeners();
        decMusicVolum.onClick.RemoveAllListeners();

        incSfxVolum.onClick.RemoveAllListeners();
        decSfxVolum.onClick.RemoveAllListeners();


        PauseButton.onClick.RemoveAllListeners();
        ExitButton.onClick.RemoveAllListeners();
        ContinewButton.onClick.RemoveAllListeners();
        RetryButton.onClick.RemoveAllListeners();
    }
}
