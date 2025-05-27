using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class ButtonHoldLineRenderer : MonoBehaviour
{
    public ButtonTabSO TabSO;
    public LineRenderer lineRenderer;

    public GameObject headButton;
    public GameObject tailButton;

    public Transform headButtonTransform; // 首判物件
    public Transform tailButtonTransform; // 尾判物件
    private Transform TableTransform;

    public Vector3 renderOffset = new Vector3(0, 0, -0.1f);

    private void OnEnable()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        if(headButton.activeInHierarchy && tailButton.activeInHierarchy)
        {
            lineRenderer.SetPosition(0, headButtonTransform.position + renderOffset);
            lineRenderer.SetPosition(1, tailButtonTransform.position + renderOffset);
            //print("head is active");
        }
        else if(!headButton.activeInHierarchy && tailButton.activeInHierarchy)
        {
            //print("head is not active");

            if (tailButtonTransform.position.y < TableTransform.position.y)
            {
                DisableSelf();
            }
            else
            {
                Vector3 StopPosistion = new Vector3(tailButtonTransform.position.x, TableTransform.position.y, tailButtonTransform.position.z);
                lineRenderer.SetPosition(0, StopPosistion + renderOffset);
                lineRenderer.SetPosition(1, tailButtonTransform.position + renderOffset);
            }
        }
        else
        {
            DisableSelf();
        }
    }

    // 動態設定首判與尾判物件
    public void SetHoldConnection(GameObject start, GameObject end)
    {
        headButtonTransform = start.transform;
        tailButtonTransform = end.transform;
        UpdateLineRenderer();
    }
    void DisableSelf()
    {
        Reset();
        gameObject.SetActive(false);
    }
    private void Reset()
    {
        SetHoldMaterial();
    }

    //----------------

    public void SetHeadButton(GameObject HeadButton)
    {
        headButton = HeadButton;
        headButtonTransform = HeadButton.transform;
    }

    public void SetTailButton(GameObject TailButton)
    {
        tailButton = TailButton;
        tailButtonTransform = TailButton.transform;
    }

    public void SetTablePosistion(Transform tableTransform)
    {
        TableTransform = tableTransform;
    }

    public void SetHoldMaterial()
    {
        lineRenderer.material = TabSO.Hold;
    }
}

