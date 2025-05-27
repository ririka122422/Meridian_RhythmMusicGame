using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SheetMusic_BeatGrup : MonoBehaviour
{
    public SheetMusic_single[][] grups = new SheetMusic_single[4][];
    public SpriteRenderer spriteRenderer;
    public Text showID;
    public int beatID;
    public string ButtonCodes;

    private string CodeSlide = "-";
    private string LineSlide = ",";
    private string GrupSlide = "&";

    void Start()
    {
        initialize();
    }

    public void initialize()
    {
        int index = 0;
        //取前24個子物件
        for (int j = 0; j < 4; j++)
        {
            grups[j] = new SheetMusic_single[6]; // 初始化子陣列
            for (int i = 0; i < 6; i++)
            {
                if (index >= transform.childCount)
                {
                    Debug.LogError("子物件數量不足，請檢查場景設定。");
                    return;
                }

                GameObject single = transform.GetChild(index).gameObject;

                grups[j][i] = single.GetComponent<SheetMusic_single>();
                if (grups[j][i] == null)
                {
                    Debug.LogError($"物件 {single.name} 缺少 SheetMusic_single 組件！");
                }

                index++;
            }
        }

        if(beatID % 4 == 0)
        {
            spriteRenderer.color = Color.red;
        }
        showID.text = $"{beatID}";
    }

    //On Save
    public string getGrupIDs()
    {
        string ids = "";
        for (int j =  0; j < 4; j++)
        {
            for (int i = 0; i < 6; i++)
            {

                ids += grups[j][i].GetCode();
                ids += CodeSlide;
            }
            ids += LineSlide;
        }
        ids += GrupSlide + "\n";
        ButtonCodes = ids;
        //print($"{gameObject.name} : {ids}");
        return ids;
    }

    //On Load
    public void setGrupIDs(string GropCodes)
    {
        string[] LinsCodes = GropCodes.Split(LineSlide);
        for (int j = 0; j < 4; j++)
        {
            string[] codes = LinsCodes[j].Split(CodeSlide);
            for (int i = 0; i < 6; i++)
            {
                grups[j][i].SetCode(Convert.ToInt32(codes[i]));
            }
        }
    }
}

