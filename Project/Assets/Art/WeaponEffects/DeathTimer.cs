using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour {
    public double lifetime = 5;
    private double myLife = 0;
	
	// Update is called once per frame
	void Update () {
        myLife += Time.deltaTime;
        if (myLife >= lifetime)
            Destroy(gameObject);
	}
}
