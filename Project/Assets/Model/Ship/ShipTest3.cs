using UnityEngine;
using System.Collections;

public class ShipTest3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001SimulateMove();
        test002MovementEndingInInvalidHex();
	}
	
	void test001SimulateMove() 
    {
        print("Ship Test3 1: move simulation");
        Gameboard b = new Gameboard(6);
        Ship s = new Ship();
        s.setPosition(b.getHex("0"));
        PropulsionSystem p = new PropulsionSystem();
        s.setPropulsionCount(1);
        s.addPropulsion(0, p);
        s.setDirection(1);
        p.setMoves(6);
        p.setMoveCost(1); p.setTurnCost(1);
        Hex h = s.simulateMove("124");
        print("Test 1-1: Simulate a Move");
        DebugUtil.Assert(s.getPosition() == b.getHex("0") && h == b.getHex("124") && s.getDirection() == 1);
        print("Test 1: Tests Passed.");
    }

    void test002MovementEndingInInvalidHex()
    {
        print("Ship Test3 2: Movement to filled hexes");
        Gameboard b = new Gameboard(6);
        Ship s = new Ship();
        Ship t = new Ship();
        PropulsionSystem p = new PropulsionSystem();
        s.setPropulsionCount(1);
        s.addPropulsion(0, p);
        p.setMoves(6);
        p.setMoveCost(1); p.setTurnCost(1);
        print("Test 2-1: Movement through filled Hex");
        t.setPosition(b.getHex("11"));
        s.setPosition(b.getHex("1"));
        s.followPath("111");
        DebugUtil.Assert(s.getPosition() == b.getHex("1111"));
        print("Test 2-2: Move Ending on Filled Hex");
        s.setPosition(b.getHex("1"));
        t.setPosition(b.getHex("0"));
        s.followPath("4");
        DebugUtil.Assert(s.getPosition() == b.getHex("1"));
        print("Test 2-3: Move Ending on Filled Hex with path continuing");
        s.setPosition(b.getHex("1"));
        t.setPosition(b.getHex("164"));
        s.followPath("64");
        DebugUtil.Assert(s.getPosition() == b.getHex("16") && s.getDirection() == 4);
        print("Test 2: Tests Passed");
    }
}
