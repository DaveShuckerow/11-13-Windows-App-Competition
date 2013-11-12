/**************************************************
 * By David Shuckerow
 * Test the WeaponSystem class as of iteration 2.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System.Collections;

public class WeaponTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001Creation();
        test002DamageSetup();
	}

    void test001Creation()
    {
        print("WeaponSystem Test 1: Creation & Ship Assignment");
        DebugUtil.Assert(new WeaponSystem() is UtilitySystem);
        print("WeaponSystem Test 1 passed");        
    }
    void test002DamageSetup()
    {
        print("WeaponSystem Test 2: Damage Scenarios");
        WeaponSystem w = new WeaponSystem();
        print("WeaponSystem Test 2-1: Damage Setup");
        w.setDamage(1);
        DebugUtil.Assert(w.getDamage() is double && w.getDamage() == 1.0);
        print("WeaponSystem Test 2 passed.");
    }
}
