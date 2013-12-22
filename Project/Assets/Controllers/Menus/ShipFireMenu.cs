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
        style.fontSize = 20;
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
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.Box(new Rect(w / 4, 0, w / 2, h / 4), "");
        if (fireStatus < 1)
        {
            GUI.Label(new Rect(w / 4, 25, w / 2, 200), "Choose a ship to fire upon.", style);
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
}
