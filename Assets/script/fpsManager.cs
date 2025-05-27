using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class fpsManager : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private int counter = 0;

    void Start()
    {
        setFPSInitialize();
    }

    async void setFPSInitialize()
    {
        await keepFPS();
    }

    async Task keepFPS()
    {
        while(deltaTime < 150)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            if (counter < 150)
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    // 移動設備設置 60 FPS
                    Application.targetFrameRate = 60;
                }
                else
                {
                    // 桌面設備設置 120 FPS
                    Application.targetFrameRate = 120;
                }

                QualitySettings.vSyncCount = 0;
            }

            await Task.Yield();
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;

        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS", fps);
        GUI.Label(rect, text, style);
    }
}
