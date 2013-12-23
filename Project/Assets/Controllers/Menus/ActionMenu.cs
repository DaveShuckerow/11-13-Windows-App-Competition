using UnityEngine;
using System.Collections;
using System;

public class ActionMenu : MonoBehaviour
{
    public GUISkin skin;
    ShipController ship;
    public AIController caller;
    int expandAmount = 0;
    public float expandSpeed = 20;
    bool expanded = false;
    public bool moved;
    public bool fired;

    public void setShip(ShipController s)
    {
        ship = s;
        moved = false;
        fired = false;
        //ShipStatus t = gameObject.GetComponent<ShipStatus>();
        //t.setShip(ship);
        if (s != null)
            print("Setting a ship: " + s.gameObject.name);
        expand();
    }

    public ShipController getShip()
    {
        return ship;
    }

    void Update()
    {
        if (ship == null) return;
        if (!ship.isDoneMoving())
        {
            return;
        }
        if (expanded && expandAmount < Screen.width / 4)
            expandAmount += (int)Math.Round(expandSpeed * 100 * Time.deltaTime);
        if (!expanded && expandAmount >= 0)
            expandAmount -= (int)Math.Round(expandSpeed * 100 * Time.deltaTime);
        expandAmount = Mathf.Min(Mathf.Max(expandAmount, 0), Screen.width / 4);
    }

    void OnGUI()
    {
        if (GetComponent<PauseMenu>().pauseStatus == 1)
            return;
        if (ship == null)
            return;
        // Draw outline.
        if (expandAmount <= 0) return;
        GUI.skin = skin;
        int w = Screen.width;
        int h = Screen.height;
        GUI.Box(new Rect(w - expandAmount, 0, expandAmount, h), "");
        if (ship == null)
            return;

        // Draw actions
        GUI.enabled = !moved;
        if (GUI.Button(new Rect(w - expandAmount, h / 2, expandAmount, h / 8), "Move"))
        {
            ShipMovementMenu m = gameObject.AddComponent<ShipMovementMenu>();
            m.setShip(ship);
            m.skin = skin;
            retract();
        }
        GUI.enabled = !fired;
        if (GUI.Button(new Rect(w - expandAmount, h / 2 + h / 8, expandAmount, h / 8), "Fire"))
        {
            ShipFireMenu m = gameObject.AddComponent<ShipFireMenu>();
            m.setShip(ship);
            m.skin = skin;
            retract();
        }
        GUI.enabled = true;
        if (GUI.Button(new Rect(w - expandAmount, h / 2 + h / 4, expandAmount, h / 8), "Done"))
        {
            retract();
            setShip(null);
            if (caller != null)
            {
                caller.endMove();
//                caller = null;
            }
        }

        // Draw the ship's status:
        if (ship == null) return;
        double maxHP = ship.myShip.getMaxHP();
        double currentHP = ship.myShip.getHP();
        double maxShield = ship.myShip.getMaxShieldHP();
        double currentShield = ship.myShip.getShieldHP();
        int numOfMoves = ship.myShip.getMoves();
        int counter = 0;
        for (int i = 0; i < ship.myShip.getUtilityCount(); i++)
        {
            if (ship.myShip.getUtility(i) is WeaponSystem)
            {
                counter++;
            }
        }
        int numOfWeaps = counter;
        GUI.color = Color.white;
        
        GUI.Label(new Rect(w-expandAmount, h/32, expandAmount/2, h/16), "Hull:    " + currentHP + "/" + maxHP + "");
        GUI.Label(new Rect(w-expandAmount/2, h/32, expandAmount/2, h/16), "Shields: " + currentShield + "/" + maxShield + "");
        GUI.DrawTexture(new Rect(w - expandAmount, h / 16, expandAmount, h / 2 - h / 8), ship.GetComponent<MeshRenderer>().renderer.material.GetTexture(0), ScaleMode.ScaleToFit);
        GUI.Label(new Rect(w-expandAmount, h/2-h/16, expandAmount/2, h/16), "Moves: " + numOfMoves);
        GUI.Label(new Rect(w-expandAmount/2, h/2-h/16, expandAmount/2, h/16), "Weapons: " + numOfWeaps);

        //GUI.backgroundColor = Color.red;
        //GUI.Box(new Rect(1000, 40, 100, 140), "");

    }

    public void retract()
    {
        expanded = false;
    }

    public void expand()
    {
        expanded = true;
    }
}
