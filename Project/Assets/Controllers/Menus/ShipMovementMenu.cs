/**
 * How to do this...
 *   1: Wait for the mouse to press on ship hex.
 *   2: Lock camera scrolling
 *   3: Draw a path to a target hex
 *   4: Highlight it.
 **/
using UnityEngine;
using System.Collections.Generic;


public class ShipMovementMenu : MonoBehaviour
{
    ShipController ship;
    List<HexController> path;
    int pathStatus = 0;
    GUIStyle style = new GUIStyle();


    // Use this for initialization
    void Start()
    {
        style.normal.textColor = Color.red;
        style.fontSize = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (ship == null)
        {
            return;
        }
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
            if (path.Count == 0 || path[path.Count - 1] != HexController.mouseHex)
            {
                print("Adding a hex!");
                path.Add(HexController.mouseHex);
            }
            if (Input.GetMouseButtonUp(0))
            {
                pathStatus = 2;
            }
        }

    }

    void OnGUI()
    {
        
        int w = Screen.width;
        int h = Screen.height;
        if (pathStatus < 2)
        {
            GUI.Label(new Rect(w / 2 - 60, 25, 400, 200), "Draw a path to follow.", style);
        }
        if (GUI.Button(new Rect(1200, 600, 60, 40), "Cancel"))
        {
           
            GetComponent<ActionMenu>().expand();
            pathStatus = 0;
            Destroy(this);
        }
        if (pathStatus == 2)
        {

            GUI.Label(new Rect(w / 2 - 60, 25, 400, 200), "Is this the correct path?", style);
            if (GUI.Button(new Rect(w / 2 - 60, 80, 80, 20), "Yes"))
            {
                
                giveShipPath();
                GetComponent<ActionMenu>().expand();
                Destroy(this);
            }
            if (GUI.Button(new Rect(w / 2 + 60, 80, 80, 20), "No"))
            {
                pathStatus = 0;
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
