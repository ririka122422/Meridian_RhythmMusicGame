using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongMenu_Song : MonoBehaviour
{
    public MusicSO musicSO;
    public Image Cover;

    public void SetupPanel(MusicSO musicSO)
    {
        this.musicSO = musicSO;
        if (musicSO.Cover169 != null)
            Cover.sprite = musicSO.Cover169;
    }

    public void SetFream(Transform freamTransform)
    {
        gameObject.transform.position = freamTransform.position;
        gameObject.transform.rotation = freamTransform.rotation;
        gameObject.transform.parent = freamTransform;
    }
    public void ReplaceToSongCardCollection(Transform CollectionBox)
    {
        gameObject.transform.parent = CollectionBox;
        gameObject.transform.position = new Vector3(0,0,0);
        gameObject.transform.rotation = Quaternion.Euler(0,0,0);
    }
}
