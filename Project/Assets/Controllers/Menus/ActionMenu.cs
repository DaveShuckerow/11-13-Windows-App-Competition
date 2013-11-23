using UnityEngine;
using System.Collections;
using System;

public class ActionMenu : MonoBehaviour {
    GUISkin skin;
    ShipController ship;
    int expandAmount = 0;
    public float expandSpeed = 20;
    bool expanded = false;

    public void setShip(ShipController s)
    {
        ship = s;
        expand();
    }

    public ShipController getShip()
    {
        return ship;
    }

    void Update()
    {
        if (expanded && expandAmount < Screen.width/4)
            expandAmount += (int)Math.Round(expandSpeed *100* Time.deltaTime);
        if (!expanded && expandAmount >= 0)
            expandAmount -= (int)Math.Round(expandSpeed *100* Time.deltaTime);
    }

    void OnGUI()
    {
        // Draw outline.
        if (expandAmount <= 0) return;
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.Box(new Rect(w - expandAmount, 0, expandAmount, h), "");

        // Draw information
        //if (ship == null)
            //return;

        // Draw actions
        if (GUI.Button(new Rect(w - expandAmount, h / 2, expandAmount, h / 8), "Move"))
        {
            ShipMovementMenu m = gameObject.AddComponent<ShipMovementMenu>();
            m.setShip(ship);
            retract();
        }
        if (GUI.Button(new Rect(w - expandAmount, h / 2 + h / 8, expandAmount, h / 8), "Fire"))
        {
            ShipFireMenu m = gameObject.AddComponent<ShipFireMenu>();
            m.setShip(ship);
            retract();
        }
        if (GUI.Button(new Rect(w - expandAmount, h / 2 + h / 4, expandAmount, h / 8), "Done"))
        {
            retract();
        }

    }

    void retract()
    {
        expanded = false;
    }

    void expand()
    {
        expanded = true;
    }
}
