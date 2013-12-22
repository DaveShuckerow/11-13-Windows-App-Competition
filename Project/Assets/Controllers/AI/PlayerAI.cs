using UnityEngine;
using System.Collections;

public class PlayerAI : AIController {

    public override void startMove(GameboardController cntrl, ShipController myShip)
    {
        myController = cntrl;
        ship = myShip;
        aiState = 1; wait = 0;

        GameObject.Find("MenuProvider").GetComponent<ActionMenu>().caller = this;
        GameObject.Find("MenuProvider").GetComponent<ActionMenu>().setShip(myShip);
    }

    public override void endMove()
    {
        base.endMove();
    }
}
