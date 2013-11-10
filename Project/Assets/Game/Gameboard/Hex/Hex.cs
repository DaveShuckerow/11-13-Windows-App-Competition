using UnityEngine;
using System.Collections;

public class Hex {
    protected Hex upHex, ulHex, dlHex, dnHex, drHex, urHex;

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
    // Hex directions: [1, 2, 3, 4, 5, 6]
    //                  up ul dl dn dr ur
    public Hex getNeighbor(int dir)
    {
        switch (dir)
        {
            case 1:
                return getUp();
            case 2:
                return getUl();
            case 3:
                return getDl();
            case 4:
                return getDn();
            case 5:
                return getDr();
            case 6:
                return getUr();
            default:
                return this;
        }
    }
    public bool finalizeNeighbors()
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
