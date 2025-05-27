using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TrackSO : ScriptableObject
{
    public Material normal;
    public Material hightLight;

    //特效可為 Animation 或 Partical
    public GameObject TabButtonOnTabEffect1; // 點擊中特效
    public GameObject TabButtonOnTabEffect2; // 點擊中特效
    public GameObject FlickButtonOnTabEffect;// 點擊中特效
}
