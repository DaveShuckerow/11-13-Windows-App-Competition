using UnityEngine;
using System.Collections.Generic;
using System;

public class HexController : MonoBehaviour {
    [SerializeField]
    Material[] Materials;

    public Hex myHex;
    public HexController upHex, ulHex, dlHex, dnHex, drHex, urHex;
    bool initialized = false;

    public static HexController mouseHex;
    public static ShipController mouseShip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setHex(Hex h)
    {
        myHex = h;
    }

    public void colorize(Color color)
    {
        GetComponent<MeshRenderer>().renderer.material.color = color;
    }

    public void finalizeHexes()
    {
        //if (upHex == null || ulHex == null || dlHex == null || dnHex == null || drHex == null || urHex == null)
            //return;
        // UpHex:
        if (upHex != null)
        {
            upHex.drHex = urHex;
            upHex.dlHex = ulHex;
        }
        // UlHex:
        if (ulHex != null)
        {
            ulHex.urHex = upHex;
            ulHex.dnHex = dlHex;
        }
        // DlHex:
        if (dlHex != null)
        {
            dlHex.upHex = ulHex;
            dlHex.drHex = dnHex;
        }
        // DnHex:
        if (dnHex != null)
        {
            dnHex.ulHex = dlHex;
            dnHex.urHex = drHex;
        }
        // DrHex:
        if (drHex != null)
        {
            drHex.dlHex = dnHex;
            drHex.upHex = urHex;
        }
        // UrHex:
        if (urHex != null)
        {
            urHex.dnHex = drHex;
            urHex.ulHex = upHex;
        }
        initialized = true;
    }

    public static Vector3 hexDirToVector(int dir)
    {
        switch (dir)
        {
            case 1:
                return new Vector3((float)Math.Cos(3 * Math.PI / 6), 0, (float)Math.Sin(3 * Math.PI / 6));
            case 2:
                return new Vector3((float)Math.Cos(5 * Math.PI / 6), 0, (float)Math.Sin(5 * Math.PI / 6));
            case 3:
                return new Vector3((float)Math.Cos(7 * Math.PI / 6), 0, (float)Math.Sin(7 * Math.PI / 6));
            case 4:
                return new Vector3((float)Math.Cos(9 * Math.PI / 6), 0, (float)Math.Sin(9 * Math.PI / 6));
            case 5:
                return new Vector3((float)Math.Cos(11 * Math.PI / 6), 0, (float)Math.Sin(11 * Math.PI / 6));
            case 6:
                return new Vector3((float)Math.Cos(1 * Math.PI / 6), 0, (float)Math.Sin(1 * Math.PI / 6));
            default:
                return Vector3.zero;
        }
    }

    public static string hexesToPath(List<HexController> l)
    {
        HexController prev = null;
        string path = "";
        foreach (HexController h in l)
        {
            if (prev == null) {
                path = "";
            }
            else if (prev == h.upHex)
            {
                path += "4";
            }
            else if (prev == h.ulHex)
            {
                path += "5";
            }
            else if (prev == h.dlHex)
            {
                path += "6";
            }
            else if (prev == h.dnHex)
            {
                path += "1";
            }
            else if (prev == h.drHex)
            {
                path += "2";
            }
            else if (prev == h.urHex)
            {
                path += "3";
            }
            else
            {
                // end path if we get to a break in the path.
                return path;
            }
            prev = h;
        }
        return path;
    }

    public static void computeMouseHex()
    {
        // Thanks to: http://answers.unity3d.com/questions/16676/how-can-i-make-my-gameobject-find-the-nearest-obje.html
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.y));
        GameObject[] hexes = GameObject.FindGameObjectsWithTag("Hex");
        double nearestDistance = Mathf.Infinity;
        Transform nearest = null;
        for (int i = 0; i < hexes.Length; i++)
        {
            GameObject obj = hexes[i];
            Vector3 objPos = obj.transform.position;
            double distance = (objPos - mousePosition).magnitude;
            if (distance < nearestDistance)
            {
                nearest = obj.transform;
                nearestDistance = distance;
            }
        }
        mouseHex = nearest.GetComponent<HexController>();
        mouseShip = null;
        UnityEngine.Object[] ships = GameObject.FindObjectsOfType<ShipController>();
        for (int i = 0; i < ships.Length; i++)
        {
            ShipController s = ((ShipController)(ships[i]));
            if (s.hex == mouseHex)
                mouseShip = s;
        }
    }
}
