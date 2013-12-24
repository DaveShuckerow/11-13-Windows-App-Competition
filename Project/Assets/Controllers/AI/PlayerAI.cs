using UnityEngine;
using System.Collections;

public class PlayerAI : AIController {

    public override void startMove(GameboardController cntrl, ShipController myShip)
    {
        myController = cntrl;
        ship = myShip;
        aiState = 1; wait = 0;

        ship.hex.colorize(Color.blue);

        GameObject.Find("MenuProvider").GetComponent<ActionMenu>().caller = this;
        GameObject.Find("MenuProvider").GetComponent<ActionMenu>().setShip(myShip);
    }

    public override void endMove()
    {
        base.endMove();
    }

    public void isDone()
    {
        aiState = 4;
    }

    public override void update()
    {
        if (aiState == 4 && GameObject.Find("BigExplosion(Clone)") == null)
        {
            aiState = 5;
        }
        else if (aiState == 5)
        {
            endMove();
        }
        return;
    }
}
