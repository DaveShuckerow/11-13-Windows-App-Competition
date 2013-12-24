using UnityEngine;
using System.Collections;

public class CreditsMenu : MonoBehaviour
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
        GUI.Label(new Rect(w / 4, h / 32, w / 2, h / 8), "CREDITS", largeText);
        GUI.Box(new Rect(w / 4 - w / 8, h / 4 - h/8, w / 2 + w / 4, h / 8), "");
        TextAnchor currentAlignment = GUI.skin.label.alignment;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        //GUILayout.BeginArea(new Rect(w / 4 - w / 8, h / 4 - h / 8, w / 2 + w / 4, h / 4), "");
        //GUILayout.BeginHorizontal("");
        GUI.Label(new Rect(w/4-w/8, h/8, (3*w/4)/4, h/16), "Design & Programming Lead");
        GUI.Label(new Rect(w/4-w/8+(3*w/4)/4, h/8, (3*w/4)/4, h/16), "Art & Graphic Design");
        GUI.Label(new Rect(w/4-w/8+2*(3*w/4)/4, h/8, (3*w/4)/4, h/16),"Additional Programming");
        GUI.Label(new Rect(w/4-w/8+3*(3*w/4)/4, h/8, (3*w/4)/4, h/16),"Font");
        //GUILayout.EndHorizontal();
        //GUILayout.BeginHorizontal("");
        GUI.Label(new Rect(w/4-w/8, h/8+h/16, (3*w/4)/4, h/16), "David J Shuckerow");
        GUI.Label(new Rect(w/4-w/8+(3*w/4)/4, h/8+h/16, (3*w/4)/4, h/16), "Adam Sapp");
        GUI.Label(new Rect(w/4-w/8+2*(3*w/4)/4, h/8+h/16, (3*w/4)/4, h/16),"Chris Boling");
        GUI.Label(new Rect(w/4-w/8+3*(3*w/4)/4, h/8+h/16, (3*w/4)/4, h/16),"'ECHO-Sans' by 'elipurplepants'");
        //GUILayout.EndHorizontal();
        //GUILayout.EndArea();
        GUI.skin.label.alignment = currentAlignment;
        GUI.Box(new Rect(w / 4, h - h / 8, w / 2, h / 8), "");
        GUILayout.BeginArea(new Rect(w / 4, h - h / 8, w / 2, h / 8));
        GUILayout.BeginHorizontal("");
        if (GUILayout.Button("Main Menu", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(0);
        }
        if (GUILayout.Button("What's Next?", GUILayout.ExpandHeight(true)))
        {
            Application.LoadLevel(7);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
