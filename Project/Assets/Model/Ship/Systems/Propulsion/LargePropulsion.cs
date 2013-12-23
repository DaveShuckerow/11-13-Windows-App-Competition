using UnityEngine;
using System.Collections;

public class LargePropulsion : PropulsionSystem 
{
    public LargePropulsion()
    {
        setMoves(2);
        setMoveCost(1);
        setTurnCost(1);
    }
}
