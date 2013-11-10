using UnityEngine;
using System.Collections;

public class ShipTest : MonoBehaviour {
    void Start() {
        test001GridReachability();
    }

    void test001GridReachability() {
        Ship s = new Ship();
        Gameboard b = new Gameboard(6);
        s.setPosition(b.getHex("444"));
    }
}
