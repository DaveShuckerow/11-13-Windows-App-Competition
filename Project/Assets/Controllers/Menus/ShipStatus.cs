using UnityEngine;
using System.Collections;

public class ShipStatus : MonoBehaviour {
    ShipController ship;
    GUIStyle frontStyle = new GUIStyle();
    GUIStyle backStyle = new GUIStyle();
    GUIStyle textStyle = new GUIStyle();
    public GUISkin skin;
    
	// Use this for initialization
	void Start () {
        frontStyle.normal.textColor = Color.white;
        backStyle.normal.textColor = Color.white;
        textStyle.normal.textColor = Color.white;
        frontStyle.fontSize = 15;
        backStyle.fontSize = 15;
        textStyle.fontSize = 15;
        setShip(ship);
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
    void OnGUI()
    {
        if (ship == null)
        {
            return;
        }
        int w = Screen.width;
        int h = Screen.height;
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

        GUI.Box(new Rect(1000, 40, 100, 140), "", frontStyle);
        GUI.Box(new Rect(1000, 100, 100, 140), "", frontStyle);
        GUI.Label(new Rect(1000, 60, 400, 200), "" + currentHP + "/" + maxHP + "", frontStyle);
        GUI.Label(new Rect(1000, 90, 400, 200), "" + currentShield + "/" + maxShield + "", backStyle);
        GUI.Label(new Rect(900, 60, 400, 200), "Hull Integrity", textStyle);
        GUI.Label(new Rect(900, 90, 400, 200), "Shields", textStyle);
        GUI.Label(new Rect(900, 120, 200, 100), "Moves: ", textStyle);
        GUI.Label(new Rect(960, 120, 200, 100),""+ numOfMoves + "", textStyle);
        GUI.Label(new Rect(1000, 120, 200, 100), "Weapons", textStyle);
        GUI.Label(new Rect(1080, 120, 200, 100), "" + numOfWeaps + "", textStyle);

        GUI.backgroundColor = Color.red;
        GUI.Box(new Rect(1000, 40, 100, 140), "", frontStyle);

       


    }

    
}
