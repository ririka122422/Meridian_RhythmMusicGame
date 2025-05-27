using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MusicSetup_Editer : MonoBehaviour
{
    public TextAsset EditingMusicSheet;

    public MusicSetup_Manager _MC;
    public MusicSetup_Ui _Ui;
    public MusicSetup_SheetMusicMover _mover;
    private SheetMusic_Reader _smR = new SheetMusic_Reader();

    public int activeIndexScal = 5;
    public GameObject grup_Prefab;

    [HideInInspector]
    public SheetMusic_single onCallButton;

    public List<SheetMusic_BeatGrup> BeatGrupList = new List<SheetMusic_BeatGrup>();
    private List<SheetMusic_BeatGrup> ActiveingBeatGrupList = new List<SheetMusic_BeatGrup>();
    public void GenerateSheetMusic(int beatNum)
    {
        ClearAllBeatGrup();
        float TransformOffsetY = 0;
        for (int i = 0; i < beatNum; i++)
        {
            GameObject newGrup =  Instantiate(grup_Prefab, _mover.gameObject.transform);
            newGrup.name = $"Beat{i}";
            newGrup.transform.position = new Vector3(0, TransformOffsetY, 0);
            TransformOffsetY += grup_Prefab.transform.localScale.y;
            BeatGrupList.Add(newGrup.GetComponent<SheetMusic_BeatGrup>());
            newGrup.GetComponent<SheetMusic_BeatGrup>().beatID = i;
            newGrup.SetActive(false);
        }
        DisplaySheetMusic(0);
    }

    public void ClearAllBeatGrup()
    {
        for(int i = 0;i < BeatGrupList.Count; i++)
        {
            Destroy(BeatGrupList[i]);
        }
        BeatGrupList.Clear();
        ActiveingBeatGrupList.Clear();
    }

    public void StartDisplayMoving(int BeatIndex)
    {
        _mover.StartMoving(BeatIndex);
    }

    public void GoToNowBeat(int BeatIndex)
    {
        _mover.GoTotBeat(BeatIndex);
        DisplaySheetMusic(BeatIndex);
    }

    public void DisplaySheetMusic(int actaveBeatNum)
    {
        

        for (int i = 0; i< ActiveingBeatGrupList.Count; i++)
        {
            ActiveingBeatGrupList[i].gameObject.SetActive(false);
        }
        ActiveingBeatGrupList.Clear();


        if (actaveBeatNum <= activeIndexScal)
        //起始時顯示數量
        {
            for (int i = 0; i< activeIndexScal*2; i++)
            {
                BeatGrupList[i].gameObject.SetActive(true);
                ActiveingBeatGrupList.Add(BeatGrupList[i]);
            }
        }
        else if(actaveBeatNum >= _MC.beatNum - activeIndexScal)
        //到頂時顯示數量
        {
            for (int i = _MC.beatNum - 1; i > _MC.beatNum - activeIndexScal*2; i--)
            {
                BeatGrupList[i].gameObject.SetActive(true);
                ActiveingBeatGrupList.Add(BeatGrupList[i]);
            }
        }
        else  
        //中間捲動時顯示數量
        {
            for (int i = actaveBeatNum - activeIndexScal; i < actaveBeatNum + activeIndexScal; i++)
            {
                BeatGrupList[i].gameObject.SetActive(true);
                ActiveingBeatGrupList.Add(BeatGrupList[i]);
            }
        }
    }

    public void StopDisplaying()
    {
        _mover.StopMoving();
    }
    
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadSheetMusic();
        }
        */
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftControl))
        {
            SaveSheetMusic();
        }
        if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1))
        {
            ChangOnCallButonType(0);
        }
    }
    
    public async void LoadSheetMusic()
    {
        for(int i = 0; i < BeatGrupList.Count; i++)
        {
            BeatGrupList[i].gameObject.SetActive(true );
        }
        await Task.Delay(1000);
        _smR.SetSheetMusic(EditingMusicSheet, BeatGrupList);
        for (int i = 0;i < BeatGrupList.Count;i++)
        {
            BeatGrupList[i].gameObject.SetActive(false);
        }
        _Ui.SetLoadSaveState($"Load {EditingMusicSheet.name}");
        DisplaySheetMusic(0);
    }

    public void OnClickSaveButton()
    {
        if (_smR.ChackIsFileExists(EditingMusicSheet.name))
        {
            _Ui.ShowOnSaveConfirmPanal();
        }
        else
        {
            SaveSheetMusic();
        }
    }


    public async void SaveSheetMusic()
    {
        for(int i = 0; i < BeatGrupList.Count; i++)
        {
            BeatGrupList[i].gameObject.SetActive(true);
        }

        string FileName = EditingMusicSheet.name;
        string codes = _smR.GetSheetMusic(BeatGrupList);
        await Task.Delay(1000);
        _smR.SaveSheetMusic(FileName, codes);
        Debug.Log("Save codes");
        Debug.Log(codes);
        await Task.Delay(1000);

        for (int i = 0; i < BeatGrupList.Count; i++)
        {
            BeatGrupList[i].gameObject.SetActive(false);
        }
        _Ui.SetLoadSaveState($"Save {EditingMusicSheet.name}");
        DisplaySheetMusic(0);
    }

    public void SetEditFileName(TextAsset editingMusicSheet)
    {
        EditingMusicSheet = editingMusicSheet;
    }


    public void SetOnCallButton(SheetMusic_single inputButton)
    {
        if(onCallButton != null)
        {
            onCallButton.SetDisCall();
        }

        onCallButton = inputButton;
    }
    public void ChangOnCallButonType(int TypeID)
    {
        if(onCallButton != null)
        {
            onCallButton.SetCode(TypeID);
        }
    }
}