/**************************************************
 * By David Shuckerow
 * Test the UtilitySystem class as of iteration 2.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System.Collections;

public class UtilityTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001Creation();
	}
	
    // No functionality for UtilitySystem is planned currently beyond creation.
    void test001Creation()
    {
        print("UtilitySystem Test 1: Creation");
        DebugUtil.Assert(new UtilitySystem() is ShipSystem);
        print("UtilitySystem Test 1 passed.");
    }
}
