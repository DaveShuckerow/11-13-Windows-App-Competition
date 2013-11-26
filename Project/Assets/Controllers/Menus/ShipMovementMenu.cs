/**
 * How to do this...
 *   1: Wait for the mouse to press on ship hex.
 *   2: Lock camera scrolling
 *   3: Draw a path to a target hex
 *   4: Highlight it.
 **/
using UnityEngine;
using System.Collections;

public class ShipMovementMenu : MonoBehaviour {
    ShipController ship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void setShip(ShipController s)
    {
        ship = s;
    }

    public ShipController getShip()
    {
        return ship;
    }
}
