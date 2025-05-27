using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu]
public class MusicSO : ScriptableObject
{
    [Header("Info")]
    public string MusicDisplayName;
    //public string MusicSheetFileName;
    public string Vocal;
    public Sprite Cover169;
    public AudioClip Music;
    public AudioClip MusicPreview;
    public VideoClip MV;

    [Header("Setting")]
    public int meter;
    public float bpm;
    public int BeatOffset = 1;
    public int FinishBeatOffset = 0;

    [Header("Level Easy")]
    public bool EasyPlayable;
    public int EasyLevel = 0;
    public TextAsset Easy;
    [Header("Level Normal")]
    public bool NormalPlayable;
    public int NormalLevel = 0;
    public TextAsset Normal;
    [Header("Level Hard")]
    public bool HardPlayable;
    public int HardLevel = 0;
    public TextAsset Hard;
}
