/**************************************************
 * By David Shuckerow
 * A Ship class for the boardgame.
 * Capable of moving around the hexboard and seeing what hexes it can enter.
 * NOTE when adding new functionality:
 * if any old test cases fail with new behaviors, 
 *      check the old methods for direct uses of local variables instead of calls to the getter methods.
 *      check the test cases and that they're consistent with behavior that should be expected.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;

public class Ship
{
    Hex position;
    int moves, moveCost, turnCost, direction;
    const int MAX_DIRS = 6;
    int controlSys;
    int utilSys;
    int propSys;
    double maxShipHP;
    double shipHP;
    double multiplier;
    List<ControlSystem> controlList;
    List<UtilitySystem> utilityList;
    List<PropulsionSystem> propList;



    public Ship()
    {
    }

    public List<ShipLocation> followPath(string path)
    {
        List<ShipLocation> posPath = new List<ShipLocation>();
        posPath.Add(new ShipLocation(position, direction));

        int movesLeft = getMoves();
        Hex current = getPosition();
        int tempDir = direction;

        for (int i = 0; i < path.Length; i++)
        {
            int nextPos = Int32.Parse(path[i].ToString());
            if (nextPos == 0)
                break;
            int diff = Math.Abs(directionDifference(tempDir, nextPos));
            while (movesLeft >= getTurnCost() && diff > 0)
            {
                movesLeft -= (int)Math.Round(getTurnCost());
                diff -= 1;
                int dirChange = tempDir - nextPos;
                if (dirChange > 0 && dirChange <= 3)
                    tempDir -= 1;
                if (dirChange < 0 && dirChange >= -3)
                    tempDir += 1;
                if (dirChange > 3)
                    tempDir += 1;
                if (dirChange < -3)
                    tempDir -= 1;

                while (tempDir <= 0) tempDir += MAX_DIRS;
                while (tempDir > MAX_DIRS) tempDir -= MAX_DIRS;
                posPath.Add(new ShipLocation(current, tempDir));
            }
            if (movesLeft >= (int)Math.Round(getMoveCost()))
            {
                movesLeft -= (int)Math.Round(getMoveCost());
                if (current.getNeighbor(nextPos) == null)
                {
                    movesLeft = 0;
                    break;
                }
                current = current.getNeighbor(nextPos);
                posPath.Add(new ShipLocation(current, tempDir));
            }
        }
        setDirection(tempDir);
        setPosition(current);
        return posPath;
    }

    private int directionDifference(int dir1, int dir2)
    {
        int mid = MAX_DIRS / 2 + 1;
        return (Math.Abs(mid - dir1) - Math.Abs(mid - dir2));
    }

    public HashSet<Hex> reachableHexes()
    {
        HashSet<Hex> reachable = new HashSet<Hex>();
        reachable.Add(position);
        reachable.UnionWith(reachableHelper(moves, direction, position));
        reachable.Remove(null);
        return reachable;
    }

    // Return all hexes we could reach in movesLeft moves from direction dir.
    public HashSet<Hex> reachableHelper(int movesLeft, int dir, Hex current)
    {
        while (dir <= 0) dir += MAX_DIRS;
        while (dir > MAX_DIRS) dir -= MAX_DIRS;

        HashSet<Hex> reachable = new HashSet<Hex>();
        reachable.Add(current);

        if (movesLeft == 0)
            return reachable;

        if (movesLeft >= 1 * getMoveCost())
        {
            reachable.UnionWith(reachableHelper(movesLeft - 1 * (int)Math.Round(getMoveCost()), dir, current.getNeighbor(dir)));
        }

        if (movesLeft >= 2 * getTurnCost())
        {
            reachable.UnionWith(reachableHelper(movesLeft - 1 * (int)Math.Round(getTurnCost()), dir - 1, current));
            reachable.UnionWith(reachableHelper(movesLeft - 1 * (int)Math.Round(getTurnCost()), dir + 1, current));
        }

        if (movesLeft >= 3 * getTurnCost())
        {
            reachable.UnionWith(reachableHelper(movesLeft - 2 * (int)Math.Round(getTurnCost()), dir - 2, current));
            reachable.UnionWith(reachableHelper(movesLeft - 2 * (int)Math.Round(getTurnCost()), dir + 2, current));
        }

        if (movesLeft > 3 * turnCost)
        {
            reachable.UnionWith(reachableHelper(movesLeft - 3 * (int)Math.Round(getTurnCost()), dir + 3, current));
        }

        return reachable;
    }
    // Getters/Setters
    public void setMoves(int m)
    {
        moves = m;
    }
    public int getMoves()
    {
        int moves = 0;
         for (int i = 0; i < propList.Count; i ++) {
             if (propList[i] is PropulsionSystem && propList[i].getStatus() == true)
             {
                 moves += ((PropulsionSystem)propList[i]).getMoves();
             }
         }
         return moves;
    }
    
    public void setMoveCost(int m)
    {
        moveCost = m;
    }
    public double getMoveCost()
    {
        double daMoveCost = 0;
        for (int i = 0; i < propList.Count; i++)
        {
            if (propList[i] is PropulsionSystem && propList[i].getStatus() == true)
            {
                daMoveCost += (((PropulsionSystem)propList[i]).getMoves() * ((PropulsionSystem)propList[i]).getMoveCost());
            } 
        }
        daMoveCost = daMoveCost / getMoves();
        if (daMoveCost == 0)
        {
            daMoveCost = 0.1;
            return daMoveCost;
        }
        else
        {
            return daMoveCost;
        }
    }
    public void setTurnCost(int m)
    {
        turnCost = m;
    }
    public double getTurnCost()
    {
        double daTurnCost = 0;
        for (int i = 0; i < propList.Count; i++)
        {
            if (propList[i] is PropulsionSystem && propList[i].getStatus() == true)
            {
                daTurnCost += (((PropulsionSystem)propList[i]).getMoves() * ((PropulsionSystem)propList[i]).getTurnCost());
            }
        }
        daTurnCost = daTurnCost / getMoves();
        return daTurnCost;
    }
    public void setDirection(int d)
    {
        direction = d;
    }
    public int getDirection()
    {
        return direction;
    }
    public void setPosition(Hex p)
    {
        if (p != null)
            position = p;
    }
    public Hex getPosition()
    {
        return position;
    }

    public void setSystemCount(int controlNum, int utilNum, int propNum)
    {
        setControlCount(controlNum);
        setUtilityCount(utilNum);
        setPropulsionCount(propNum);
    }

    public void setControlCount(int controlVal)
    {
        controlSys = controlVal;
        controlList = new List<ControlSystem>(controlSys);
        for (int i = 0; i < controlSys; i++) controlList.Add(null);

    }

    public int getControlCount()
    {
        return controlSys;
    }

    public void setUtilityCount(int utilVal)
    {
        utilSys = utilVal;
        utilityList = new List<UtilitySystem>(utilSys);
        for (int i = 0; i < utilSys; i++) utilityList.Add(null);
    }

    public int getUtilityCount()
    {
        return utilSys;
    }

    public void setPropulsionCount(int propVal)
    {
        propSys = propVal;
        propList = new List<PropulsionSystem>(propSys);
        for (int i = 0; i < propSys; i++) propList.Add(null);
    }

    public int getPropulsionCount()
    {
        return propSys;
    }

    public void setMaxHP(double hpVal)
    {
        if (hpVal >= 0)
        {
            maxShipHP = hpVal;
            shipHP = hpVal;
        }
        else
        {
            maxShipHP = 0;
            shipHP = 0;
        }
    }

    public double getMaxHP()
    {
        return maxShipHP;
    }

    public void setHP(double hpVal)
    {
        if (hpVal <= maxShipHP)
        {
            if (hpVal >= 0)
            {
                shipHP = hpVal;
            }
            else
            {
                shipHP = 0;
            }
        }
        
    }

    public double getHP()
    {

        return shipHP;
    }

    public void setMoveMultiplier(double multi)
    {
        if (multi <= 0)
        {
            multiplier = 0.1;
        }
        else
        {
            multiplier = multi;
        }
    }

    public double getMoveMultiplier()
    {
        return multiplier;
    }

    // Control System Methods

    public ControlSystem addControl(int index, ControlSystem c)
    {
        if (index >= 0 && index < controlList.Count && !controlList.Contains(c) && c.getShip() == null)
        {
                if (controlList[index] != null)
                {
                    ControlSystem d = controlList[index];
                    controlList[index] = c;
                    c.setShip(this);
                    d.setShip(null);
                    return d;
                }
                else
                {
                    controlList[index] = c;
                    c.setShip(this);
                    return c;
                }
            
        }
        return c;
    }

    public ControlSystem removeControl(int index)
    {
        if (index >= 0 && index < controlList.Count)
        {

            ControlSystem c;
            c = controlList[index];
            controlList[index] = null;
            c.setShip(null);
            return c;
        }
        else
        {
            return null;
        }
        
    }

    public ControlSystem getControl(int index)
    {
        if (index >= 0 && index < controlList.Count)
        {
            return controlList[index];
        }
        else
        {
            return null;
        }
    }

   

    // Utility System Methods

    public UtilitySystem addUtility(int index, UtilitySystem u) {
   
        if (index >= 0 && index < utilityList.Count && !utilityList.Contains(u) && u.getShip() == null)
        {
                if (utilityList[index] != null)
                {
                    UtilitySystem d = utilityList[index];
                    utilityList[index] = u;
                    u.setShip(this);
                    d.setShip(null);
                    return d;
                }
                else
                {
                    utilityList[index] = u;
                    u.setShip(this);
                    return u;
                }
            
        }
        return u;
    }
    


    public UtilitySystem removeUtility(int index)
    {
         if (index >= 0 && index < utilityList.Count)
        {

            UtilitySystem u;
            u = utilityList[index];
            utilityList[index] = null;
            u.setShip(null);
            return u;
        }
        else
        {
            return null;
        }
    }

    public UtilitySystem getUtility(int index)
    {
        if (index >= 0 && index < utilityList.Count)
        {
            return utilityList[index];
        }
        else
        {
            return null;
        }
    }

  

    // Propulsion System Methods

    public PropulsionSystem addPropulsion(int index, PropulsionSystem p)
    {
        if (index >= 0 && index < propList.Count && !propList.Contains(p) && p.getShip() == null)
        {
            if (propList[index] != null)
            {
                PropulsionSystem d = propList[index];
                propList[index] = p;
                p.setShip(this);
                d.setShip(null);
                return d;
            }
            else
            {
                propList[index] = p;
                p.setShip(this);
                return p;
            }
          
        }
          return p;
    }
       
    

    public PropulsionSystem removePropulsion(int index)
    {
        if (index >= 0 && index < propList.Count)
        {

            PropulsionSystem p;
            p = propList[index];
            propList[index] = null;
            p.setShip(null);
            return p;
        }
        else
        {
            return null;
        }
    }

    public PropulsionSystem getPropulsion(int index)
    {
        if (index >= 0 && index < propList.Count)
        {
            return propList[index];
        }
        else
        {
            return null;
        }
    }

  
    // Weapon System Methods

   public void fire(Ship s, bool acc)
    {

    }

    // Shield System Methods

    public double getShieldHP() {
       double sum = 0;
         for (int i = 0; i < utilityList.Count; i ++) {
             if (utilityList[i] is ShieldSystem)
             {
                 sum += ((ShieldSystem)utilityList[i]).getShieldHP();
             }
         }
         return sum;
    }

    public double getMaxShieldHP()
    {
        double maxSum = 0;
        for (int i = 0; i < utilityList.Count; i++)
        {
            if (utilityList[i] is ShieldSystem)
            {
                maxSum += ((ShieldSystem)utilityList[i]).getMaxShieldHP();
            }
        }
        return maxSum;
    }

    public double getShieldRecharge()
    {
        double rSum = 0;
        for (int i = 0; i < utilityList.Count; i++)
        {
            if (utilityList[i] is ShieldSystem)
            {
                rSum += ((ShieldSystem)utilityList[i]).getRecharge();
            }
        }
        return rSum;
    }

    public void repair()
    {
        for (int i = 0; i < utilityList.Count; i++)
        {
            if (utilityList[i] is ShieldSystem)
            {
                ((ShieldSystem)utilityList[i]).recharge();
            }
        }
    
    }
}


public class ShipLocation
{
    public readonly Hex position;
    public readonly int direction;

    public ShipLocation(Hex h, int d)
    {
        position = h;
        direction = d;
    }

    public bool Equals(ShipLocation other)
    {
        if (!(other is ShipLocation))
            return base.Equals(other);
        return position == other.position && direction == other.direction;
    }



}