using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameSceneUiManager : MonoBehaviour
{
    public GameSceneManager GameSceneManager;

    public GameObject GameSceneUi;
    public Animator TransitionAnimator;
    public AnimationClip occlusion_TransitionAnimationClip;
    public AnimationClip release_TransitionAnimationClip;


    private bool isOcclusion;
    private bool isRelease;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameSceneUiManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        isOcclusion = false;
    }
    public async void PlayLoadSceneTransitionAnimation()
    {
        await PlayTransitionAnimator();
    }

    private async Task PlayTransitionAnimator()
    {
        isRelease = false;
        TransitionAnimator.SetTrigger("Enter");
        await Task.Delay((int)(occlusion_TransitionAnimationClip.length*1000));

        isOcclusion = true;//已遮蔽場景
        await Task.Delay(100);//wait
        while (!GameSceneManager.GetLoadSceneFinish())
        {
           await Task.Yield();
        }

        //播放結束Loading，釋放畫面(顯示inGame 畫面)
        TransitionAnimator.SetTrigger("Release");
        await Task.Delay((int)(release_TransitionAnimationClip.length * 1000));

        isRelease = true;
        isOcclusion = false;//未遮蔽場景
    }

    public bool GetIsOcclusion()
    {
        return isOcclusion;
    }
    public bool GetIsTransitionFinish()
    {
        return isRelease;
    }
}
