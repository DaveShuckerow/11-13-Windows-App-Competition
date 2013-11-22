using UnityEngine;
using System.Collections;

public class PropulsionSystem : ShipSystem {
    int moves;
    double moveCost;
    double turnCost;
	// Use this for initialization
	void Start () {
	
	}

     
    public int getMoves() // Returns the number of moves a ship has.
    {
        return moves;
    }

    public void setMoves(int numOfMoves) // Sets the number of moves a ship has.
    {
        if (numOfMoves >= 0)
        {
            moves = numOfMoves;
        }
        else
        {
            moves = 0;
        }
    }

    public double getMoveCost()
    {
        return moveCost;
    }

    public void setMoveCost(double theMoveCost)
    {
        if (theMoveCost >= 0)
        {
            moveCost = theMoveCost;
        }
        else
        {
            moveCost = 0.1;
        }
    }

    public double getTurnCost()
    {
        return turnCost;
    }

    public void setTurnCost(double theTurnCost)
    {
        if (theTurnCost >= 0)
        {
            turnCost = theTurnCost;
        }
        else
        {
            turnCost = 0;
        }
    }
}
