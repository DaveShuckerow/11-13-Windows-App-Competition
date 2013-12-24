using UnityEngine;
using System.Collections;

public class MediumScenario : GameboardController
{

    protected override void setupFleets()
    {
        teams.Add(new Team());
        teams.Add(new Team());

        ShipController s1 = createShip("SajedFrigate", findHexController(board.getHex("112")), 4);
        s1.myShip.addControl(0, new Bridge());
        s1.myShip.addPropulsion(0, new MediumPropulsion());
        s1.myShip.addUtility(0, new WeakShield());
        s1.myShip.addUtility(1, new TorpedoSystem());
        s1.myShip.addUtility(2, new TorpedoSystem());
        s1.myShip.setMaxHP(5);

        ShipController s2 = createShip("SajedFrigate", findHexController(board.getHex("116")), 4);
        s2.myShip.addControl(0, new Bridge());
        s2.myShip.addPropulsion(0, new MediumPropulsion());
        s2.myShip.addUtility(0, new WeakShield());
        s2.myShip.addUtility(1, new TorpedoSystem());
        s2.myShip.addUtility(2, new TorpedoSystem());
        s2.myShip.setMaxHP(5);

        ShipController s3 = createShip("SajedCruiser", findHexController(board.getHex("11")), 4);
        s3.myShip.addControl(0, new Bridge());
        s3.myShip.addPropulsion(0, new LargePropulsion());
        s3.myShip.addUtility(0, new MedShield());
        s3.myShip.addUtility(1, new LaserSystem());
        s3.myShip.addUtility(2, new LaserSystem());
        s3.myShip.addUtility(3, new TorpedoSystem());
        s3.myShip.addUtility(4, new TorpedoSystem());
        s3.myShip.setMaxHP(7);

        ShipController t1 = createShip("BelliatCruiser", findHexController(board.getHex("44")));
        t1.myShip.addControl(0, new Bridge());
        t1.myShip.addPropulsion(0, new MediumPropulsion());
        t1.myShip.addUtility(0, new MedShield());
        t1.myShip.addUtility(1, new LaserSystem());
        t1.myShip.addUtility(2, new LaserSystem());
        t1.myShip.addUtility(3, new TorpedoSystem());
        t1.myShip.addUtility(4, new TorpedoSystem());
        t1.myShip.setMaxHP(7);

        ShipController t2 = createShip("BelliatFrigate", findHexController(board.getHex("443")));
        t2.myShip.addControl(0, new Bridge());
        t2.myShip.addPropulsion(0, new SmallPropulsion());
        t2.myShip.addUtility(0, new WeakShield());
        t2.myShip.addUtility(1, new TorpedoSystem());
        t2.myShip.addUtility(2, new TorpedoSystem());
        t2.myShip.setMaxHP(5);

        ShipController t3 = createShip("BelliatFrigate", findHexController(board.getHex("445")));
        t3.myShip.addControl(0, new Bridge());
        t3.myShip.addPropulsion(0, new SmallPropulsion());
        t3.myShip.addUtility(0, new WeakShield());
        t3.myShip.addUtility(1, new LaserSystem());
        t3.myShip.addUtility(2, new LaserSystem());
        t3.myShip.setMaxHP(5);

        shipList.Add(t2);
        shipList.Add(t3);
        shipList.Add(t1);
        shipList.Add(s1);
        shipList.Add(s2);
        shipList.Add(s3);

        teams[1].add(s1.myShip);
        teams[1].add(s2.myShip);
        teams[1].add(s3.myShip);
        teams[0].add(t1.myShip);
        teams[0].add(t2.myShip);
        teams[0].add(t3.myShip);

        teams[0].setAI(new PlayerAI());
        teams[1].setAI(new AIController());
    }
}
