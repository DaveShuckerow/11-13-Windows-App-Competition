using UnityEngine;
using System.Collections.Generic;

public class Team {
    private HashSet<Ship> ships;

    private AIController ai;

    public Team()
    {
        ships = new HashSet<Ship>();
    }

    public Team add(Ship newShip)
    {
        ships.Add(newShip);
        return newShip.setTeam(this);
    }

    public bool contains(Ship ship)
    {
        return ships.Contains(ship);
    }

    public void remove(Ship ship)
    {
        ships.Remove(ship);
    }

    public int size()
    {
        return ships.Count;
    }

    public void setAI(AIController a)
    {
        ai = a;
    }

    public AIController getAI()
    {
        return ai;
    }
}
