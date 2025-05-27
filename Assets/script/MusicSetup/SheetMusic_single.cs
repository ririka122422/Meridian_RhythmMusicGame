using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SheetMusic_single : MonoBehaviour
{
    public MusicSetup_Editer _msEditer;
    SpriteRenderer SpriteRenderer;
    public int ButtonCode = 0;

    [Header("Tap Color")]
    public Color TapNormal;
    public Color TapStar;

    [Header("Flick SC Color")]
    public Color SC_LV1;
    public Color SC_LV2;
    public Color SC_LV3;
    public Color SC_LV4;
    public Color SC_LV5;
    public Color SC_LV6;
    public Color SC_LV7;

    [Header("Flick Tresh Color")]
    public Color Tresh;

    [Header("Hold")]
    public Color Hold11;// 4beat
    public Color Hold12;// 2beat
    public Color Hold14;// 1beat
    public Color Hold18;// 0.5beat

    public Color Hold21;// 8beat


    public bool isOnCallClick = true;

    private void Start()
    {
        ButtonCode = 0;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    

    private void ChangeType()
    {
        switch (ButtonCode)
        {
            case 0:
                gameObject.tag = "Untagged";
                SpriteRenderer.color = Color.black;
                break;
            case 1:
                gameObject.tag = "TapNormal";
                SpriteRenderer.color = TapNormal;
                break;
            case 2:
                gameObject.tag = "TapStar";
                SpriteRenderer.color = TapStar;
                break;
            case 3:
                gameObject.tag = "FlickSC";
                SpriteRenderer.color = SC_LV1;
                break;
            case 4:
                gameObject.tag = "FlickSC";
                SpriteRenderer.color = SC_LV2;
                break;
            case 5:
                gameObject.tag = "FlickSC";
                SpriteRenderer.color = SC_LV3;
                break;
            case 6:
                gameObject.tag = "FlickSC";
                SpriteRenderer.color = SC_LV4;
                break;
            case 7:
                gameObject.tag = "FlickSC";
                SpriteRenderer.color = SC_LV5;
                break;
            case 8:
                gameObject.tag = "FlickSC";
                SpriteRenderer.color = SC_LV6;
                break;
            case 9:
                gameObject.tag = "FlickSC";
                SpriteRenderer.color = SC_LV7;
                break;
            case 10:
                gameObject.tag = "FlickTrash";
                SpriteRenderer.color = Tresh;
                break;
            case 11:
                gameObject.tag = "Hold";
                SpriteRenderer.color = Hold11;
                break;
            case 12:
                gameObject.tag = "Hold";
                SpriteRenderer.color = Hold12;
                break;
            case 14:
                gameObject.tag = "Hold";
                SpriteRenderer.color = Hold14;
                break;
            case 18:
                gameObject.tag = "Hold";
                SpriteRenderer.color = Hold18;
                break;
            case 21:
                gameObject.tag = "Hold";
                SpriteRenderer.color = Hold21;
                break;
            default:
                gameObject.tag = "Untagged";
                SpriteRenderer.color = SpriteRenderer.color;
                break;
        }
    }

    private void ChangeButtonType()
    {
        ButtonCode += 1;
        if (ButtonCode >= 3)
        {
            ButtonCode = 0;
        }
        ChangeType();
    }

    public int GetCode()
    {
        return ButtonCode;
    }

    private void OnMouseDown()
    {
        if(isOnCallClick)
        {
            SetOnCall();
            isOnCallClick = false;
        }
        else
        {
            ChangeButtonType();
        }
    }

    private void SetOnCall()
    {
        if(_msEditer == null)
        {
            _msEditer = GameObject.Find("SheetMusic_Editer").GetComponent<MusicSetup_Editer>();
        }
        _msEditer.SetOnCallButton(this);
    }

    public void SetDisCall()
    {
        isOnCallClick = true;
    }

    public void SetCode(int codeID)
    {
        ButtonCode = codeID;
        ChangeType();
    }
}
