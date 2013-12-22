using UnityEngine;
using System.Collections;

public class WeaponEffectBehavior : MonoBehaviour {
    public Transform start;
    public Transform end;
    public double lifetime = 1;
    protected double myLife = 0;
    public bool doHit = true;
    public bool hitShields = true;

	// Use this for initialization
	void Start () {
	
	}

    public void setup(Transform s, Transform e, bool hit, bool shields)
    {
        start = s;
        end = e;
        doHit = hit;
        hitShields = shields;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
