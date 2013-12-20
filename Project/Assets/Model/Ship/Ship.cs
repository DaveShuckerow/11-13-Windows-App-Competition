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
    int direction;
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

    public Hex simulateMove(string path)
    {
        Hex final; Hex start;
        int startDirection = getDirection();
        start = getPosition();
        makeMove(path);
        final = getPosition();
        setPosition(start);
        setDirection(startDirection);
        return final;
    }

    public List<ShipLocation> followPath(string path)
    {
        return makeMove(path);
    }

    private List<ShipLocation> makeMove(string path)
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
            Debug.Log(tempDir + " " + nextPos + " " + directionDifference(tempDir, nextPos));
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
                //if (tempDir == 7) Debug.Log(dirChange);
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
        if (current.isReachable() || current == getPosition())
        {
            setDirection(tempDir);
            setPosition(current);
        }
        else
        {
            // Look backwoard through the posPath for a reachable hex.
            int lastHex = 0;
            for (int i = 0; i < posPath.Count; i++)
            {
                if (posPath[i].position.isReachable())
                    lastHex = i;
            }
            // Remove all elements after lastHex.
            while (posPath.Count > lastHex + 1)
            {
                posPath.RemoveAt(lastHex + 1);
            }
            setDirection(posPath[lastHex].direction);
            setPosition(posPath[lastHex].position);
        }
        return posPath;
    }

    private int directionDifference(int dir1, int dir2)
    {
        int diff = Math.Abs(dir1 - dir2);
        if (Math.Abs(MAX_DIRS - diff) < Math.Abs(diff))
            diff = Math.Abs(MAX_DIRS - diff);
        return diff;
    }

    public HashSet<Hex> reachableHexes()
    {
        HashSet<Hex> reachable = new HashSet<Hex>();
        reachable.Add(position);
        int mov = getMoves();
        int dir = getDirection();
        reachable.UnionWith(reachableHelper(mov, dir, position));
        reachable.Remove(null);
        return reachable;
    }

    // Return all hexes we could reach in movesLeft moves from direction dir.
    public HashSet<Hex> reachableHelper(int movesLeft, int dir, Hex current)
    {
        while (dir <= 0) dir += MAX_DIRS;
        while (dir > MAX_DIRS) dir -= MAX_DIRS;

        HashSet<Hex> reachable = new HashSet<Hex>();
        if (current != null && current.isReachable())
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

        if (movesLeft > 3 * getTurnCost())
        {
            reachable.UnionWith(reachableHelper(movesLeft - 3 * (int)Math.Round(getTurnCost()), dir + 3, current));
        }

        return reachable;
    }
    // Getters/Setters
    public int getMoves()
    {
        int move = 0;
         for (int i = 0; i < propSys; i ++) {
             if (propList[i] is PropulsionSystem && propList[i].getStatus())
             {
                 PropulsionSystem p = (PropulsionSystem)propList[i];
                 move += p.getMoves();
             }
         }
         return move;
    }
    public double getMoveCost()
    {
        double daMoveCost = 0;
        for (int i = 0; i < propSys; i++)
        {
            if (propList[i] is PropulsionSystem && propList[i].getStatus())
            {
                PropulsionSystem p = (PropulsionSystem)propList[i];
                daMoveCost += p.getMoveCost()*p.getMoves();
            } 
        }
        if (daMoveCost <= 0.1)
        {
            daMoveCost = 0.1;
            return daMoveCost;
        }
        else
        {
            return daMoveCost/getMoves();
        }
    }
    public double getTurnCost()
    {
        double daTurnCost = 0;
        for (int i = 0; i < propSys; i++)
        {
            if (propList[i] is PropulsionSystem && propList[i].getStatus())
            {
                PropulsionSystem p = (PropulsionSystem)propList[i];
                daTurnCost += p.getTurnCost()*p.getMoves();
            }
        }
        if (getMoves() < 1) {
            return 0;
        }
        return daTurnCost/getMoves();
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
        if (p != null && p.isReachable())
        {
            // Only 1 ship can occupy a hex at a time.
            if (position != null)
                position.setReachable(true);
            position = p;
            p.setReachable(false);
        }
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
        if (multi <= 0.1)
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
        if (index >= 0 && index < controlSys && !controlList.Contains(c) && (c.getShip() == null || c.getShip() == this))
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
        if (index >= 0 && index < controlSys)
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
        if (index >= 0 && index < controlSys)
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
   
        if (index >= 0 && index < utilSys && !utilityList.Contains(u) && (u.getShip() == null || u.getShip() == this))
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
         if (index >= 0 && index < utilSys)
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
        if (index >= 0 && index < utilSys)
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
        if (index >= 0 && index < propSys && !propList.Contains(p) && (p.getShip() == null || p.getShip() == this))
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
                return null;
            }
          
        }
        return p;
    }
       
    

    public PropulsionSystem removePropulsion(int index)
    {
        if (index >= 0 && index < propSys)
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
        if (index >= 0 && index < propSys)
        {
            return propList[index];
        }
        else
        {
            return null;
        }
    }

  
    // Weapon System Methods
    public bool[] fire(Ship target)
    {
        List<bool> result = new List<bool>();
        for (int i = 0; i < utilSys; i++)
        {
            if (utilityList[i] is WeaponSystem)
            {
                WeaponSystem w = (WeaponSystem)utilityList[i];
                result.Add(w.fire(target));
            }
        }
        return result.ToArray();
    }
    public void fire(Ship target, bool[] acc)
    {
        int accCounter = 0;
        for (int i = 0; i < utilSys; i++)
        {
            if (utilityList[i] is WeaponSystem)
            {
                WeaponSystem w = (WeaponSystem)utilityList[i];
                if (accCounter < acc.Length)
                    w.fire(target, acc[accCounter]);
                else
                    w.fire(target, false);
                accCounter += 1;
            }
        }
    }

    // Shield System Methods

    public double getShieldHP() {
       double sum = 0;
         for (int i = 0; i < utilSys; i++)
         {
             if (utilityList[i] is ShieldSystem && utilityList[i].getStatus())
             {
                 sum += ((ShieldSystem)utilityList[i]).getShieldHP();
             }
         }
         return sum;
    }

    public double getMaxShieldHP()
    {
        double maxSum = 0;
        for (int i = 0; i < utilSys; i++)
        {
            if (utilityList[i] is ShieldSystem && utilityList[i].getStatus())
            {
                maxSum += ((ShieldSystem)utilityList[i]).getMaxShieldHP();
            }
        }
        return maxSum;
    }

    public double getShieldRecharge()
    {
        double rSum = 0;
        for (int i = 0; i < utilSys; i++)
        {
            if (utilityList[i] is ShieldSystem && utilityList[i].getStatus())
            {
                ShieldSystem ss = (ShieldSystem)utilityList[i];
                rSum += ss.getRecharge()*ss.getMaxShieldHP();
            }
        }
        if (rSum == 0)
            return 0;
        return rSum/getMaxShieldHP();
    }

    public void repair()
    {
        for (int i = 0; i < utilSys; i++)
        {
            if (utilityList[i] is ShieldSystem)
            {
                ((ShieldSystem)utilityList[i]).recharge();
            }
        }
    
    }

    public int damage(double dmg)
    {
        int activeSystems = 0;
//        double totHP = 0;
        for (int i=0; i<utilSys; i++) 
        {
            if (utilityList[i] is ShieldSystem && utilityList[i].getStatus() && ((ShieldSystem)utilityList[i]).getShieldHP() > 0)
            {
                //totHP += ((ShieldSystem)utilityList[i]).getShieldHP();
                activeSystems += 1;
            }
        }
        if (getShieldHP() <= 0 || activeSystems == 0)
        {
            // Damage the hull alone.
            setHP(getHP()-dmg);
            return 0;
        }
        else if (getShieldHP() >= dmg)
        {
            while (dmg > 0)
            {
                double damageRemaining = dmg;
                int sysRemaining = activeSystems;
                for (int i = 0; i < utilSys; i++)
                {
                    if (utilityList[i] is ShieldSystem && utilityList[i].getStatus() && ((ShieldSystem)utilityList[i]).getShieldHP() > 0)
                    {
                        ShieldSystem ss = (ShieldSystem)utilityList[i];
                        double dmgDone = Math.Min(dmg / activeSystems, ss.getShieldHP());
                        damageRemaining -= dmgDone;
                        ss.setShieldHP(ss.getShieldHP() - dmgDone);
                        if (ss.getShieldHP() <= 0)
                        {
                            sysRemaining -= 1;
                        }
                    }
                }
                dmg = damageRemaining;
                activeSystems = sysRemaining;
            }
            return 1;
        }
        else
        {
            while (activeSystems > 0)
            {
                double damageRemaining = dmg;
                int sysRemaining = activeSystems;
                for (int i = 0; i < utilSys; i++)
                {
                    if (utilityList[i] is ShieldSystem && utilityList[i].getStatus() && ((ShieldSystem)utilityList[i]).getShieldHP() > 0)
                    {
                        ShieldSystem ss = (ShieldSystem)utilityList[i];
                        double dmgDone = Math.Min(dmg / activeSystems, ss.getShieldHP());
                        damageRemaining -= dmgDone;
                        ss.setShieldHP(ss.getShieldHP() - dmgDone);
                        if (ss.getShieldHP() <= 0)
                        {
                            sysRemaining -= 1;
                        }
                    }
                }
                dmg = damageRemaining;
                activeSystems = sysRemaining;
            }
            setHP(getHP() - dmg);
            return 2;
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