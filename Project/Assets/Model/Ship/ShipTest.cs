/**************************************************
 * By David Shuckerow
 * Test the Ship class as of iteration 1.
 * Updated to conform to projected specs for iteration 2.
 * If any acceptance problems exist with this code, let me know.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System.Collections.Generic;

public class ShipTest : MonoBehaviour {
    void Start() {
        test001GridReachability();
        test002PathFollowing();
        test003PathPositions();
        test004NullPath();
    }

    void test001GridReachability() {
        Ship s = new Ship();
        Gameboard b = new Gameboard(6);
        s.setPropulsionCount(1);
        PropulsionSystem ps = new PropulsionSystem();
        ps.setShip(s);
        s.addPropulsion(0, ps);
        ps.setMoves(3);
        ps.setTurnCost(1);
        ps.setMoveCost(1);
        s.setPosition(b.getHex("444"));
        s.setDirection(1);
        print("Test 0: Ship basic data");
        DebugUtil.Assert(s.getMoves() == 3 && s.getTurnCost() == 1 && s.getMoveCost() == 1 && s.getDirection() == 1);
        print("Test 1: Ship position initializaiton");
        DebugUtil.Assert(s.getPosition() == b.getHex("444"));
        print("Test 2: Ship reachability across the grid");
        HashSet<Hex> reachable = s.reachableHexes();
        print("Test 2-1: Straight Ahead");
        DebugUtil.Assert(reachable.Contains(b.getHex("0")));
        DebugUtil.Assert(!reachable.Contains(b.getHex("1")));
        print("Test 2-2: Left Turn 1");
        DebugUtil.Assert(reachable.Contains(b.getHex("34")));
        DebugUtil.Assert(!reachable.Contains(b.getHex("3")));
        print("Test 2-3: Left Turn 2");
        DebugUtil.Assert(reachable.Contains(b.getHex("4443")));
        DebugUtil.Assert(!reachable.Contains(b.getHex("44433")));
        print("Test 2-4: 180 Turn");
        DebugUtil.Assert(reachable.Contains(b.getHex("444")));
        DebugUtil.Assert(!reachable.Contains(b.getHex("4444")));
        print("Test 2-5: Right Turn 2");
        DebugUtil.Assert(reachable.Contains(b.getHex("4445")));
        DebugUtil.Assert(!reachable.Contains(b.getHex("44455")));
        print("Test 2-6: Right Turn 1");
        DebugUtil.Assert(reachable.Contains(b.getHex("54")));
        DebugUtil.Assert(!reachable.Contains(b.getHex("5")));
        print("Tests passed.");
    }

    void test002PathFollowing()
    {
        print("Test 3: Path following");
        Ship s = new Ship();
        Gameboard b = new Gameboard(6);
        s.setPropulsionCount(1);
        PropulsionSystem ps = new PropulsionSystem();
        ps.setShip(s);
        s.addPropulsion(0, ps);
        ps.setMoves(3);
        ps.setTurnCost(1);
        ps.setMoveCost(1);
        s.setPosition(b.getHex("444"));
        s.setDirection(1);
        print("Test 3-1: Path 111");
        Hex p = s.getPosition();
        s.followPath("111");
        DebugUtil.Assert(s.getPosition() == b.getHex("111", p) && s.getDirection() == 1);
        print("Test 3-2: Path 12");
        s.setPosition(p); s.setDirection(1);
        s.followPath("12");
        DebugUtil.Assert(s.getPosition() == b.getHex("12", p) && s.getDirection() == 2);
        print("Test 3-3: Path 22");
        s.setPosition(p); s.setDirection(1);
        s.followPath("22");
        DebugUtil.Assert(s.getPosition() == b.getHex("22", p) && s.getDirection() == 2);
        print("Test 3-4: Path 3");
        s.setPosition(p); s.setDirection(1);
        s.followPath("3");
        DebugUtil.Assert(s.getPosition() == b.getHex("3", p) && s.getDirection() == 3);
        print("Test 3-5: Path 23");
        s.setPosition(p); s.setDirection(1);
        s.followPath("23");
        DebugUtil.Assert(s.getPosition() == b.getHex("2", p) && s.getDirection() == 3);
        print("Test 3-6: Path 4");
        s.setPosition(p); s.setDirection(1);
        s.followPath("4");
        DebugUtil.Assert(s.getPosition() == b.getHex("0", p) && s.getDirection() == 4);
        print("Test 3-7: Path 16");
        s.setPosition(p); s.setDirection(1);
        s.followPath("16");
        DebugUtil.Assert(s.getPosition() == b.getHex("16", p) && s.getDirection() == 6);
        print("Test 3-8: Path 66");
        s.setPosition(p); s.setDirection(1);
        s.followPath("66");
        DebugUtil.Assert(s.getPosition() == b.getHex("66", p) && s.getDirection() == 6);
        print("Test 3-9: Path 5");
        s.setPosition(p); s.setDirection(1);
        s.followPath("5");
        DebugUtil.Assert(s.getPosition() == b.getHex("5", p) && s.getDirection() == 5);
        print("Test 3-10: Path 65");
        s.setPosition(p); s.setDirection(1);
        s.followPath("65");
        DebugUtil.Assert(s.getPosition() == b.getHex("6", p) && s.getDirection() == 5);
        print("Tests passed.");
    }

    void test004NullPath()
    {
        print("Test 5: Invalid paths");
        Ship s = new Ship();
        Gameboard b = new Gameboard(2);
        s.setPropulsionCount(1);
        PropulsionSystem ps = new PropulsionSystem();
        ps.setShip(s);
        s.addPropulsion(0, ps);
        ps.setMoves(3);
        ps.setTurnCost(1);
        ps.setMoveCost(1);
        s.setPosition(b.getHex("0"));
        s.setDirection(1);
        print("Test 5-1: Path 111");
        Hex p = s.getPosition();
        s.followPath("111");
        DebugUtil.Assert(s.getPosition() == b.getHex("11", p) && s.getDirection() == 1);
        print("Tests passed.");
    }

    void test003PathPositions()
    {
        print("Test 4: Path positions");
        Ship s = new Ship();
        Gameboard b = new Gameboard(6);
        s.setPropulsionCount(1);
        PropulsionSystem ps = new PropulsionSystem();
        ps.setShip(s);
        s.addPropulsion(0, ps);
        ps.setMoves(3);
        ps.setTurnCost(1);
        ps.setMoveCost(1);
        Hex p = b.getHex("444");
        print("Test 4-1: Path 111");
        s.setPosition(p); s.setDirection(1);
        List<ShipLocation> l = s.followPath("111");
        List<ShipLocation> m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p.getUp(), 1)); m.Add(new ShipLocation(p.getUp().getUp(), 1));
        m.Add(new ShipLocation(p.getUp().getUp().getUp(), 1));
        DebugUtil.Assert(listEqual(l,m));
        print("Test 4-2: Path 12");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("12");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p.getUp(), 1)); m.Add(new ShipLocation(p.getUp(), 2));
        m.Add(new ShipLocation(p.getUp().getUl(), 2));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-3: Path 22");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("22");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p, 2)); m.Add(new ShipLocation(p.getUl(), 2));
        m.Add(new ShipLocation(p.getUl().getUl(), 2));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-4: Path 3");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("3");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p, 2)); m.Add(new ShipLocation(p, 3));
        m.Add(new ShipLocation(p.getDl(), 3));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-5: Path 23");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("23");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p, 2)); m.Add(new ShipLocation(p.getUl(), 2));
        m.Add(new ShipLocation(p.getUl(), 3));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-6: Path 4");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("4");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p, 2)); m.Add(new ShipLocation(p, 3));
        m.Add(new ShipLocation(p, 4));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-7: Path 16");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("16");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p.getUp(), 1)); m.Add(new ShipLocation(p.getUp(), 6));
        m.Add(new ShipLocation(p.getUp().getUr(), 6));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-8: Path 66");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("66");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p, 6)); m.Add(new ShipLocation(p.getUr(), 6));
        m.Add(new ShipLocation(p.getUr().getUr(), 6));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-9: Path 5");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("5");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p, 6)); m.Add(new ShipLocation(p, 5));
        m.Add(new ShipLocation(p.getDr(), 5));
        DebugUtil.Assert(listEqual(l, m));
        print("Test 4-10: Path 65");
        s.setPosition(p); s.setDirection(1);
        l = s.followPath("65");
        m = new List<ShipLocation>();
        m.Add(new ShipLocation(p, 1)); m.Add(new ShipLocation(p, 6)); m.Add(new ShipLocation(p.getUr(), 6));
        m.Add(new ShipLocation(p.getUr(), 5));
        DebugUtil.Assert(listEqual(l, m));
        print("Tests passed.");
    }

    private bool listEqual(List<ShipLocation> l, List<ShipLocation> m)
    {
        if (l.Count != m.Count) return false;
        for (int i = 0; i < l.Count; i++)
        {
            if (!l[i].Equals(m[i])) return false;
        }
        return true;
    }
}
