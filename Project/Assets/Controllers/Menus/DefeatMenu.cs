using UnityEngine;
using System.Collections;

public class DefeatMenu : MonoBehaviour {

    public GUISkin skin;
    public Texture background;

    GUIStyle largeText;

    void Start()
    {
        largeText = new GUIStyle();
        largeText.font = Resources.Load<Font>("ECHO-Sans");
        largeText.fontStyle = FontStyle.Bold;
        largeText.normal.textColor = Color.white;
        largeText.fontSize = 42;
        largeText.alignment = TextAnchor.UpperCenter;
    }

    void OnGUI()
    {
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.DrawTexture(new Rect(0, 0, w, h), background, ScaleMode.ScaleAndCrop);
        GUI.Box(new Rect(w / 4, h / 16, w / 2, 3 * h / 8), "");
        GUI.Label(new Rect(w / 4, h / 8, w / 2, h / 8), "You Lost...", largeText);
        GUI.Label(new Rect(w / 4 + w / 16, h / 4, w / 2 - w / 8, h / 8), "Try again, maybe?");

        GUI.Box(new Rect(w / 4, h - h / 8, w / 2, h / 8), "");
        GUILayout.BeginArea(new Rect(w / 4, h - h / 8, w / 2, h / 8));
        GUILayout.BeginHorizontal("");
        if (GUILayout.Button("Main Menu", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(0);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
