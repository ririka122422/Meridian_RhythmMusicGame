using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class MusicBgmSO : ScriptableObject
{
    public string BgmName;
    public AudioClip Bgm;
    public float PlayClipTime;

    public void SaveClipTime(float time)
    {
        PlayClipTime = time;
    }
    public float GetClipTime()
    {
        return PlayClipTime;
    }
}
