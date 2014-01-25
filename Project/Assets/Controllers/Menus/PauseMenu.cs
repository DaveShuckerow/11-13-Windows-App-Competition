using UnityEngine;
using System.Collections;
using System;

public class PauseMenu : MonoBehaviour
{
    public GUISkin skin;
    ShipController ship;
    public int pauseStatus = 0;
    int quitStatus = 0;
    // Use this for initialization
    void Start()
    {
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
        if (GUI.Button(new Rect(0, 0, w/8, h/16), "Pause"))
        {
            pauseStatus = 1;
            Time.timeScale = 0;
            foreach (MonoBehaviour c in GetComponents<MonoBehaviour>())
            {
                if (c != (MonoBehaviour)this)
                    c.enabled = false;
            }
        }
        if (pauseStatus == 1)
        {
            GUI.Box(new Rect(0, 0, w, h), "");
            GUI.Box(new Rect(w / 4, h/8, w/2, 3*h/4),"");
            GUI.Label(new Rect(w / 2 - w / 16, h / 8, w / 8, h / 16), "Game Paused");
            if (GUI.Button(new Rect(w / 2 - w/16, h/4, w/8, h/16), "Resume"))
            {
                Time.timeScale = 1;
                foreach (MonoBehaviour c in GetComponents<MonoBehaviour>())
                {
                    if (c != (MonoBehaviour)this)
                        c.enabled = true;
                }
                pauseStatus = 0;
            }
            if (GUI.Button(new Rect(w / 2 - w / 16, 3* h / 8, w / 8, h / 16), "Menu"))
            {
                Application.LoadLevel(0);
            }
            if (GUI.Button(new Rect(w / 2 - w/16, 4*h/8, w/8, h/16), "Toggle Sound"))
            {
                GameObject.Find("MusicPlaya").GetComponent<AudioSource>().mute = !GameObject.Find("MusicPlaya").GetComponent<AudioSource>().mute;
                if (!GameObject.Find("MusicPlaya").GetComponent<AudioSource>().mute)
                {
                    GameObject.Find("MusicPlaya").GetComponent<AudioSource>().Play();
                }
            }
            if (GUI.Button(new Rect(w / 2 - w/16, 5*h/8, w/8, h/16), "Quit"))
            {
                quitStatus = 1;
            }
        }


    }
}
