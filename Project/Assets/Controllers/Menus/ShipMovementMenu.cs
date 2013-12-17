/**
 * How to do this...
 *   1: Wait for the mouse to press on ship hex.
 *   2: Lock camera scrolling
 *   3: Draw a path to a target hex
 *   4: Highlight it.
 **/
using UnityEngine;
using System.Collections.Generic;

public class ShipMovementMenu : MonoBehaviour {
    ShipController ship;
    List<HexController> path;
    int pathStatus = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (HexController.mouseHex == ship.hex && pathStatus == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                path = new List<HexController>();
                pathStatus = 1;
            }
        }
        if (pathStatus == 1)
        {
            if (path.Count == 0 || path[path.Count-1] != HexController.mouseHex)
            {
                print("Adding a hex!");
                path.Add(HexController.mouseHex);
            }
            if (Input.GetMouseButtonUp(0))
            {
                pathStatus = 2;
                giveShipPath();
            }
        }
	}
    
    public void setShip(ShipController s)
    {
        ship = s;
    }

    public ShipController getShip()
    {
        return ship;
    }

    void giveShipPath()
    {
        print(path.Count);
        string pth = HexController.hexesToPath(path);
        print(pth);
        if (pth.Length > 1)
        ship.move(ship.myShip.followPath(pth));
    }
}
