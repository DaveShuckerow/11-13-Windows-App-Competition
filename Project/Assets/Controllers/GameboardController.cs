using UnityEngine;
using System.Collections.Generic;
using System;

public class GameboardController : MonoBehaviour {
    [SerializeField]
    int size;
    [SerializeField]
    int hexRad = 96;
    Gameboard board;
	// Use this for initialization
	void Start () {
        board = new Gameboard(size);
        initDisplay();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void initDisplay()
    {
        Hex center = board.getHex("0"); 
        GameObject g = (GameObject)Instantiate(Resources.Load("HexPrefab"));
        HexController hex = g.GetComponent<HexController>();
        hex.myHex = center;
        g.transform.parent = transform;
        HashSet<HexController> hexSet = new HashSet<HexController>();
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
        }
        center.finalizeHexes();

        // Expand again.
        if (times > 0)
        {
            initDisplayExpand(center.upHex, times - 1);
            initDisplayExpand(center.ulHex, times - 1);
            initDisplayExpand(center.dlHex, times - 1);
            initDisplayExpand(center.dnHex, times - 1);
            initDisplayExpand(center.drHex, times - 1);
            initDisplayExpand(center.urHex, times - 1);
        }
    }

}
