using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManu_SettingPanel : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameAudioManager _gameAudioManager;

    public Button Back;

    public Text Text_MusicVolum;
    public Button incMusicVolum;
    public Button decMusicVolum;

    public Text Text_SfxVolum;
    public Button incSfxVolum;
    public Button decSfxVolum;

    public Text Text_DropSpeed;
    public Button incDropSpeed;
    public Button decDropSpeed;

    public Text Text_MusicTimeOffset;
    public Button incMusicTimeOffset;
    public Button decMusicTimeOffset;

    public Button MvSwicher;
    public Image MvSwicherImg;
    public Sprite MvOn;
    public Sprite MvOff;

    private void Start()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        Back.onClick.AddListener(OnClickBackButton);

        incMusicVolum.onClick.AddListener(() => OnClickSetMusiceVolum(1));
        decMusicVolum.onClick.AddListener(() => OnClickSetMusiceVolum(-1));

        incSfxVolum.onClick.AddListener(() => OnClickSetSfxVolum(1));
        decSfxVolum.onClick.AddListener(() => OnClickSetSfxVolum(-1));

        incDropSpeed.onClick.AddListener(() => OnClickSetDropSpeed(1));
        decDropSpeed.onClick.AddListener(() => OnClickSetDropSpeed(-1));

        incMusicTimeOffset.onClick.AddListener(() => OnClickSetMusicTimeOffset(10));
        decMusicTimeOffset.onClick.AddListener(() => OnClickSetMusicTimeOffset(-10));

        MvSwicher.onClick.AddListener(OnClickChangeMvStatue);

        if(_gameSceneManager.isMvOn)
        {
            MvSwicherImg.sprite = MvOn;
        }
        else
        {
            MvSwicherImg.sprite = MvOff;
        }

        Text_MusicVolum.text = $"{_gameSceneManager.MusicVolum}";
        Text_SfxVolum.text = $"{_gameSceneManager.SfxVolume}";
        Text_DropSpeed.text = $"{_gameSceneManager.DropSpeed}";
        Text_MusicTimeOffset.text = $"{_gameSceneManager.MusicTimeOffset}ms";
    }

    public void OnClickBackButton()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        gameObject.SetActive(false);
    }
    public void OnClickSetMusiceVolum(int value)
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        SetMusiceVolum(value);
    }
    public void OnClickSetSfxVolum(int value)
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        SetSfxVolum(value);
    }
    public void OnClickSetDropSpeed(int value)
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        SetDropSpeed(value);
    }
    public void OnClickSetMusicTimeOffset(float value)
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        SetMusicTimeOffset(value);
    }
    public void OnClickChangeMvStatue()
    {
        _gameAudioManager.PlayOnClickButtonSfx();
        ChangeMvStatue();
    }


    public void SetMusiceVolum(int value)
    {
        int Volum = _gameSceneManager.MusicVolum;
        Volum += value;
        if (Volum < 0)
        {
            Volum = 10;
        }
        else if (Volum > 10)
        {
            Volum = 0;
        }
        _gameSceneManager.MusicVolum = Volum;
        _gameAudioManager.SetBgmVolum(Volum);

        Text_MusicVolum.text = $"{_gameSceneManager.MusicVolum}";
    }

    public void SetSfxVolum(int value)
    {
        int Volum = _gameSceneManager.SfxVolume;
        Volum += value;
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
    public void SetDropSpeed(int value)
    {
        int DropSpeed = _gameSceneManager.DropSpeed;
        DropSpeed += value;
        if (DropSpeed < 1)
        {
            DropSpeed = 6;
        }
        else if (DropSpeed > 6)
        {
            DropSpeed = 1;
        }
        _gameSceneManager.DropSpeed = DropSpeed;
        _gameSceneManager.SetDropSpeed(DropSpeed);
        Text_DropSpeed.text = $"{_gameSceneManager.GetDropSpeed()}";
    }
    public void SetMusicTimeOffset(float value)
    {
        float TimeOffset = _gameSceneManager.MusicTimeOffset;
        TimeOffset += value;
        if (TimeOffset < 0)
        {
            TimeOffset = 1000;
        }
        else if (TimeOffset > 1000)
        {
            TimeOffset = 0;
        }
        _gameSceneManager.MusicTimeOffset = TimeOffset;
        Text_MusicTimeOffset.text = $"{_gameSceneManager.MusicTimeOffset}ms";
    }

    public void ChangeMvStatue()
    {
        bool isMvOn = _gameSceneManager.GetMvStatue();
        isMvOn = !isMvOn;

        if (isMvOn == true)
        {
            MvSwicherImg.sprite = MvOn;
        }
        else
        {
            MvSwicherImg.sprite = MvOff;
        }
        _gameSceneManager.SetMvOn(isMvOn);
    }

    private void OnDestroy()
    {
        Back.onClick.RemoveAllListeners();

        incMusicVolum.onClick.RemoveAllListeners();
        decMusicVolum.onClick.RemoveAllListeners();

        incSfxVolum.onClick.RemoveAllListeners();
        decSfxVolum.onClick.RemoveAllListeners();

        incDropSpeed.onClick.RemoveAllListeners();
        decDropSpeed.onClick.RemoveAllListeners();

        incMusicTimeOffset.onClick.RemoveAllListeners();
        decMusicTimeOffset.onClick.RemoveAllListeners();

        MvSwicher.onClick.RemoveAllListeners();
    }
}
