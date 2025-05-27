using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class inGame_Dropper : MonoBehaviour
{
    private inGame_GameManager gameManager;

    [Header("Nodes Button Factory")]
    public ButtonsFactorySO TapNormalButtonsFactory;
    public ButtonsFactorySO TapStarButtonFactory;

    public ButtonsFactorySO FlickSC_LV1_ButtonFactory;
    public ButtonsFactorySO FlickSC_LV2_ButtonFactory;
    public ButtonsFactorySO FlickSC_LV3_ButtonFactory;
    public ButtonsFactorySO FlickSC_LV4_ButtonFactory;
    public ButtonsFactorySO FlickSC_LV5_ButtonFactory;
    public ButtonsFactorySO FlickSC_LV6_ButtonFactory;
    public ButtonsFactorySO FlickSC_LV7_ButtonFactory;
    public ButtonsFactorySO Flick_TrashButtonFactory;

    public ButtonsFactorySO HoldButtonFactory;
    public ButtonsFactorySO HoldLineFactory;

    [Header("Initial Scal")]
    public int initializeTapNoemalScal;
    public int initializeTapStarScal;

    public int initializeFlickSC_LV1_Scal;
    public int initializeFlickSC_LV2_Scal;
    public int initializeFlickSC_LV3_Scal;
    public int initializeFlickSC_LV4_Scal;
    public int initializeFlickSC_LV5_Scal;
    public int initializeFlickSC_LV6_Scal;
    public int initializeFlickSC_LV7_Scal;
    public int initializeFlickTrashScal;

    public int initializeHoldScal;
    public int initializeHoldLineScal;

    [Header("Scene Object Setting")]
    public inGame_Track[] Tracks = new inGame_Track[6];
    public Transform[] DropPosistion = new Transform[6];
    public Transform TablePosistion;
    public Transform FloorPosistion;

    private float DropSpeed;


    [Header("Debug")]
    public int HaveDropNode = 0;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<inGame_GameManager>();
        HaveDropNode = 0;
        prewarmFactorys();
    }

    private void prewarmFactorys()
    {
        TapNormalButtonsFactory.prewarm(initializeTapNoemalScal);
        TapStarButtonFactory.prewarm(initializeTapStarScal);

        FlickSC_LV1_ButtonFactory.prewarm(initializeFlickSC_LV1_Scal);
        FlickSC_LV2_ButtonFactory.prewarm(initializeFlickSC_LV2_Scal);
        FlickSC_LV3_ButtonFactory.prewarm(initializeFlickSC_LV3_Scal);
        FlickSC_LV4_ButtonFactory.prewarm(initializeFlickSC_LV4_Scal);
        FlickSC_LV5_ButtonFactory.prewarm(initializeFlickSC_LV5_Scal);
        FlickSC_LV6_ButtonFactory.prewarm(initializeFlickSC_LV6_Scal);
        FlickSC_LV7_ButtonFactory.prewarm(initializeFlickSC_LV7_Scal);
        Flick_TrashButtonFactory.prewarm(initializeFlickTrashScal);

        HoldButtonFactory.prewarm(initializeHoldScal);
        HoldLineFactory.prewarm(initializeHoldLineScal);
    }

    public void DropNodes(string NodeCodes)
    {
        string[] nodes = NodeCodes.Split('-');

        for (int i = 0; i < nodes.Length-1; i++)
        {
            switch (nodes[i])
            {
                case "0":
                    //此軌這次空白
                    break;
                case "1":
                    DropNormalTab(i);//Normal
                    break;
                case "2":
                    DropStarTab(i);//Star
                    break;
                case "3":
                    DropScFlick(i, nodes[i]);//SC
                    break;
                case "4":
                    DropScFlick(i, nodes[i]);//SC
                    break;
                case "5":
                    DropScFlick(i, nodes[i]);//SC
                    break;
                case "6":
                    DropScFlick(i, nodes[i]);//SC
                    break;
                case "7":
                    DropScFlick(i, nodes[i]);//SC
                    break;
                case "8":
                    DropScFlick(i, nodes[i]);//SC
                    break;
                case "9":
                    DropScFlick(i, nodes[i]);//SC
                    break;
                case "10":
                    DropTrashFlick(i);//Trash
                    break;


                case "11":
                    DropHold(i, gameManager.DisplayingMusic.meter);//Hold
                    break;
                case "12":
                    DropHold(i, ((float)gameManager.DisplayingMusic.meter) / 2);//Hold
                    break;
                case "14":
                    DropHold(i, ((float)gameManager.DisplayingMusic.meter) / 4);//Hold
                    break;
                case "18":
                    DropHold(i, ((float)gameManager.DisplayingMusic.meter)/8);//Hold
                    break;
                case "21":
                    DropHold(i, gameManager.DisplayingMusic.meter * 2);//Hold
                    break;
                case "41":
                    DropHold(i, gameManager.DisplayingMusic.meter * 4);//Hold
                    break;
                default:
                    Debug.LogError($"UnReadable Node Code : {nodes[i]}");
                    break;
            }
        }
    }

    public void SetDropSpeed(float dropSpeed)
    {
        DropSpeed = dropSpeed;
    }

    public void DropNormalTab(int TrackID)
    {
        GameObject dropTab = TapNormalButtonsFactory.Request();
        ButtonTap buttonTap = dropTab.GetComponent<ButtonTap>();

        buttonTap.SetDropPosistion(DropPosistion[TrackID]);
        buttonTap.SetTablePosistion(TablePosistion);
        buttonTap.SetFloorPosistion(FloorPosistion);
        buttonTap.Drop(DropSpeed);
        Tracks[TrackID].addNodeDetermination("TapNormal", "Tap", buttonTap);//一般按鍵
        HaveDropNode += 1;

    }
    public void DropStarTab(int TrackID)
    {
        GameObject dropTab = TapStarButtonFactory.Request();
        ButtonTap buttonTap = dropTab.GetComponent<ButtonTap>();

        buttonTap.SetDropPosistion(DropPosistion[TrackID]);
        buttonTap.SetTablePosistion(TablePosistion);
        buttonTap.SetFloorPosistion(FloorPosistion);
        //buttonTap.SetStartMaterial();
        buttonTap.Drop(DropSpeed);
        Tracks[TrackID].addNodeDetermination("TapStar", "Tap", buttonTap);//一般按鍵
        HaveDropNode += 1;
    }
    public void DropScFlick(int TrackID, string nodeID)
    {
        GameObject dropTab = new GameObject();
        string buttonType = "";
        switch (nodeID)
        {
            case "3":
                dropTab = FlickSC_LV1_ButtonFactory.Request();
                buttonType = "FlickSC_LV1";
                break;
            case "4":
                dropTab = FlickSC_LV2_ButtonFactory.Request();
                buttonType = "FlickSC_LV2";
                break;
            case "5":
                dropTab = FlickSC_LV3_ButtonFactory.Request();
                buttonType = "FlickSC_LV3";
                break;
            case "6":
                dropTab = FlickSC_LV4_ButtonFactory.Request();
                buttonType = "FlickSC_LV4";
                break;
            case "7":
                dropTab = FlickSC_LV5_ButtonFactory.Request();
                buttonType = "FlickSC_LV5";
                break;
            case "8":
                dropTab = FlickSC_LV6_ButtonFactory.Request();
                buttonType = "FlickSC_LV6";
                break;
            case "9":
                dropTab = FlickSC_LV7_ButtonFactory.Request();
                buttonType = "FlickSC_LV7";
                break;
            default:
                Debug.Log($"Unknow nodeID");
                break;
        }
        ButtonTap buttonTap = dropTab.GetComponent<ButtonTap>();

        buttonTap.SetDropPosistion(DropPosistion[TrackID]);
        buttonTap.SetTablePosistion(TablePosistion);
        buttonTap.SetFloorPosistion(FloorPosistion);
        //buttonTap.SetMaterial(nodeID);
        buttonTap.Drop(DropSpeed);
        Tracks[TrackID].addNodeDetermination(buttonType, "Flick", buttonTap);//SC
        HaveDropNode += 1;
    }
    public void DropTrashFlick(int TrackID)
    {
        GameObject dropTab = Flick_TrashButtonFactory.Request();
        ButtonTap buttonTap = dropTab.GetComponent<ButtonTap>();

        buttonTap.SetDropPosistion(DropPosistion[TrackID]);
        buttonTap.SetTablePosistion(TablePosistion);
        buttonTap.SetFloorPosistion(FloorPosistion);
        //buttonTap.SetMaterial(nodeID);
        buttonTap.Drop(DropSpeed);
        Tracks[TrackID].addNodeDetermination("FlickTrash", "Flick", buttonTap);//Trash
        HaveDropNode += 1;
    }

    public async void DropHold(int TrackID, float DelayBeat)
    {
        GameObject HoldHead = HoldButtonFactory.Request();
        GameObject HoldEnd = HoldButtonFactory.Request();
        GameObject HoleLine = HoldLineFactory.Request();
        ButtonTap Head = HoldHead.GetComponent<ButtonTap>();
        ButtonTap End = HoldEnd.GetComponent<ButtonTap>();
        ButtonHoldLineRenderer HoldLine = HoleLine.GetComponent<ButtonHoldLineRenderer>();

        Head.SetDropPosistion(DropPosistion[TrackID]);
        Head.SetTablePosistion(TablePosistion);
        Head.SetFloorPosistion(FloorPosistion);
        //Head.SetHoldMaterial(); 

        End.SetDropPosistion(DropPosistion[TrackID]);
        End.SetTablePosistion(TablePosistion);
        End.SetFloorPosistion(FloorPosistion);
        //End.SetHoldMaterial();

        HoldLine.SetHeadButton(HoldHead);
        HoldLine.SetTailButton(HoldEnd);
        HoldLine.SetTablePosistion(TablePosistion);
        //HoldLine.SetHoldMaterial();

        Head.Drop(DropSpeed);
        Tracks[TrackID].addNodeDetermination("Hold", "Tap", Head);//長押開始的第一個單擊頭判
        HaveDropNode += 1;
        await Task.Delay((int)((DelayBeat* gameManager.beatleng)*1000));
        //持續生成Hold判定

        End.Drop(DropSpeed);
        Tracks[TrackID].addNodeDetermination("Hold", "Flick", End);//長押尾判
        HaveDropNode += 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) { Tracks[0].OnTapTrack(); }
        if (Input.GetKeyDown(KeyCode.D)) { Tracks[1].OnTapTrack(); }
        if (Input.GetKeyDown(KeyCode.F)) { Tracks[2].OnTapTrack(); }
        if (Input.GetKeyDown(KeyCode.J)) { Tracks[3].OnTapTrack(); }
        if (Input.GetKeyDown(KeyCode.K)) { Tracks[4].OnTapTrack(); }
        if (Input.GetKeyDown(KeyCode.L)) { Tracks[5].OnTapTrack(); }

        if (Input.GetKeyUp(KeyCode.S)) { Tracks[0].OnFlickTrack("Flick"); }
        if (Input.GetKeyUp(KeyCode.D)) { Tracks[1].OnFlickTrack("Flick"); }
        if (Input.GetKeyUp(KeyCode.F)) { Tracks[2].OnFlickTrack("Flick"); }
        if (Input.GetKeyUp(KeyCode.J)) { Tracks[3].OnFlickTrack("Flick"); }
        if (Input.GetKeyUp(KeyCode.K)) { Tracks[4].OnFlickTrack("Flick"); }
        if (Input.GetKeyUp(KeyCode.L)) { Tracks[5].OnFlickTrack("Flick"); }
    }
}
