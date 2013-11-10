using UnityEngine;
using System.Collections;

public class GameboardTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001GetHex();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void test001GetHex()
    {
        Gameboard b = new Gameboard(3);
        print("Test 1: 0 == 14");
        DebugUtil.Assert(b.getHex("0") == b.getHex("14"));
        print("Test 2: 124 == 13");
        DebugUtil.Assert(b.getHex("124") == b.getHex("13"));
        print("Test 3: 3331 == 233");
        DebugUtil.Assert(b.getHex("3331") == b.getHex("233"));
        print("Test 4: 6664 == 566");
        DebugUtil.Assert(b.getHex("6664") == b.getHex("665"));
        print("Test 5: 451 == 46");
        DebugUtil.Assert(b.getHex("451") == b.getHex("46"));
        try
        {
            b.getHex("1111");
        }
        catch {
            print("Handling invalid hexes: 1111");
        }
        try
        {
            b.getHex("3334");
        }
        catch
        {
            print("Handling invalid hexes: 3334");
        }
        try
        {
            b.getHex("6166");
        }
        catch
        {
            print("Handling invalid hexes: 61661");
        }
        try
        {
            b.getHex("4445");
        }
        catch
        {
            print("Handling invalid hexes: 4445");
        }
        try
        {
            b.getHex("2223333");
        }
        catch
        {
            print("Handling invalid hexes: 2223333");
        }
        print("Tests Passed.");

    }
}
