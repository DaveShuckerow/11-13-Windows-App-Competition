using UnityEngine;
using System.Collections;

public class ShipSystem  
{
    Ship s;
    bool status = true;
    // Use this for initialization
    void Start()
    {

    }

    
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
    
}