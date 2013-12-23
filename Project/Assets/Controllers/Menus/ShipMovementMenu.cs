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
    public GUISkin skin;
    GUIStyle style = new GUIStyle();

    // Use this for initialization
    void Start()
    {
        style.normal.textColor = Color.white;
        style.fontSize = 36;
        style.font = Resources.Load<Font>("ECHO-Sans");
        style.alignment = TextAnchor.UpperCenter;
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
                HexController.mouseHex.colorize(Color.green);
            }
            if (Input.GetMouseButtonUp(0))
            {
                pathStatus = 2;
                HexController finalSpot = ship.board.findHexController(ship.myShip.simulateMove(HexController.hexesToPath(path)));
                finalSpot.colorize(Color.blue);
            }
        }

    }

    void OnGUI()
    {
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.Box(new Rect(w / 4, 0, w / 2, h / 4),"");
        if (pathStatus < 2)
        {
            GUI.Label(new Rect(w / 4, 25, w/2, 200), "Draw a path to follow.", style);
        }
        if (GUI.Button(new Rect(w-w/8, h-h/16, w/8, h/16), "Cancel"))
        {
            GetComponent<ActionMenu>().expand();
            pathStatus = 0;
            Destroy(this);
        }
        if (pathStatus == 2)
        {

            GUI.Label(new Rect(w / 4, 0, w / 2, 200), "Is this the correct path?", style);
            if (GUI.Button(new Rect(w / 2 - w/16 - w/8, h/8, w/8, h/16), "Yes"))
            {

                giveShipPath();
                GetComponent<ActionMenu>().moved = true;
                GetComponent<ActionMenu>().expand();
                Destroy(this);
            }
            if (GUI.Button(new Rect(w / 2 + w/16, h/8, w/8, h/16), "No"))
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
            ship.move(pth);
        // Recolor hexes.
        foreach (HexController h in path)
        {
            h.colorize(Color.white);
        }
        HexController finalSpot = ship.board.findHexController(ship.myShip.getPosition());
        finalSpot.colorize(Color.blue);
    }
}
