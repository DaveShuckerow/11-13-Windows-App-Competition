using UnityEngine;
using System.Collections;

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
}
