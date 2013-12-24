using UnityEngine;
using System.Collections;

public class SupportMenu : MonoBehaviour
{

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
        GUI.Label(new Rect(w / 4, h / 32, w / 2, h / 8), "What's Next?", largeText);
        GUI.Box(new Rect(w / 4 - w / 8, h / 4 - h / 8, w / 2 + w / 4, 5 * h / 16), "");
        TextAnchor currentAlignment = GUI.skin.label.alignment;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.BeginArea(new Rect(w / 4 - w / 8, h / 4 - h / 8, w / 2 + w / 4, 5 * h / 16), "");
        //GUILayout.Label("");
        //GUILayout.EndArea();
        //GUI.Box(new Rect(w / 4 - w / 8, h / 2 + 3*h / 16, w / 2 + w / 4, h / 8), "");
        //GUILayout.BeginArea(new Rect(w / 4 - w / 8, h / 2 + 3*h / 16, w / 2 + w / 4, h / 8), "");
        GUILayout.BeginHorizontal("");
        GUILayout.Label("Campaign");
        GUILayout.Label("Multiplayer");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("");
        GUILayout.Label("Build up a fleet, and gather what it takes to free the galaxy from the Sa'jed!");
        GUILayout.Label("Play against a friend, both on the same device and over the web, and see who's the best!");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("");
        GUILayout.Label("Customizable Ships");
        GUILayout.Label("Dynamic Fleets");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("");
        GUILayout.Label("So that you can choose what weapons, shields, engines, etc. that your units have.");
        GUILayout.Label("More units that you can customize, outfit, and build. Try to build an ace fleet!");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        GUI.skin.label.alignment = currentAlignment;
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
