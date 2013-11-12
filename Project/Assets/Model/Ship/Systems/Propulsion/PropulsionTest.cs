/**************************************************
 * By David Shuckerow
 * Test the PropulsionSystem class as of iteration 2.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System.Collections;

public class PropulsionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001Creation();
        test002MovingAndTurning();
	}

    void test001Creation()
    {
        print("PropulsionSystem Test 1: Creation");
        DebugUtil.Assert(new PropulsionSystem() is ShipSystem);
        print("PropulsionSystem Test 1 passed.");
    }
    void test002MovingAndTurning()
    {
        /*
         *  Ship movement cost needs to be expressed as a total number of moves per turn
         *  There should be a cost associated with both moving and turning.
         *  These will generally be integer-valued, but I want to allow for 0.25 or 0.5 move 
         *  and turn costs, just as examples.
         */
        print("PropulsionSystem Test 2: Moving and Turning Assignments");
        PropulsionSystem p = new PropulsionSystem();
        print("PropulsionSystem Test 2-1: Moves setup");
        p.setMoves(1);
        DebugUtil.Assert(p.getMoves() is int && p.getMoves() == 1);
        print("PropulsionSystem Test 2-2: Moves bounds");
        p.setMoves(-1);
        DebugUtil.Assert(p.getMoves() == 0);
        print("PropulsionSystem Test 2-3: Moving setup");
        p.setMoveCost(1);
        DebugUtil.Assert(p.getMoveCost() is double && p.getMoveCost() == 1);
        print("PropulsionSystem Test 2-4: Moving bounds");
        p.setMoveCost(-1);
        DebugUtil.Assert(p.getMoveCost() == 0.1);
        print("PropulsionSystem Test 2-5: Turning setup");
        p.setTurnCost(1);
        DebugUtil.Assert(p.getTurnCost() is double && p.getTurnCost() == 1);
        print("PropulsionSystem Test 2-6: Turning bounds");
        p.setTurnCost(-1);
        DebugUtil.Assert(p.getTurnCost() == 0);
        print("PropulsionSystem Test 2 passed.");
    }
}
