using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetup_NodeAudioPlayer : MonoBehaviour
{
    public AudioSource[] SFX_NormalTap = new AudioSource[4];
    public AudioSource[] SFX_NormalStar = new AudioSource[4];
    public AudioSource[] SFX_FlickSC = new AudioSource[4];
    public AudioSource[] SFX_FlickTrash = new AudioSource[4];
    public AudioSource[] SFX_Hold = new AudioSource[4];

    private int NormalTapIndex;
    private int NormalStarIndex;
    private int FlickSCIndex;
    private int FlickTrashIndex;
    private int HoldIndex;

    private void Start()
    {
        NormalTapIndex = 0;
        NormalStarIndex = 0;
        FlickSCIndex = 0;
        FlickTrashIndex = 0;
        HoldIndex = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TapNormal")
        {
            SFX_NormalTap[NormalTapIndex].Play();

            NormalTapIndex += 1;
            NormalTapIndex = NormalTapIndex % 4;
        }

        if (collision.gameObject.tag == "TapStar")
        {
            SFX_NormalStar[NormalStarIndex].Play();

            NormalStarIndex += 1;
            NormalStarIndex = NormalStarIndex % 4;
        }

        if (collision.gameObject.tag == "FlickSC")
        {
            SFX_FlickSC[FlickSCIndex].Play();

            FlickSCIndex += 1;
            FlickSCIndex = FlickSCIndex % 4;
        }

        if (collision.gameObject.tag == "FlickTrash")
        {
            SFX_FlickTrash[FlickTrashIndex].Play();

            FlickTrashIndex += 1;
            FlickTrashIndex = FlickTrashIndex % 4;
        }

        if (collision.gameObject.tag == "Hold")
        {
            SFX_Hold[HoldIndex].Play();

            HoldIndex += 1;
            HoldIndex = HoldIndex % 4;
        }
    }
}
