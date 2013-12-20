using UnityEngine;
using System.Collections;

public class WeaponTest3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001CoinFlip();
        test002MaxLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void test001CoinFlip()
    {
        Gameboard b = new Gameboard(6);
        Ship s = new Ship();
        s.setPosition(b.getHex("11"));
        Ship t = new Ship();
        t.setPosition(b.getHex("44"));
        s.setUtilityCount(1);

        print("WeaponTest3 1: Probabilities");
        WeaponSystem w1 = new LaserSystem();
        WeaponSystem w2 = new TorpedoSystem();
        s.addUtility(0, w1);
        print("Test 1-1: Laser Out of Range");
        DebugUtil.Assert(w1.hitProbability(t) == 0.0);
        print("Test 1-2: Laser Extreme Range");
        t.setPosition(b.getHex("4"));
        DebugUtil.Assert(w1.hitProbability(t) == 0.5);
        print("Test 1-3: Laser Medium Range");
        t.setPosition(b.getHex("0"));
        print(w1.hitProbability(t));
        print(s.getPosition().getHexDistance(t.getPosition()));
        DebugUtil.Assert(w1.hitProbability(t) == 0.7);
        print("Test 1-4: Laser Short Range");
        t.setPosition(b.getHex("1"));
        print(w1.hitProbability(t));
        print(s.getPosition().getHexDistance(t.getPosition()));
        DebugUtil.Assert(w1.hitProbability(t) == 0.9);
        print("Test 1-5: Torpedo Out of Range");
        s.addUtility(0, w2);
        t.setPosition(b.getHex("44"));
        DebugUtil.Assert(w2.hitProbability(t) == 0.0);
        print("Test 1-6: Torpedo Extreme Range");
        t.setPosition(b.getHex("4"));
        DebugUtil.Assert(w2.hitProbability(t) == 0.7);
        print("Test 1-7: Torpedo Medium Range");
        t.setPosition(b.getHex("0"));
        DebugUtil.Assert(w2.hitProbability(t) == 0.7);
        print("Test 1-8: Torpedo Short Range");
        t.setPosition(b.getHex("1"));
        DebugUtil.Assert(w2.hitProbability(t) == 0.7);
        print("WeaponTest3 1: Test passed");

    }

    void test002MaxLevel()
    {
        WeaponSystem w = new WeaponSystem();
        
        print("WeaponTest3 2: System Levels");
        DebugUtil.Assert(w.getLevel() == 1);
        w.setLevel(4);
        DebugUtil.Assert(w.getLevel() == 4);
        w.setLevel(5);
        DebugUtil.Assert(w.getLevel() == 4);
        w.setLevel(1);
        DebugUtil.Assert(w.getLevel() == 1);
        w.setLevel(0);
        DebugUtil.Assert(w.getLevel() == 1);
        print("WeaponTest3 2: Test passed");
    }
}
