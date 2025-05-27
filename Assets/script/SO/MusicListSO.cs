using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MusicListSO : ScriptableObject
{
    [SerializeField] List<MusicSO> musicSOs = new List<MusicSO>();

    public List<MusicSO> GetMusicSO_List()
    {
        return musicSOs;
    }
}
