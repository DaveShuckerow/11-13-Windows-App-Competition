using UnityEngine;
using System.Collections;

public class SmallPropulsion : PropulsionSystem {

    public SmallPropulsion()
    {
        setMoves(4);
        setMoveCost(1);
        setTurnCost(1);
    }
}
