/**************************************************
 * By David Shuckerow
 * Test the ShieldSystem class as of iteration 2.
 * 11/11/2013
 **************************************************/
using UnityEngine;
using System.Collections;

public class ShieldSystemTest : MonoBehaviour {

	void Start () {
        test001Creation();
        test002ShieldHP();
        test003ShieldRecharge();
	}

    void test001Creation() {
        print("ShieldSystem Test 1: Creation");
        DebugUtil.Assert((new ShieldSystem()) is UtilitySystem);
        print("ShieldSystem Test 1 passed.");
    }

    void test002ShieldHP()
    {
        print("ShieldSystem Test 2: ShieldHP");
        ShieldSystem ss = new ShieldSystem();
        print("ShieldSystem Test 2-1: Max HP Assignment");
        ss.setMaxShieldHP(10);
        DebugUtil.Assert(ss.getMaxShieldHP() is double && ss.getMaxShieldHP() == 10.0);
        print("ShieldSystem Test 2-2: HP Assignment");
        ss.setShieldHP(5);
        DebugUtil.Assert(ss.getShieldHP() is double && ss.getShieldHP() == 5.0);
        print("ShieldSystem Test 2-3: Max HP Exceeded");
        ss.setShieldHP(20);
        DebugUtil.Assert(ss.getShieldHP() == 10.0);
        print("ShieldSystem Test 2-4: Max HP Reduced");
        ss.setMaxShieldHP(5);
        DebugUtil.Assert(ss.getShieldHP() == 5.0);
        print("ShieldSystem Test 2-5: Max HP Under 0");
        ss.setMaxShieldHP(-5);
        DebugUtil.Assert(ss.getMaxShieldHP() == 0);
        ss.setMaxShieldHP(10);
        ss.setShieldHP(10);
        print("ShieldSystem Test 2-6: HP Under 0");
        ss.setShieldHP(-5);
        DebugUtil.Assert(ss.getShieldHP() == 0);
        print("ShieldSystem Test 2 passed.");
    }

    void test003ShieldRecharge()
    {
        ShieldSystem ss = new ShieldSystem();
        print("ShieldSystem Test 3: Shield Recharge");
        print("ShieldSystem Test 3-1: Recharge Assignment");
        ss.setRecharge(1);
        DebugUtil.Assert(ss.getRecharge() is double && ss.getRecharge() == 1.0);
        print("ShieldSystem Test 3-2: Shield Recharging");
        ss.setMaxShieldHP(10);
        ss.setShieldHP(5);
        ss.recharge();
        DebugUtil.Assert(ss.getShieldHP() == 6);
        print("ShieldSystem Test 3-3: Recharge under 0");
        ss.setRecharge(-0.5);
        DebugUtil.Assert(ss.getRecharge() == 0);
        print("ShieldSystem Test 3 passed.");
    }
}
