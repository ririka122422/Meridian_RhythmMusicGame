using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicSetup_Ui : MonoBehaviour
{
    public MusicSetup_Manager _MS;
    public MusicSetup_Editer _msEditer;

    public Text TextMusicName;
    public Text TextBPM;

    public Text nowBeat;
    public Text nowBar;
    public Text MusicLeng;

    public Text SliderText;

    public Text OnCallButtonINFO;

    public Button DemoButton;

    public SpriteRenderer tampler;

    public Slider BeatController;
    public Slider ShowSlider;//沒有功能顯示進度用的Slider

    //顯示儲存狀態
    public Text LoadSaveState;
    private Coroutine ShowLoadSaveState;

    //確認儲存
    public GameObject Panel_OnSaveConfirm;

    private void Start()
    {
        DemoButton.onClick.AddListener(OnClickGoDemoScene);
    }

    public void initialize()
    {
        TextMusicName.text = $"{_MS._gameSceneManager.GetPlayMusicSO().MusicDisplayName} / {_MS._gameSceneManager.GetSelectSongMusicSheet().name}";
        TextBPM.text = $"BPM : {_MS.MusicSO.bpm}";
        LoadSaveState.text = "";
        Panel_OnSaveConfirm.SetActive(false);
        BeatController.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void Update()
    {
        if(_MS.nowBeat%_MS.MusicSO.meter == 0)
        {
            tampler.color = Color.blue;
        }
        else
        {
            tampler.color = Color.white;
        }
        MusicLeng.text =     $"音樂時長    {_MS.nowBeat * _MS.beatleng} / {_MS.MusicLeng}";
        nowBeat.text =       $"節拍        {_MS.nowBeat % 4} > {_MS.nowBeat} / {_MS.beatNum} RealTime Beatleng:{_MS.RealtimeBeatLeng}";
        nowBar.text =        $"小節數      {_MS.nowBar} / {_MS.barNum}";

        if(_msEditer.onCallButton != null)
        {
            OnCallButtonINFO.text = $"This Node code\n{_msEditer.onCallButton.ButtonCode}";
        }
        else
        {
            OnCallButtonINFO.text = "";
        }

        SliderText.text =    $"{ShowSlider.value} / Beat : {_MS.nowBeat} / {_MS.beatNum}";
        ShowSlider.value = (float)_MS.nowBeat / _MS.beatNum;
    }

    void OnSliderValueChanged(float value)
    {
        int value2Beat = (int)(_MS.beatNum * value);
        _MS.SetNowBeat(value2Beat);
    }

    public void OnClickGoDemoScene()
    {
        _MS._gameSceneManager.OnClickSwichScene("inGameScene");
    }
    public void OnClickGoMusicSelect()
    {
        _MS._gameSceneManager.SetNextMenu("SongMenu");
        _MS._gameSceneManager.OnClickSwichScene("MainMenu");
    }

    public void ShowOnSaveConfirmPanal()
    {
        Panel_OnSaveConfirm.SetActive(true);
    }

    public void CloseOnSaveConfirmPanal(bool SaveConfirm)
    {
        if (SaveConfirm)
        {
            _msEditer.SaveSheetMusic();
        }
        else
        {
            
        }
        Panel_OnSaveConfirm.SetActive(false);
    }

    public void SetLoadSaveState(string State)
    {
        if (ShowLoadSaveState != null)
        {
            StopCoroutine(ShowLoadSaveState);
        }
        StartCoroutine(DisplayLoadSaveState(State));
    }

    private IEnumerator DisplayLoadSaveState(string State)
    {
        LoadSaveState.text = State;
        yield return new WaitForSeconds(2);
        LoadSaveState.text = "";
    }

}
