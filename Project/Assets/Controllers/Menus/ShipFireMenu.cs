using UnityEngine;
using System.Collections;

public class ShipFireMenu : MonoBehaviour
{
    ShipController ship;
    public GUISkin skin;
    int fireStatus = 0;
    ShipController target;
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
        if (fireStatus == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fireStatus = 1;
                target = HexController.mouseShip;
            }
        }
    }

    void OnGUI()
    {
        if (GetComponent<PauseMenu>().pauseStatus == 1)
            return;
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.Box(new Rect(w / 4, 0, w / 2, h / 4), "");
        if (fireStatus < 1)
        {
            GUI.Label(new Rect(w / 4, 25, w / 2, 200), "Choose a ship to fire upon.", style);
            GUI.Label(new Rect(w / 4, 60, w / 2, 200), "Targets are marked in red.");
        }
        if (GUI.Button(new Rect(w - w / 8, h - h / 16, w / 8, h / 16), "Cancel"))
        {

            GetComponent<ActionMenu>().expand();
            fireStatus = 0;
            Destroy(this);
        }
        if (fireStatus == 1)
        {
            GUI.Label(new Rect(w / 4, 25, w / 2, 200), "Are you sure?.", style);
            if (GUI.Button(new Rect(w / 2 - w / 16 - w / 8, h / 8, w / 8, h / 16), "Yes"))
            {

                fireUponShip();
                Debug.Log("PEW");
                GetComponent<ActionMenu>().fired = true;
                GetComponent<ActionMenu>().expand();
                Destroy(this);
                fireStatus = 0;
            }
            if (GUI.Button(new Rect(w / 2 + w / 16, h / 8, w / 8, h / 16), "No"))
            {
                fireStatus = 0;
            }
        }
    }


    public void setShip(ShipController s)
    {
        ship = s;
        foreach (ShipController sh in ship.board.shipList)
        {
            if (sh != ship && sh.myShip.getTeam() != ship.myShip.getTeam() && ship.myShip.getPosition().getHexDistance(sh.myShip.getPosition()) <= 3)
            {
                sh.hex.colorize(Color.red);
            }
        }
    }

    public ShipController getShip()
    {
        return ship;
    }

    public void fireUponShip()
    {
        ship.fire(target);
        Debug.Log("PEW");
    }

    void OnDestroy()
    {
        if (ship != null)
        {
            ship.board.resetHexColors();
            ship.hex.colorize(Color.blue);
        }
    }
}
