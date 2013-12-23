using UnityEngine;
using System.Collections;

public class MediumPropulsion : PropulsionSystem 
{
    public MediumPropulsion()
    {
        setMoves(3);
        setMoveCost(1);
        setTurnCost(1);
    }
}
