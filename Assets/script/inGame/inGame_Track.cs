using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class inGame_Track : MonoBehaviour
{
    private GameObject ManagerOBJ;
    private inGame_GameManager _gameManager;
    private inGame_ScoreManager _ScoreManager;
    private inGame_MediaManager _MediaManager;

    public TrackSO TrackSO;
    private MeshRenderer MeshRenderer;

    public AudioSource AudioSource;

    public Animator TapNormal;
    public Animator TapStar;
    public Animator FlickSc;
    public Animator Hold;
    public Animator FlickTrash;

    private Coroutine highLightTrack;
    private Queue<inGame_readNodeDetermination> DeterminationQueue = new Queue<inGame_readNodeDetermination>();

    private void Start()
    {
        ManagerOBJ = GameObject.Find("GameManager");
        _gameManager = ManagerOBJ.GetComponent<inGame_GameManager>();
        _ScoreManager = ManagerOBJ.GetComponent<inGame_ScoreManager>();
        _MediaManager = _gameManager.mediaManager;

        MeshRenderer = GetComponent<MeshRenderer>();

        DeterminationQueue.Clear();

    }

    void Update()
    {
        //getTouchInput();// 偵測軌道輸入
    }

    private void getTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Debug.Log($"手指 {touch.fingerId} 點擊到了 2D物件 {gameObject.name}");
                    OnTapTrack();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        //OnTapTrack();
    }

    IEnumerator HighLightTrack()
    {
        MeshRenderer.material = TrackSO.hightLight;
        yield return new WaitForSeconds(0.1f);
        MeshRenderer.material = TrackSO.normal;
    }

    public void OnTapTrack()// 軌道被點擊時(頭判) 單點時使用
    {
        if(highLightTrack != null)
        {
            StopCoroutine(highLightTrack);
        }
        StartCoroutine(HighLightTrack());
        bool hitNull = true;
        if (DeterminationQueue.Count >= 1)
        {
            inGame_readNodeDetermination nextDetermination = DeterminationQueue.Peek();
            if (nextDetermination.isDeterminable())
            {
                hitNull = false;
                Determining("Tap", nextDetermination);
            }
        }
        if (hitNull)
        {
            _MediaManager.PlaySFX("TapNull");
        }
        //Debug.Log($"{gameObject.name} + has been Tap");
    }
    public void OnHoldTrack()
    {
        //Determining("Hold");
    }
    public void OnFlickTrack(string inputType)//回傳尾判 Flick時使用
    {
        if (DeterminationQueue.Count >= 1)
        {
            inGame_readNodeDetermination nextDetermination = DeterminationQueue.Peek();
            if (nextDetermination.isDeterminable())
            {
                Determining(inputType, nextDetermination);
            }
        }
        //Determining("Flick_" + inputType);
        //Debug.Log($"{gameObject.name} + has been Flick");
    }

    public void addNodeDetermination(string buttonType, string determinationType, ButtonTap buttonTap)
    {
        inGame_readNodeDetermination newDetermination = new inGame_readNodeDetermination();
        newDetermination.inGame_Track = this;
        newDetermination.ButtonTap = buttonTap;
        newDetermination.SetButtonType(buttonType);
        newDetermination.SetDeterminationType(determinationType);
        newDetermination.SetCountdownTimer((float)(_gameManager.DelayMinSecond/1000) / _gameManager.GetDropSpeed());
        newDetermination.StartCountDown();

        //print("Determination add");
        DeterminationQueue.Enqueue(newDetermination);
    }

    public void Determining(string inputType, inGame_readNodeDetermination Determination)
    {
        if (Determination.isDeterminable())
        {
            if (Determination.isMyTypeAcceptable(inputType))
            {
                float triggerTime = Determination.GetNowTimerTime();
                string ButtonType = Determination.GetButtonType();
                string DeterminationValue = GetDeterminationValue(triggerTime);

                TriggerButtonVFX(ButtonType, DeterminationValue);
                if(DeterminationValue != "Miss")
                {
                    _ScoreManager.AddButtonTypeByName(ButtonType);
                }

                _MediaManager.PlaySFX(ButtonType);
                _ScoreManager.AddDeterminationByName(DeterminationValue);

                Determination.SetHaveBeenTrigger();//表示此判定已被棄用
                DeterminationQueue.Dequeue();
                isDoubleFlickChack(triggerTime);
            }
        }
    }

    private void isDoubleFlickChack(float nowTime)
    {
        if(DeterminationQueue.Count >= 1)
        {
            inGame_readNodeDetermination nextDetermination = DeterminationQueue.Peek();
            string nextButtonDetermination = nextDetermination.GetDeterminationType();
            if (nextButtonDetermination == "Flick")
            {
                if (nextDetermination.GetNowTimerTime() < nowTime + 0.010 && nextDetermination.GetNowTimerTime() > nowTime - 0.010)
                {
                    Determining("Flick", nextDetermination);
                }
            }
        }
    }
    
    private void TriggerButtonVFX(string buttonType, string determinationValue)
    {
        switch (buttonType)
        {
            case "TapNormal":
                if(determinationValue == "Perfect")
                {
                    TapNormal.SetTrigger("TapNormalPerfect");
                }
                else if(determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    TapNormal.SetTrigger("TapNormalPerfect");
                }
                break;
            case "TapStar":
                if (determinationValue == "Perfect")
                {
                    TapStar.SetTrigger("TapStarPerfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    TapStar.SetTrigger("TapStarPerfect");
                }
                break;
            case "FlickSC_LV1":
                if (determinationValue == "Perfect")
                {
                    FlickSc.SetTrigger("FlickSC_LV1Perfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickSc.SetTrigger("FlickSC_LV1Perfect");
                }
                break;
            case "FlickSC_LV2":
                if (determinationValue == "Perfect")
                {
                    FlickSc.SetTrigger("FlickSC_LV2Perfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickSc.SetTrigger("FlickSC_LV2Perfect");
                }
                break;
            case "FlickSC_LV3":
                if (determinationValue == "Perfect")
                {
                    FlickSc.SetTrigger("FlickSC_LV3Perfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickSc.SetTrigger("FlickSC_LV3Perfect");
                }
                break;
            case "FlickSC_LV4":
                if (determinationValue == "Perfect")
                {
                    FlickSc.SetTrigger("FlickSC_LV4Perfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickSc.SetTrigger("FlickSC_LV4Perfect");
                }
                break;
            case "FlickSC_LV5":
                if (determinationValue == "Perfect")
                {
                    FlickSc.SetTrigger("FlickSC_LV5Perfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickSc.SetTrigger("FlickSC_LV5Perfect");
                }
                break;
            case "FlickSC_LV6":
                if (determinationValue == "Perfect")
                {
                    FlickSc.SetTrigger("FlickSC_LV6Perfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickSc.SetTrigger("FlickSC_LV6Perfect");
                }
                break;
            case "FlickSC_LV7":
                if (determinationValue == "Perfect")
                {
                    FlickSc.SetTrigger("FlickSC_LV7Perfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickSc.SetTrigger("FlickSC_LV7Perfect");
                }
                break;
            case "FlickTrash":
                if (determinationValue == "Perfect")
                {
                    FlickTrash.SetTrigger("FlickTrash");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    FlickTrash.SetTrigger("FlickTrash");
                }
                break;
            case "Hold":
                if (determinationValue == "Perfect")
                {
                    Hold.SetTrigger("TapHoldPerfect");
                }
                else if (determinationValue == "Great_Fast" || determinationValue == "Great_Late")
                {
                    Hold.SetTrigger("TapHoldPerfect");
                }
                break;
        }
    }

    public void OnDeterminationTimeout()// 當-150ms都沒有被點擊，則判定miss
    {
        string missValue = GetDeterminationValue(-1);
        _ScoreManager.AddDeterminationByName(missValue);
        DeterminationQueue.Dequeue();
    }

    string GetDeterminationValue(float triggerTime)
    {
        string value = "";

        if (triggerTime <= 0.150 && triggerTime >= 0.075)
        {
            value = "Great_Fast";   
        }
        else if (triggerTime < 0.075 && triggerTime > -0.075)
        {
            value = "Perfect";
        }
        else if (triggerTime <= -0.075 && triggerTime >= -0.150)
        {
            value = "Great_Late";
        }
        else if (triggerTime < -0.150)
        {
            value = "Miss";
        }
        else
        {
            Debug.LogError($"{triggerTime}");
        }

        return value;
    }

    
}
