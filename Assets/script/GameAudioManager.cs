using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class GameAudioManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource buttonClick;
    public AudioMixer audioMixer;
    public string BgmVolumeParameter = "Volume";  // 對應Exposed Parameter
    public string SfxVolumParameter = "SfxVolum";
    public float fadeDuration = 0.5f;
    public float maxVolume = 0f;  // 0dB是最大音量，-80dB是最小音量
    public float minVolume = -80f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {

    }

    public void PlayOnClickButtonSfx()
    {
        buttonClick.Play();
    }

    // 切換背景音樂並淡入新音樂
    public void SetBgmMusic(AudioClip newClip, float time)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeToNewTrack(newClip, time));
    }

    // 淡入新音樂
    private IEnumerator FadeToNewTrack(AudioClip newClip, float time)
    {
        // 淡出當前音樂
        yield return StartCoroutine(FadeMixer(minVolume));

        bgm.clip = newClip;
        bgm.Play();
        bgm.time = time;
        // 淡入新音樂
        yield return StartCoroutine(FadeMixer(maxVolume));
    }

    // 淡入淡出混音器的音量
    private IEnumerator FadeMixer(float targetVolume)
    {
        float currentTime = 0f;
        audioMixer.GetFloat(BgmVolumeParameter, out float currentVolume);
        float startVolume = currentVolume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / fadeDuration);
            audioMixer.SetFloat(BgmVolumeParameter, newVolume);
            yield return null;
        }

        audioMixer.SetFloat(BgmVolumeParameter, targetVolume);
    }

    // 立即切換音樂但淡入播放
    public void SetBgmImmediately(AudioClip newClip, float time)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        bgm.clip = newClip;
        bgm.Play();
        bgm.time = time;
        fadeCoroutine = StartCoroutine(FadeMixer(maxVolume));
    }

    // 淡出撥放
    public void StopBgm()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine( StopBgmFadeOut() );
    }

    private IEnumerator StopBgmFadeOut()
    {
        // 淡出當前音樂
        yield return StartCoroutine(FadeMixer(minVolume));

        bgm.Stop();
        bgm.clip = null;
    }

    //改變音量
    public void SetBgmVolum(float value)
    {
        value = value / 10;
        bgm.volume = value;
    }

    public void SetMusicVolum(float value)
    {
        float newVolume = Mathf.Lerp(minVolume, maxVolume, value / 10);
        audioMixer.SetFloat(BgmVolumeParameter, newVolume);
    }
    public void SetSfxVolum(float value)// 0 到10
    {
        float newVolume = Mathf.Lerp(minVolume, maxVolume, value / 10);
        audioMixer.SetFloat(SfxVolumParameter, newVolume);
        //bgm.volume = value;
    }







    public float GetPlayingAudioTime()
    {
        float time = bgm.time;
        return time;
    }
    public void SetPlayingAudioTime(float time)
    {
        bgm.time = time;
    }
}
