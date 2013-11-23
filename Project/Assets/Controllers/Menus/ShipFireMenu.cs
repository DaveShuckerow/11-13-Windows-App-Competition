using UnityEngine;
using System.Collections;

public class ShipFireMenu : MonoBehaviour {
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
