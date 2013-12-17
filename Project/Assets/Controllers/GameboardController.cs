using UnityEngine;
using System.Collections.Generic;
using System;

public class GameboardController : MonoBehaviour {
    public int size;
    public int hexRad = 96;
    HashSet<HexController> hexSet;
    Gameboard board;
	// Use this for initialization
	void Start () {
        board = new Gameboard(size);
        initDisplay();
        ShipController s = createShip("Ship1", findHexController(board.getHex("0")));
        //s.move(s.myShip.followPath("123456"));
	}

    void Update()
    {
        HexController.computeMouseHex();
    }

    void initDisplay()
    {
        Hex center = board.getHex("0"); 
        GameObject g = (GameObject)Instantiate(Resources.Load("HexPrefab"));
        HexController hex = g.GetComponent<HexController>();
        hex.myHex = center;
        g.transform.parent = transform;
        hexSet = new HashSet<HexController>();
        initDisplayExpand(hex, size-1);
        print("Hexes generated.");
    }
    private void initDisplayExpand(HexController center, int times) 
    {
        Hex h = center.myHex;
        if (times <= 0)
            return;

        if (center.upHex == null)
        {
            GameObject g = (GameObject)(Instantiate(Resources.Load("HexPrefab")));
            HexController hc = g.GetComponent<HexController>();
            hc.transform.parent = center.transform.parent;
            hc.transform.position = center.transform.position + new Vector3((float)Math.Cos(3 * Math.PI / 6) * hexRad, 0,
                                                                            (float)Math.Sin(3 * Math.PI / 6) * hexRad);
            center.upHex = hc;
            center.upHex.myHex = center.myHex.getUp();
            hc.dnHex = center;
        }
        if (center.ulHex == null)
        {
            GameObject g = (GameObject)(Instantiate(Resources.Load("HexPrefab")));
            HexController hc = g.GetComponent<HexController>();
            hc.transform.parent = center.transform.parent;
            hc.transform.position = center.transform.position + new Vector3((float)Math.Cos(5 * Math.PI / 6) * hexRad, 0,
                                                                            (float)Math.Sin(5 * Math.PI / 6) * hexRad);
            center.ulHex = hc;
            center.ulHex.myHex = center.myHex.getUl();
            hc.drHex = center;
        }
        if (center.dlHex == null)
        {
            GameObject g = (GameObject)(Instantiate(Resources.Load("HexPrefab")));
            HexController hc = g.GetComponent<HexController>();
            hc.transform.parent = center.transform.parent;
            hc.transform.position = center.transform.position + new Vector3((float)Math.Cos(7 * Math.PI / 6) * hexRad, 0,
                                                                            (float)Math.Sin(7 * Math.PI / 6) * hexRad);
            center.dlHex = hc;
            center.dlHex.myHex = center.myHex.getDl();
            hc.urHex = center;
        }
        if (center.dnHex == null)
        {
            GameObject g = (GameObject)(Instantiate(Resources.Load("HexPrefab")));
            HexController hc = g.GetComponent<HexController>();
            hc.transform.parent = center.transform.parent;
            hc.transform.position = center.transform.position + new Vector3((float)Math.Cos(9 * Math.PI / 6) * hexRad, 0,
                                                                            (float)Math.Sin(9 * Math.PI / 6) * hexRad);
            center.dnHex = hc;
            center.dnHex.myHex = center.myHex.getDn();
            hc.upHex = center;
        }
        if (center.drHex == null)
        {
            GameObject g = (GameObject)(Instantiate(Resources.Load("HexPrefab")));
            HexController hc = g.GetComponent<HexController>();
            hc.transform.parent = center.transform.parent;
            hc.transform.position = center.transform.position + new Vector3((float)Math.Cos(11 * Math.PI / 6) * hexRad, 0,
                                                                            (float)Math.Sin(11 * Math.PI / 6) * hexRad);
            center.drHex = hc;
            center.drHex.myHex = center.myHex.getDr();
            hc.ulHex = center;
        }
        if (center.urHex == null)
        {
            GameObject g = (GameObject)(Instantiate(Resources.Load("HexPrefab")));
            HexController hc = g.GetComponent<HexController>();
            hc.transform.parent = center.transform.parent;
            hc.transform.position = center.transform.position + new Vector3((float)Math.Cos(1 * Math.PI / 6) * hexRad, 0,
                                                                            (float)Math.Sin(1 * Math.PI / 6) * hexRad);
            center.urHex = hc;
            center.urHex.myHex = center.myHex.getUr();
            hc.dlHex = center;
        }
        center.finalizeHexes();

        // Expand again.
        if (times > 0)
        {
            hexSet.Add(center);
            initDisplayExpand(center.upHex, times - 1);
            initDisplayExpand(center.ulHex, times - 1);
            initDisplayExpand(center.dlHex, times - 1);
            initDisplayExpand(center.dnHex, times - 1);
            initDisplayExpand(center.drHex, times - 1);
            initDisplayExpand(center.urHex, times - 1);
        }
        else
        {
            hexSet.Add(center.upHex);
            hexSet.Add(center.ulHex);
            hexSet.Add(center.dlHex);
            hexSet.Add(center.dnHex);
            hexSet.Add(center.drHex);
            hexSet.Add(center.urHex);
        }
    }

    public ShipController createShip(string name, HexController position)
    {
        GameObject g = (GameObject)Instantiate(Resources.Load(name));
        ShipController s = g.GetComponent<ShipController>();
        s.myShip = new Ship();
        s.myShip.setDirection(1);
        s.board = this;
        s.hex = position;
        s.myShip.setPosition(position.myHex);
        PropulsionSystem ps = new PropulsionSystem();
        s.myShip.setPropulsionCount(1);
        s.myShip.addPropulsion(0,ps);
        ps.setMoves(6);
        ps.setTurnCost(1);
        ps.setMoveCost(1);
        GameObject.Find("MenuProvider").GetComponent<ActionMenu>().setShip(s);
        return s;
    }

    public HexController findHexController(Hex h)
    {
        foreach (HexController hc in hexSet)
        {
            if (hc.myHex.Equals(h))
            {
                return hc;
            }
        }
        return null;
    }
}
