using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    public Charactor CharacterType;
    public string CharacterCardName;
    public Sprite icon;
    public Sprite Illustration;


    [Header("Level")]
    public int Level = 1;
    public int nowLevelExp = 0;
    public int upgrateNeedExpNum = 1000;
    public int MaxLevel = 60;

    [Header("Favorability")]
    public int FavorabilityLevel = 1;
    public int nowFavorabilityExp = 0;
    public int upgrateNeedFavorabilityExpNum = 100;
    public int MaxFavorabilityLevel = 60;

    [Header("Dask Table Element")]
    public Sprite SongMenuImage;
    public Sprite RaisingMenuImage;
    public Sprite GachaponImage;
    public Sprite BackGround;
    public Sprite BackGroundWhith;
    public Sprite BackGroundCharectorLogo;
    public Sprite BackGroundCenter;


    public void AddExp(int value)
    {
        nowLevelExp += value;

        while (nowLevelExp >= upgrateNeedExpNum)
        {
            Level += 1;
            nowLevelExp -= upgrateNeedExpNum;
        }

        
    }
    public void AddFavorability(int value)
    {
        nowFavorabilityExp += value;

        while (nowFavorabilityExp >= upgrateNeedFavorabilityExpNum)
        {
            FavorabilityLevel += 1;
            nowFavorabilityExp -= upgrateNeedFavorabilityExpNum;
        }
    }


    public int GetRemainingExp()
    {
        int RemainingExp = (MaxLevel - Level) * upgrateNeedExpNum - nowLevelExp;
        return RemainingExp;
    }

    public int GetRemainingFavorabilityExp()
    {
        int RemainingFavorabilityExp = (MaxFavorabilityLevel - FavorabilityLevel) * upgrateNeedFavorabilityExpNum - nowFavorabilityExp;
        return RemainingFavorabilityExp;
    }

}

public enum Charactor
{
    éûRei,
    ·×Kirali,
    ¾íYuzumi
}
