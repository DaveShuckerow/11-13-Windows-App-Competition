using UnityEngine;
using System.Collections.Generic;
using System;

public class Gameboard {
    int size;
    List<Hex> hexes;
	// Use this for initialization
	public Gameboard(int s) {
        size = s;
        hexes = new List<Hex>();
        expand(new Hex(), size);
        createView();
	}

    // Approach: Follow path to a target Hex.
    // Handle null hexes by returning last valid hex in list.
    public Hex getHex(string path, Hex center=null)
    {
        if (center == null)
            center = hexes[0];

        if (path[0] == '0')
            return center;
        Hex current = center;
        for (int i = 0; i < path.Length; i++)
        {
            Hex next = null;
            switch (path[i])
            {
                case '1':
                    next = current.getUp(); break;
                case '2':
                    next = current.getUl(); break;
                case '3':
                    next = current.getDl(); break;
                case '4':
                    next = current.getDn(); break;
                case '5':
                    next = current.getDr(); break;
                case '6':
                    next = current.getUr(); break;
            }
            if (next == null)
                throw new Exception(path + " broken at " + i);
            else
                current = next;
        }
        return current;
    }

    // Recursively expand the game board up to its appropriate size.
    private void expand(Hex center, int times)
    {
        if (times <= 0)
            return;

        hexes.Add(center);

        if (center.getUp() == null)
            center.setUp(new Hex());
        if (center.getUl() == null)
            center.setUl(new Hex());
        if (center.getDl() == null)
            center.setDl(new Hex());
        if (center.getDn() == null)
            center.setDn(new Hex());
        if (center.getDr() == null)
            center.setDr(new Hex());
        if (center.getUr() == null)
            center.setUr(new Hex());
        center.finalizeNeighbors();
        
        // Expand again.
        if (times > 0)
        {
            expand(center.getUp(), times - 1);
            expand(center.getUl(), times - 1);
            expand(center.getDl(), times - 1);
            expand(center.getDn(), times - 1);
            expand(center.getDr(), times - 1);
            expand(center.getUr(), times - 1);
        }
        else
        {
            hexes.Add(center.getUp());
            hexes.Add(center.getUl());
            hexes.Add(center.getDl());
            hexes.Add(center.getDn());
            hexes.Add(center.getDr());
            hexes.Add(center.getUr());
        }
    }

    // TODO: Create the rendering view over the gameboard model.
    private void createView()
    {
    }
}
