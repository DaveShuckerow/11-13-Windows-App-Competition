using UnityEngine;
using System.Collections;

public class TipsMenu : MonoBehaviour {

    public GUISkin skin;
    public string text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.Box(new Rect(w / 4, h - h / 16, w / 2, h / 16), "");
        GUI.Label(new Rect(w / 4, h - h / 16, w / 2, h / 16), text);
        if (GUI.Button(new Rect(3 * w / 4 - w / 16, h - h / 16, w / 16, h / 16), "OK"))
        {
            Destroy(this);
        }
    }
}
