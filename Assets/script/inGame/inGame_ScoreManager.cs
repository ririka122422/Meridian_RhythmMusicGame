using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGame_ScoreManager : MonoBehaviour
{
    public inGame_uiManager uiManager;
    public int TotleScore;

    public int PerfectIncreaseScore = 3;
    public int GreatIncreaseScore = 1;
    public int MissIncreaseScore = 0;

    public int ComboCount;
    public int MaxComboCount;
    public int SongMaxComboCount;//Full Combo Number

    public int PerfectCount;
    public int GreatCount;
    public int MissCount;


    public int ExpCount;
    public int HoldCount;
    public int StarCount;
    public int ScCount_Level1;
    public int ScCount_Level2;
    public int ScCount_Level3;
    public int ScCount_Level4;
    public int ScCount_Level5;
    public int ScCount_Level6;
    public int ScCount_Level7;
    public int TrashCoount;


    private void Start()
    {
        initialize();
    }

    public void initialize()
    {
        TotleScore = 0;
        ComboCount = 0;
        MaxComboCount = 0;
        PerfectCount = 0;
        GreatCount = 0;
        MissCount = 0;

        ExpCount = 0;
        HoldCount = 0;
        StarCount = 0;
        ScCount_Level1 = 0;
        ScCount_Level2 = 0;
        ScCount_Level3 = 0;
        ScCount_Level4 = 0;
        ScCount_Level5 = 0;
        ScCount_Level6 = 0;
        ScCount_Level7 = 0;
        TrashCoount = 0;
    }
    private void OnPerfect()
    {
        ComboCount += 1;
        PerfectCount += 1;
        TotleScore += PerfectIncreaseScore;
    }
    private void OnGreat()
    {
        ComboCount += 1;
        GreatCount += 1;
        TotleScore += GreatIncreaseScore;
    }
    private void OnMiss()
    {
        ComboCount = 0;
        MissCount += 1;
        TotleScore += MissIncreaseScore;
        ScoreZeroChack();
    }

    private void ScoreZeroChack()
    {
        if(TotleScore <= 0)
        {
            TotleScore = 0;
        }
    }

    private void ChackMaxCombo()
    {
        if(ComboCount > MaxComboCount)
        {
            MaxComboCount = ComboCount;
        }
    }

    public string GetLiveFinalResult()
    {
        if (PerfectCount == SongMaxComboCount)
        {
            return "All_Perfect";
        }
        else if (ComboCount == SongMaxComboCount)
        {
            return "Full_Combo";
        }
        else
        {
            return "Live_Success";
        }
    }

    public void AddButtonTypeByName(string name)
    {
        switch (name)
        {
            case "TapNormal":
                ExpCount++;
                break;
            case "TapStar":
                StarCount++;
                break;
            case "FlickSC_LV1":
                ScCount_Level1++;
                break;
            case "FlickSC_LV2":
                ScCount_Level2++;
                break;
            case "FlickSC_LV3":
                ScCount_Level3++;
                break;
            case "FlickSC_LV4":
                ScCount_Level4++;
                break;
            case "FlickSC_LV5":
                ScCount_Level5++;
                break;
            case "FlickSC_LV6":
                ScCount_Level6++;
                break;
            case "FlickSC_LV7":
                ScCount_Level7++;
                break;
            case "FlickTrash":
                TrashCoount++;
                break;
            case "Hold":
                HoldCount++;
                break;


        }
    }

    public void AddDeterminationByName(string DeterminationName)
    {
        switch (DeterminationName)
        {
            case "Great_Fast":
                OnGreat();
                uiManager.ShowDeterminationResult("Great_Fast"); 
                break;
            case "Perfect":
                OnPerfect();
                uiManager.ShowDeterminationResult("Perfect"); 
                break;
            case "Great_Late":
                OnGreat();
                uiManager.ShowDeterminationResult("Great_Late"); 
                break;
            case "Miss":
                OnMiss();
                uiManager.ShowDeterminationResult("Miss"); 
                break;
        }
        ChackMaxCombo();
    }

    public int GetTotleScore()
    {
        return TotleScore;
    }
    public int GetComboCount()
    {
        return ComboCount;
    }
    public int GetMaxComboCount()
    {
        return MaxComboCount;
    }
    public int GetPerfectCount()
    {
        return PerfectCount;
    }
    public int GetGreatCount()
    {
        return GreatCount;
    }
    public int GetMissCount()
    {
        return MissCount;
    }
}
