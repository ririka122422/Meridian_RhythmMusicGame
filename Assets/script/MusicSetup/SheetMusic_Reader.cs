using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SheetMusic_Reader
{
    //On Load
    //遊戲內讀取
    public List<string> LoadSheetMusic(TextAsset textAsset)
    {
        string allGrupsCode = textAsset.text;

        allGrupsCode = allGrupsCode.Replace("&", "");
        allGrupsCode = allGrupsCode.Replace("\n", "");
        string[] MusicSheet = allGrupsCode.Split(",");

        List<string> MusicSheetList = new List<string>();
        MusicSheetList.Clear();
        for (int i = 0; i < MusicSheet.Length; i++)
        {
            MusicSheetList.Add(MusicSheet[i]);
        }

        return MusicSheetList;
    }

    public int GetMaxNodeCount(TextAsset MusicSheetFile)
    {
        string allGrupsCode = MusicSheetFile.text;

        allGrupsCode = allGrupsCode.Replace("&", "");
        allGrupsCode = allGrupsCode.Replace("\n", "");
        allGrupsCode = allGrupsCode.Replace(",", "");

        string[] MusicSheet = allGrupsCode.Split("-");

        int comboCount = 0;


        for (int i = 0; i < MusicSheet.Length; i++)
        {
            switch (MusicSheet[i])
            {
                case "0":
                    continue;
                case "1":
                    comboCount++;
                    break;
                case "2":
                    comboCount++;
                    break;
                case "3":
                    comboCount++;
                    break;
                case "4":
                    comboCount++;
                    break;
                case "5":
                    comboCount++;
                    break;
                case "6":
                    comboCount++;
                    break;
                case "7":
                    comboCount++;
                    break;
                case "8":
                    comboCount++;
                    break;
                case "9":
                    comboCount++;
                    break;
                case "10":
                    comboCount++;
                    break;
                case "11":
                    comboCount += 2;
                    break;
                case "12":
                    comboCount += 2;
                    break;
                case "14":
                    comboCount += 2;
                    break;
                case "18":
                    comboCount += 2;
                    break;
                case "21":
                    comboCount += 2;
                    break;
                case "41":
                    comboCount += 2;
                    break;
                default:
                    break;
            }

            //Debug.Log($"{i} > {MusicSheet[i]} -------- {comboCount}");
            /*
            
            allGrupsCode = allGrupsCode.Replace("&", "");
            allGrupsCode = allGrupsCode.Replace("\n", "");
            allGrupsCode = allGrupsCode.Replace("-", "");
            allGrupsCode = allGrupsCode.Replace("0", "");
            allGrupsCode = allGrupsCode.Replace(",", "");
            allGrupsCode = allGrupsCode.Replace(" ", "");

            if (allGrupsCode[i] == 11 || allGrupsCode[i] == 12 || allGrupsCode[i] == 14 || allGrupsCode[i] == 18 || allGrupsCode[i] == 21)
            {
                comboCount += 2;
                Debug.Log($"{i} > {allGrupsCode[i]} -------- {comboCount}");
            }
            else
            {
                comboCount += 1;
                Debug.Log($"{i} > {allGrupsCode[i]} -------- {comboCount}");
            }*/
        }
        return comboCount;
    }

    //僅限音樂編輯器內讀取，遊戲場景不可用
    public void SetSheetMusic(TextAsset SheetFile, List<SheetMusic_BeatGrup> newGrupList)
    {
        Debug.Log(SheetFile.text);

        string[] allGrupsCode = SheetFile.text.Split("&");
        for (int i = 0; i < allGrupsCode.Length-1; i++)
        {
            SheetMusic_BeatGrup newBeatGrup = newGrupList[i];
            newBeatGrup.setGrupIDs(allGrupsCode[i]);
        }
    }

    //On Save
    public bool ChackIsFileExists(string FileName)
    {
        string path = Path.Combine(Application.dataPath, "Resources/SheetMusic", $"{FileName}.txt");

        // 檢查是否存在檔案
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return false;
        }
    }

    public string GetSheetMusic(List<SheetMusic_BeatGrup> BeatGrupList)
    {
        string Codes = "";
        //string path = $"SheetMusic/{musicName}";

        for (int i = 0; i < BeatGrupList.Count; i++)
        {
            BeatGrupList[i].enabled = true;
            Codes += BeatGrupList[i].getGrupIDs();
            BeatGrupList[i].enabled = false;
        }
        
        return Codes;
    }

    public void SaveSheetMusic(string FileName, string codes)
    {
        string path = Path.Combine(Application.dataPath, "Resources/SheetMusic", $"{FileName}.txt");
        File.WriteAllText(path, codes);
        Debug.Log($"檔案已儲存到：{path}");
    }


}