using UnityEngine;
using System.Collections;

public class ShipSystem  
{
    Ship s;
    bool status = true;
    // This is some weird C# thing called a property.  I don't know exactly how it works.
    // Google told me about it.
    // Scott Manley bless Stack Overflow...
    public virtual int maxLevel { get { return 1; } }
    protected int level = 1;
    
    public ShipSystem(Ship t)
    {
        s = t;
    }

    public ShipSystem()
    {
        s = null;
    }
    public void setShip(Ship t)
    {
        s = t;
        setStatus(true);
    }
    public Ship getShip()
    {
        return s;
    }
    public bool getStatus()
    {
        return status;
    }

    public void setStatus(bool boo)
    {
        status = boo; 
    }

    public void setLevel(int lev)
    {
        level = Mathf.Min(maxLevel, Mathf.Max(1, lev));
    }

    public int getLevel()
    {
        return level;
    }    
}