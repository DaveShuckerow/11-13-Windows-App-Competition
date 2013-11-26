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
        test003FireTesting();
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

    void test003FireTesting()
    {
        print("WeaponSystem Test 3: Firing Scenarios");
        WeaponSystem w = new WeaponSystem();
        w.setDamage(1);
        Ship t = new Ship();
        t.setMaxHP(10);
        t.setHP(10);
        print("WeaponSystem Test 3-1: Fire hit");
        w.fire(t, true);
        DebugUtil.Assert(t.getHP() == 9);
        print("WeaponSystem Test 3-2: Fire miss");
        w.fire(t, false);
        DebugUtil.Assert(t.getHP() == 9);
        print("WeaponSystem Test 3-3: Fire random");
        // Exactly how to do firing will be determined later.
        bool result = w.fire(t);
        DebugUtil.Assert(result == true || result == false);
        print("WeaponSystem Test 3-4: Fire disabled");
        w.setStatus(false);
        result = w.fire(t);
        DebugUtil.Assert(result == false);
        print("WeaponSystem Test 3-5: Fire disabled hit target");
        w.setStatus(false);
        t.setHP(10);
        w.fire(t,true);
        DebugUtil.Assert(t.getHP() == 10);
        print("WeaponSystem Test 3 passed.");
    }
}
