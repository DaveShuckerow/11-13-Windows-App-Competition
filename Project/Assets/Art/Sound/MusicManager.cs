using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (MusicPlayer.isPlaying())
        {
            Destroy(gameObject);
        }

        else
        {
            MusicPlayer.setPlaying(true);
            DontDestroyOnLoad(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
