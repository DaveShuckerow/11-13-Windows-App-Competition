using UnityEngine;
using System.Collections;

public class HexTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001HexLining();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void test001HexLining() {
        Hex h1 = new Hex();
        Hex h2 = new Hex();
        Hex h3 = new Hex();
        Hex h4 = new Hex();
        Hex h5 = new Hex();
        Hex h6 = new Hex();
        Hex h7 = new Hex();
        h1.setUp(h2);
        h1.setUl(h3);
        h1.setDl(h4);
        h1.setDn(h5);
        h1.setDr(h6);
        h1.setUr(h7);
        bool cond1 = h1.getUp() == h2 && h1.getUl() == h3 && h1.getDl() == h4 &&
                h1.getDn() == h5 && h1.getDr() == h6 && h1.getUr() == h7;
        bool cond2 = h2.getDn() == h1 && h3.getDr() == h1 && h4.getUr() == h1 &&
                h5.getUp() == h1 && h6.getUl() == h1 && h7.getDl() == h1;
        print("Test 1: Assignment");
        DebugUtil.Assert(cond1);
        print("Test 2: Reflexivity");
        DebugUtil.Assert(cond2);
        print("Test 3: Completeness");
        DebugUtil.Assert(h1.getUl() == h2.getDl() && h1.getUr() == h2.getDr());
        print("Test 3-1: passed");
        DebugUtil.Assert(h1.getDl() == h3.getDn() && h1.getUp() == h3.getUr());
        print("Test 3-2: passed");
        DebugUtil.Assert(h1.getDn() == h4.getDr() && h1.getUl() == h4.getUp());
        print("Test 3-3: passed");
        DebugUtil.Assert(h1.getDr() == h5.getUr() && h1.getDl() == h5.getUl());
        print("Test 3-4: passed");
        DebugUtil.Assert(h1.getUr() == h6.getUp() && h1.getDn() == h6.getDl());
        print("Test 3-5: passed");
        DebugUtil.Assert(h1.getUp() == h7.getUl() && h1.getDr() == h7.getDn());
        print("Test 3-6: passed");
    }
}
