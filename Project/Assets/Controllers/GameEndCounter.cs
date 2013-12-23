using UnityEngine;
using System.Collections;

public class GameEndCounter : MonoBehaviour {

    public int targetLevel;
    float lifetime = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Application.LoadLevel(targetLevel);
            Destroy(this);
        }
	}
}
