using UnityEngine;
using System.Collections;
using System;

public class PauseMenu : MonoBehaviour
{
    public GUISkin skin;
    GUIStyle style = new GUIStyle();
    ShipController ship;
    int pauseStatus = 0;
    int quitStatus = 0;
    // Use this for initialization
    void Start()
    {
        style.fontSize = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (quitStatus == 1)
        {
            Application.Quit();
        }
    }

    public void setShip(ShipController s)
    {
        ship = s;

    }

    public ShipController getShip()
    {
        return ship;
    }

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;
        GUI.skin = skin;
        if (GUI.Button(new Rect(30, 15, 60, 40), "Pause"))
        {
            pauseStatus = 1;
            Time.timeScale = 0;
        }
        if (pauseStatus == 1)
        {
            GUI.Box(new Rect(w / 2, 25, 100, 140), "Pause Menu", style);
            if (GUI.Button(new Rect(w / 2 - 40, 100, 80, 22), "Resume"))
            {
                Time.timeScale = 1;
                pauseStatus = 0;
            }
            if (GUI.Button(new Rect(w / 2 - 40, 140, 80, 22), "Menu"))
            {
                //Placeholder as of 12/21
            }
            if (GUI.Button(new Rect(w / 2 - 40, 180, 80, 22), "Settings"))
            {
                //Placeholder as of 12/21
            }
            if (GUI.Button(new Rect(w / 2 - 40, 220, 80, 22), "Quit"))
            {
                quitStatus = 1;
            }
        }


    }
}
