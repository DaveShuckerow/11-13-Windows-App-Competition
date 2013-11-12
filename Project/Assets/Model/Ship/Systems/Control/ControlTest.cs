/**************************************************
 * By David Shuckerow
 * Test the ControlSystem class as of iteration 2.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System.Collections;

public class ControlTest : MonoBehaviour {

	void Start () {
        test001Creation();
	}

    // No functionality for ControlSystem is planned in this iteration beyond creation with a specific ship.
    void test001Creation()
    {
        ControlSystem c = new ControlSystem();
        DebugUtil.Assert(c is ShipSystem);
    }
}
