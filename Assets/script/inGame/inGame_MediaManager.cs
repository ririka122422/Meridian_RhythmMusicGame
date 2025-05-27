using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using UnityEngine.Video;

public class inGame_MediaManager : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;

    [SerializeField] AudioSource Music;
    [SerializeField] VideoPlayer MvPlayer;
    [SerializeField] RawImage MvScreen;
    [SerializeField] RenderTexture MvVideoRenderer;

    [SerializeField] AudioSource[] SFX_TapNull = new AudioSource[6];
    [SerializeField] AudioSource[] SFX_TapNormal = new AudioSource[6];
    [SerializeField] AudioSource[] SFX_TapStar = new AudioSource[6];
    [SerializeField] AudioSource[] SFX_FlickSc = new AudioSource[6];
    [SerializeField] AudioSource[] SFX_FlickTrash = new AudioSource[6];
    [SerializeField] AudioSource[] SFX_Hold = new AudioSource[6];

    private int SFX_TapNullIndex;
    private int SFX_TapNormalIndex;
    private int SFX_TapStarIndex;
    private int SFX_FlickScIndex;
    private int SFX_FlickTrashIndex;
    private int SFX_HoldIndex;

    void Start()
    {
        _gameSceneManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>();

        SFX_TapNullIndex = 0;
        SFX_TapNormalIndex = 0;
        SFX_TapStarIndex = 0;
        SFX_FlickScIndex = 0;
        SFX_FlickTrashIndex = 0;
        SFX_HoldIndex = 0;

        MvScreen.enabled = false;
        MvPlayer.enabled = _gameSceneManager.GetMvStatue();
    }

    public void StartMusic()
    {
        Music.Play();
        if (MvPlayer.clip != null)
        {
            ClearRenderTexture();
            MvPlayer.Play();
            MvScreen.enabled = true;
        }
        else
        {
            MvScreen.enabled = false;
        }
    }

    public void PauseMusic()
    {
        Music.Pause();
        if (MvPlayer.clip != null) 
        { 
            MvPlayer.Pause(); 
        }
    }
    public void ContinewMusic()
    {
        Music.Play();
        if (MvPlayer.clip != null) 
        {
            MvPlayer.Play(); 
        }
    }
    public void StopMusic()
    {
        Music.Stop();
        if (MvPlayer.clip != null) 
        { 
            MvPlayer.Stop(); 
        }
    }

    public void SetMusicClip(MusicSO music)
    {
        Music.clip = music.Music;
    }
    public void SetMusicVolum(float volume)
    {
        volume = volume / 10;
        Music.volume = volume;
    }
    public void SetVideoClip(MusicSO music)
    {
        MvPlayer.clip = null;
        if (music.MV != null)
        {
            MvPlayer.clip = music.MV;
        }
        else
        {
            MvPlayer.enabled = false;
            MvPlayer.clip = null;
        }
    }

    public void PlaySFX_TapNull()
    {
        SFX_TapNull[SFX_TapNullIndex].Play();

        SFX_TapNullIndex += 1;
        SFX_TapNullIndex = SFX_TapNullIndex % SFX_TapNull.Length;
    }

    public void PlaySFX_TapNormal()
    {
        SFX_TapNormal[SFX_TapNormalIndex].Play();

        SFX_TapNormalIndex += 1;
        SFX_TapNormalIndex = SFX_TapNormalIndex% SFX_TapNormal.Length;
    }

    public void PlaySFX_TapStar()
    {
        SFX_TapStar[SFX_TapStarIndex].Play();

        SFX_TapStarIndex += 1;
        SFX_TapStarIndex = SFX_TapStarIndex % SFX_TapStar.Length;
    }

    public void PlaySFX_FlickSc()
    {
        SFX_FlickSc[SFX_FlickScIndex].Play();

        SFX_FlickScIndex += 1;
        SFX_FlickScIndex = SFX_FlickScIndex % SFX_FlickSc.Length;
    }
    public void PlaySFX_FlickTrash()
    {
        SFX_FlickTrash[SFX_FlickTrashIndex].Play();

        SFX_FlickTrashIndex += 1;
        SFX_FlickTrashIndex = SFX_FlickTrashIndex % SFX_FlickTrash.Length;
    }
    public void PlaySFX_Hold()
    {
        SFX_Hold[SFX_HoldIndex].Play();

        SFX_HoldIndex += 1;
        SFX_HoldIndex = SFX_HoldIndex % SFX_Hold.Length;
    }

    private void PlaySFX_AllPerfect()
    {
        print("SFX AllPerfect!!");
    }
    private void PlaySFX_FullCombo()
    {
        print("SFX FullCombo!!");
    }
    private void PlaySFX_LiveSuccess()
    {
        print("SFX LiveSuccess!!");
    }

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "TapNull":
                PlaySFX_TapNull();
                break;
            case "TapNormal":
                PlaySFX_TapNormal();
                break;
            case "TapStar":
                PlaySFX_TapStar();
                break;
            case "FlickSC_LV1":
                PlaySFX_FlickSc();
                break;
            case "FlickSC_LV2":
                PlaySFX_FlickSc();
                break;
            case "FlickSC_LV3":
                PlaySFX_FlickSc();
                break;
            case "FlickSC_LV4":
                PlaySFX_FlickSc();
                break;
            case "FlickSC_LV5":
                PlaySFX_FlickSc();
                break;
            case "FlickSC_LV6":
                PlaySFX_FlickSc();
                break;
            case "FlickSC_LV7":
                PlaySFX_FlickSc();
                break;
            case "FlickTrash":
                PlaySFX_FlickTrash();
                break;
            case "Hold":
                PlaySFX_Hold();
                break;
            //--------------------------------------------------
            case "All_Perfect":
                PlaySFX_AllPerfect();
                break;
            case "Full_Combo":
                PlaySFX_FullCombo();
                break;
            case "Live_Success":
                PlaySFX_LiveSuccess();
                break;
            //--------------------------------------------------
            default:
                Debug.LogError($"No audio source for \"{sfxName}\" ");
                break;
        }
        //print(nodeType + " SFX");
    }

    // 新增一個清空RenderTexture的方法
    private void ClearRenderTexture()
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = MvVideoRenderer;
        GL.Clear(true, true, Color.black);  // 清空畫面為黑色
        RenderTexture.active = rt;
    }
}
