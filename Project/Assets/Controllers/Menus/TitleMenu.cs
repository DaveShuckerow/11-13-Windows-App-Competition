using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

    public Texture bg;
    public GUISkin skin;

    void OnGUI()
    {
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.DrawTexture(new Rect(0, 0, w, h), bg, ScaleMode.ScaleAndCrop);
        GUI.Box(new Rect(0, h - h / 8, w, h / 8), "");
        GUI.Box(new Rect(0, 0, w, h / 8), "");
        GUILayout.BeginArea(new Rect(0, 0, w, h / 8), "");
        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("Play Easy", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(3);
        }
        if (GUILayout.Button("Play Medium", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(4);
        }
        if (GUILayout.Button("Play Hard", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(5);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(0, h - h / 8, w, h / 8), "");
        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("Toggle Sound", GUILayout.ExpandHeight(true)))
        {
            GameObject.Find("MusicPlaya").GetComponent<AudioSource>().mute = !GameObject.Find("MusicPlaya").GetComponent<AudioSource>().mute;
            if (!GameObject.Find("MusicPlaya").GetComponent<AudioSource>().mute)
            {
                GameObject.Find("MusicPlaya").GetComponent<AudioSource>().Play();
            }
        }
        if (GUILayout.Button("Credits", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(6);
        }
        if (GUILayout.Button("What's Next?", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(7);
        }
        if (GUILayout.Button("Quit", GUILayout.ExpandHeight(true)))
        {
            Application.Quit();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
