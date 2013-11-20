using UnityEngine;
using System.Collections;
using System;

public class HexController : MonoBehaviour {
    [SerializeField]
    Material[] Materials;

    public Hex myHex;
    public HexController upHex, ulHex, dlHex, dnHex, drHex, urHex;
    bool initialized = false;

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

    public void finalizeHexes()
    {
        if (upHex == null || ulHex == null || dlHex == null || dnHex == null || drHex == null || urHex == null)
            return;
        // UpHex:
        upHex.drHex = urHex;
        upHex.dlHex = ulHex;
        // UlHex:
        ulHex.urHex = upHex;
        ulHex.dnHex = dlHex;
        // DlHex:
        dlHex.upHex = ulHex;
        dlHex.drHex = dnHex;
        // DnHex:
        dnHex.ulHex = dlHex;
        dnHex.urHex = drHex;
        // DrHex:
        drHex.dlHex = dnHex;
        drHex.upHex = urHex;
        // UrHex:
        urHex.dnHex = drHex;
        urHex.ulHex = upHex; 
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
}
