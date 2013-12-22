using UnityEngine;
using System.Collections;

public class ShipFireMenu : MonoBehaviour
{
    ShipController ship;
    GUIStyle style = new GUIStyle();
    int fireStatus = 0;
    ShipController target;


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
        int w = Screen.width;
        int h = Screen.height;
        if (fireStatus < 1)
        {
            GUI.Label(new Rect(w / 2 - 60, 25, 400, 200), "Choose a ship to fire upon.", style);
        }
        if (GUI.Button(new Rect(1200, 600, 60, 40), "Cancel"))
        {

            GetComponent<ActionMenu>().expand();
            fireStatus = 0;
            Destroy(this);
        }
        if (fireStatus == 1)
        {
            GUI.Label(new Rect(w / 2 - 60, 25, 400, 200), "Are you sure?.", style);
            if (GUI.Button(new Rect(w / 2 - 60, 80, 80, 20), "Yes"))
            {

                fireUponShip();
                Debug.Log("PEW");
                GetComponent<ActionMenu>().expand();
                Destroy(this);
                fireStatus = 0;
            }
            if (GUI.Button(new Rect(w / 2 + 60, 80, 80, 20), "No"))
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
