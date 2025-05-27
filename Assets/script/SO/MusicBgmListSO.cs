using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MusicBgmListSO : ScriptableObject
{
    public List<MusicBgmSO> BgmList = new List<MusicBgmSO>();

    public MusicBgmSO GetRendomBgm()
    {
        int rendomIndex = Random.Range(0, BgmList.Count);
        MusicBgmSO bgmSo = BgmList[rendomIndex];

        return bgmSo;
    }
}
