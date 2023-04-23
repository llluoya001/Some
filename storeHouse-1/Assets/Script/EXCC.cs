using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EXCC : MonoBehaviour
{
    // public
    public int windowWidth = 200;
    public int windowHight = 75;

    // private
    Rect windowRect;
    int windowSwitch = 0;
    float alpha = 0;

    void GUIAlphaColor_0_To_1()
    {
        if (alpha < 1)
        {
            alpha += Time.deltaTime;
            GUI.color = new Color(1, 1, 1, alpha);
        }
    }

    // Init
    void Awake()
    {
        windowRect = new Rect(
            (Screen.width - windowWidth) / 2,
            (Screen.height - windowHight) / 2,
            windowWidth,
            windowHight);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            windowSwitch = 1;
            alpha = 0;
            
        }
    }

    void OnGUI()
    {
        if (windowSwitch == 1)
        {
            GUIAlphaColor_0_To_1();
            windowRect = GUI.Window(0, windowRect, QuitWindow, "退出遊戲");
        }
    }

    void QuitWindow(int windowID)
    {
        GUI.Label(new Rect(100, 50, 300, 30), "確定退出？");
        Time.timeScale = 0f;
        if (GUI.Button(new Rect(80, 110, 100, 20), "是"))
        {
            Application.Quit();
        }
        if (GUI.Button(new Rect(220, 110, 100, 20), "不要"))
        {
            windowSwitch = 0;
            Time.timeScale = 1f;
        }

        GUI.DragWindow();
    }
}
