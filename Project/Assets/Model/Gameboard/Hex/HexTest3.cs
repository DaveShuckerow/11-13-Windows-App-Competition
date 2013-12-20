using UnityEngine;
using System;
using System.Collections;

public class HexTest3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001HexDistance();
        test003Reachability();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void test001HexDistance()
    {
        Gameboard b = new Gameboard(6);
        Hex c = b.getHex("0");
        print("Hex Test 1: Hex Distance");
        print("Hex Test 1-1: up 3");
        print(c.getHexDistance(b.getHex("111")));
        DebugUtil.Assert(c.getHexDistance(b.getHex("111")) == 3);
        print("Hex Test 1-2: down 4");
        DebugUtil.Assert(c.getHexDistance(b.getHex("4444")) == 4);
        print("Hex Test 1-3: Same location");
        DebugUtil.Assert(c.getHexDistance(c) == 0);
        print("Hex Test 1-4: Mixed Path ul dl dn");
        DebugUtil.Assert(c.getHexDistance(b.getHex("234")) == 2);
        print("Hex Test 1-5: Path longer than distance");
        DebugUtil.Assert(c.getHexDistance(b.getHex("124")) == 1);
        print("Hex Test 1-6: Hex out of gameboard");
        // A hex out of the gameboard should return -1.
        DebugUtil.Assert(c.getHexDistance(new Hex()) == -1);
        print("Hex Test 1-7: Null Hex");
        DebugUtil.Assert(c.getHexDistance(null) == -1);
        print("Hex Test 1 passed.");
    }

    void test002Passability()
    {
        print("Hex Test 2: Hex Passability");
        // No tests yet until obstacles implemented.
        print("Hex Test 2 passed.");
    }

    void test003Reachability()
    {
        print("Hex Test 3: Hex Reachability");
        Gameboard b = new Gameboard(6);
        Hex c = b.getHex("0");
        Ship s = new Ship();
        s.setDirection(1);
        s.setPropulsionCount(1);
        PropulsionSystem p = new PropulsionSystem();
        p.setMoves(6); p.setMoveCost(1); p.setTurnCost(1);
        s.addPropulsion(0, p);
        print("Hex Test 3-1: Empty Hex Reachability");
        DebugUtil.Assert(c.isReachable() == true);
        print("Hex Test 3-2: Occupied Hex Reachability");
        s.setPosition(c);
        DebugUtil.Assert(c.isReachable() == false);
        print("Hex Test 3-3: Newly emptied Hex Reachability");
        s.followPath("5");
        DebugUtil.Assert(c.isReachable() == true);
        print("Hex Test 3-4: Newly entered Hex Reachability");
        DebugUtil.Assert(b.getHex("5").isReachable() == false);
        print("Hex Test 3-5: Reachable Hex List inclusion");
        Ship t = new Ship();
        t.setPosition(b.getHex("11", s.getPosition()));
        DebugUtil.Assert(s.reachableHexes().Contains(s.getPosition()));
        DebugUtil.Assert(!s.reachableHexes().Contains(t.getPosition()));

        print("Hex Test 3 passed.");
    }

}
