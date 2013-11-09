using UnityEngine;
using System.Collections;

public class Hex : ScriptableObject {
    protected Hex upHex, ulHex, dlHex, dnHex, drHex, urHex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Hex getUp()
    {
        return upHex;
    }
    public Hex getUl()
    {
        return ulHex;
    }
    public Hex getDl()
    {
        return dlHex;
    }
    public Hex getDn()
    {
        return dnHex;
    }
    public Hex getDr()
    {
        return drHex;
    }
    public Hex getUr()
    {
        return urHex;
    }

    public void setUp(Hex up)
    {
        upHex = up;
        up.dnHex = this;
        finalizeNeighbors();
    }
    public void setUl(Hex ul)
    {
        ulHex = ul;
        ul.drHex = this;
        finalizeNeighbors();
    }
    public void setDl(Hex dl)
    {
        dlHex = dl;
        dl.urHex = this;
        finalizeNeighbors();
    }
    public void setDn(Hex dn)
    {
        dnHex = dn;
        dn.upHex = this;
        finalizeNeighbors();
    }
    public void setDr(Hex dr)
    {
        drHex = dr;
        dr.ulHex = this;
        finalizeNeighbors();
    }
    public void setUr(Hex ur)
    {
        urHex = ur;
        ur.dlHex = this;
        finalizeNeighbors();
    }
    private bool finalizeNeighbors()
    {
        if (upHex == null || ulHex == null || dlHex == null || dnHex == null || drHex == null || urHex == null)
            return false;
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
        return true;
    }
}
