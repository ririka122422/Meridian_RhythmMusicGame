using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ButtonsFactorySO : ScriptableObject
{
    public GameObject NodePrefeb;
    public List<GameObject> NodeList = new List<GameObject>();
    private int index = 0;
    private int listCount;

    public void prewarm(int num)
    {
        NodeList.Clear();
        GameObject ButtonsCollition = new GameObject();
        ButtonsCollition.name = $"{NodePrefeb.name}ButtonsCollition";

        for (int i = 0; i < num; i++)
        {
            GameObject newNodes = Instantiate(NodePrefeb, ButtonsCollition.transform);
            NodeList.Add(newNodes);
            newNodes.SetActive(false);
        }
        listCount = num;
    }

    public GameObject Request()
    {
        if (NodeList.Count <= 0)
        {
            Debug.LogError("¼Æ¶q¤£¨¬");
            return null;
        }
        else
        {
            GameObject requestNode = NodeList[index% listCount];
            requestNode.SetActive(true);
            index++;
            return requestNode;
        }
    }

    public void Return(GameObject returnNode)
    {
        returnNode.SetActive(false);
    }
}
