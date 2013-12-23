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
        GUILayout.BeginArea(new Rect(0, h - h / 8, w, h / 8), "");
        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("Play Easy", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(3);
        }        
        if (GUILayout.Button("Credits", GUILayout.ExpandHeight(true)))
        {
            //Application.LoadLevel(0);
        }
        if (GUILayout.Button("Support Hectics", GUILayout.ExpandHeight(true)))
        {

        }
        if (GUILayout.Button("Quit", GUILayout.ExpandHeight(true)))
        {
            Application.Quit();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
