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

public class Ship {
    Hex position;
    int moves, moveCost, turnCost, direction;
    const int MAX_DIRS = 6;
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
                movesLeft -= getTurnCost();
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
            if (movesLeft >= getMoveCost())
            {
                movesLeft -= getMoveCost();
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

        if (movesLeft >= 1*getMoveCost())
        {
            reachable.UnionWith(reachableHelper(movesLeft - 1*getMoveCost(), dir, current.getNeighbor(dir)));
        }
        
        if (movesLeft >= 2*getTurnCost()) {
            reachable.UnionWith(reachableHelper(movesLeft - 1*getTurnCost(), dir - 1, current));
            reachable.UnionWith(reachableHelper(movesLeft - 1*getTurnCost(), dir + 1, current));
        }

        if (movesLeft >= 3*getTurnCost()) {
            reachable.UnionWith(reachableHelper(movesLeft - 2*getTurnCost(), dir - 2, current));
            reachable.UnionWith(reachableHelper(movesLeft - 2*getTurnCost(), dir + 2, current));
        }

        if (movesLeft > 3*turnCost) 
        {
            reachable.UnionWith(reachableHelper(movesLeft - 3*getTurnCost(), dir + 3, current));
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
        return moves;
    }
    public void setMoveCost(int m)
    {
        moveCost = m;
    }
    public int getMoveCost()
    {
        return moveCost;
    }
    public void setTurnCost(int m)
    {
        turnCost = m;
    }
    public int getTurnCost()
    {
        return turnCost;
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

    public bool Equals(System.Object other)
    {
        if (!(other is ShipLocation))
            return base.Equals(other);
        return position == other.position && direction == other.direction;
    }
}