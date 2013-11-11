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
        hex.setHex(center);
        g.transform.parent = transform;
        initDisplayHelper(center, new HashSet<Hex>());
    }
    private void initDisplayHelper(Hex center, HashSet<Hex> found) 
    {
        if (found.Contains(center))
            return;
        found.Add(center);
        
    }
}
