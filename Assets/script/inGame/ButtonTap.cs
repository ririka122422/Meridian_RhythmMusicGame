using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ButtonTap : MonoBehaviour
{
    public ButtonTabSO tabSO;
    public MeshRenderer MeshRenderer;

    private Transform DropPosistion;
    private Transform TablePosistion;
    private Transform FloorTransform;

    private bool DropFinish;

    public void OnEnable()
    {
        DropFinish = false;
    }

    private void OnDisable()
    {
        DropFinish = true;
        Reset();
    }

    public void Reset()
    {
        MeshRenderer.material = tabSO.normal;
    }

    public void SetDropPosistion(Transform dropPosistion)
    {
        DropPosistion = dropPosistion;
        transform.position = dropPosistion.position;
    }
    public void SetTablePosistion(Transform tablePosistion)
    {
        TablePosistion = tablePosistion;
    }
    public void SetFloorPosistion(Transform floorPosistion)
    {
        FloorTransform = floorPosistion;
    }

    public async void Drop(float speed)
    {
        await DroppingDown(speed);
    }

    async Task DroppingDown(float speed)
    {
        Vector3 startPosition = DropPosistion.position;
        Vector3 DeterminationPosistion = new Vector3(DropPosistion.position.x, TablePosistion.position.y, DropPosistion.position.z);
        Vector3 endPosition = new Vector3(DropPosistion.position.x, FloorTransform.position.y, DropPosistion.position.z);

        float startTime = 0;
        float DeterminationTime = 3 / speed;
        float endTime = 6 / speed;

        while (startTime < DeterminationTime && DropFinish == false)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, DeterminationPosistion, startTime / DeterminationTime);
            startTime += Time.deltaTime;
            await Task.Yield();
        }
        if (DropFinish == false)
        {
            // 確保移動結束時位置精確到達終點
            transform.position = DeterminationPosistion;
        }
        await Task.Yield();
        while (startTime < endTime && DropFinish == false)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, startTime / endTime);
            startTime += Time.deltaTime;
            await Task.Yield();
        }
        if (DropFinish == false)
        {
            transform.position = endPosition;
            DisableThisButton();
        }
    }
    /*
    public void SetStartMaterial()
    {
        MeshRenderer.material = tabSO.star;
    }
    public void SetHoldMaterial()
    {
        MeshRenderer.material = tabSO.Hold;
    }
    public void SetMaterial_SC_Lv1()
    {
        MeshRenderer.material = tabSO.SC_LV1;
    }
    public void SetMaterial(string buttonCode)
    {
        switch (buttonCode)
        {
            case "1":
                MeshRenderer.material = tabSO.normal;
                break;
            case "2":
                MeshRenderer.material = tabSO.star;
                break;
            case "3":
                MeshRenderer.material = tabSO.SC_LV1;
                break;
            case "4":
                MeshRenderer.material = tabSO.SC_LV2;
                break;
            case "5":
                MeshRenderer.material = tabSO.SC_LV3;
                break;
            case "6":
                MeshRenderer.material = tabSO.SC_LV4;
                break;
            case "7":
                MeshRenderer.material = tabSO.SC_LV5;
                break;
            case "8":
                MeshRenderer.material = tabSO.SC_LV6;
                break;
            case "9":
                MeshRenderer.material = tabSO.SC_LV7;
                break;
            case "10":
                MeshRenderer.material = tabSO.Trash;
                break;
            case "11":
                MeshRenderer.material = tabSO.Hold;
                break;
            case "12":
                MeshRenderer.material = tabSO.Hold;
                break;
            case "14":
                MeshRenderer.material = tabSO.Hold;
                break;
            case "18":
                MeshRenderer.material = tabSO.Hold;
                break;
            case "21":
                MeshRenderer.material = tabSO.Hold;
                break;
            default:
                MeshRenderer.material = tabSO.Unknow;
                break;
        }
    }
    */
    public void DisableThisButton()
    {
        DropFinish = true;
        gameObject.SetActive(false);
    }
}
