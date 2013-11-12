/**************************************************
 * By David Shuckerow
 * Test the ShipSystem class as of iteration 2.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System.Collections;

public class SystemTest : MonoBehaviour {

	void Start () {
        test001CreationAndShipAssignment();
        test002StatusChanges();
	}
	
    // A ShipSystem must be given a ship as an argument.
    void test001CreationAndShipAssignment()
    {
        Ship s = new Ship();
        Ship t = new Ship();
        print("ShipSystem Test 1: Creation & Ship Assignment");
        print("ShipSystem Test 1-1: Creation");
        DebugUtil.Assert((new ShipSystem()) is ShipSystem);
        print("ShipSystem Test 1-2: Ship Assignment and Access");
        DebugUtil.Assert((new ShipSystem()).getShip() == s);
        print("ShipSystem Test 1-3: Ship Reassignment");
        ShipSystem ss = new ShipSystem();
        ss.setShip(t);
        DebugUtil.Assert(ss.getShip() == t);
        print("ShipSystem Test 1 passed.");
    }

    // Allow the changing of a ShipSystem's status to enabled and disabled.
    void test002StatusChanges()
    {
        Ship s = new Ship();
        ShipSystem ss = new ShipSystem();
        
        print("ShipSystem Test 2: Status Changes");
        print("ShipSystem Test 2-1: Initial Status");
        DebugUtil.Assert(ss.getStatus() == true);
        print("ShipSystem Test 2-2: Change Status");
        ss.setStatus(false);
        DebugUtil.Assert(ss.getStatus() == false);
        print("ShipSystem Test 2-3: Restore Status");
        ss.setStatus(true);
        DebugUtil.Assert(ss.getStatus() == true);
        print("ShipSystem Test 2-4: Restore Status to enabled on addition to a ship.");
        ss.setStatus(false);
        ss.setShip(s);
        DebugUtil.Assert(ss.getStatus() == true);
        print("ShipSystem Test 2 passed.");
    }
}
