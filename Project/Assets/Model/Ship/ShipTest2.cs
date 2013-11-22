/**************************************************
 * By David Shuckerow
 * Upgrade the Ship class as to blend with
 * ShipSystems from iteration 2.
 * 11/11/2013
 **************************************************/

using UnityEngine;
using System.Collections;

/*
 * TODO: Remove the setMoveCost, setMoves, and setTurnCost methods of Ship.
 *       Bring Ship in compliance with the following test cases:
 */
public class ShipTest2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        test001AddedMethods();
        test002ControlSystems();
        test003UtilitySystems();
        test004PropulsionSystems();
        test005WeaponSystems();
        test006ShieldSystems();
	}

    /*
     * Added Methods Clarification:
     * hp and maxhp are the ship's hull and maximum hull integrity.
     * moveMultiplier is a multiplier that will affect both getMoves, but not getMoveCost() and getTurnCost().
     * moveMultiplier's value will not go below 0.1.
     */
    void test001AddedMethods()
    {
        print("Ship Test 1: Added Methods");
        print("Ship Test 1-1: Subsystem Addition");
        Ship s = new Ship();
        s.setSystemCount(1,3,1);
        DebugUtil.Assert(s.getControlCount() == 1 && s.getUtilityCount() == 3 && s.getPropulsionCount() == 1);
        print("Ship Test 1-2: Max Armor/HP");
        s.setMaxHP(10);
        DebugUtil.Assert(s.getMaxHP() is double && s.getMaxHP() == 10.0);
        print("Ship Test 1-3: Armor/HP");
        s.setHP(10);
        DebugUtil.Assert(s.getHP() is double && s.getHP() == 10.0);
        print("Ship Test 1-4: HP exceeds Max");
        s.setHP(100);
        DebugUtil.Assert(s.getHP() == 10.0);
        print("Ship Test 1-5: Max HP below 0");
        s.setMaxHP(-1);
        DebugUtil.Assert(s.getMaxHP() == 0);
        print("Ship Test 1-6: HP below 0");
        s.setHP(-1);
        DebugUtil.Assert(s.getHP() == 0);
        print("Ship Test 1-7: Movement Cost Multiplier");
        s.setMoveMultiplier(1.0);
        DebugUtil.Assert(s.getMoveMultiplier() is double && s.getMoveMultiplier() == 1.0);
        print("Ship Test 1-8: Move Multiplier Bounds");
        s.setMoveMultiplier(0.0);
        DebugUtil.Assert(s.getMoveMultiplier() == 0.1);
        print("Ship Test 1 passed.");
    }

    /*
     * Control System Clarification:
     * s.addControl(index,system) should return the control system being replaced.
     * Attempting to add a control out of bounds of the control system list should result in 
     *      no change to the ship and a return of the system that was attempted to be added.
     * Attempting to access a controlsystem out of bounds should result in 
     *      no change to the ship and a return value of null.
     * Attempting to add a duplicate copy of a ControlSystem should behave as if the system 
     *      has been added out of bounds: return the control system being added and make no change.
     * Attempting to add one controlSystem to multiple ships should behave on the 2nd and later additions
     *      as if out of bounds.
     */
    void test002ControlSystems() 
    {
        print("Ship Test 2: Control Systems");
        print("Ship Test 2-1: Control System Assignment");
        Ship s = new Ship();
        s.setControlCount(1);
        ControlSystem c = new ControlSystem();
        s.addControl(0,c);
        DebugUtil.Assert(s.getControl(0) == c);
        print("Ship Test 2-2: Control System Replacement");
        ControlSystem d = new ControlSystem();
        DebugUtil.Assert(s.addControl(0, d) == c && s.getControl(0) == d);
        print("Ship Test 2-3: Control System Removal");
        DebugUtil.Assert(s.removeControl(0) == d && s.getControl(0) == null);
        print("Ship Test 2-4: Control System out-of-bounds");
        DebugUtil.Assert(s.addControl(1, d) == d && s.getControlCount() == 1);
        DebugUtil.Assert(s.addControl(-1, c) == c && s.getControlCount() == 1);
        print("Ship Test 2-5: Get System out-of-bounds");
        DebugUtil.Assert(s.getControl(-1) == null && s.getControlCount() == 1);
        DebugUtil.Assert(s.getControl(1) == null && s.getControlCount() == 1);
        print("Ship Test 2-6: Removal System out-of-bounds");
        DebugUtil.Assert(s.removeControl(-1) == null && s.getControlCount() == 1);
        DebugUtil.Assert(s.removeControl(1) == null && s.getControlCount() == 1);
        print("Ship Test 2-7: Duplicating a control system on a ship.");
        c = new ControlSystem();
        d = new ControlSystem();
        s = new Ship(); 
        s.setControlCount(2);
        Ship t = new Ship(); 
        t.setControlCount(2);
        s.addControl(0, c);
        DebugUtil.Assert(s.addControl(1, c) == c && s.getControl(1) == null);
        s.removeControl(0);
        print("Ship Test 2-8: Adding one control system to multiple ships.");
        s.addControl(0, d);
        DebugUtil.Assert(t.addControl(0, d) == d && t.getControl(0) == null);
        print("Ship Test 2 passed.");
    }

    /*
     * Utility System Clarification:
     * Utility Systems will likely only serve as an abstract parent class to weapons and shields in the future.
     * Utility System addition, access and removal will behave similarly to Control System addition access and removal.
     */
    void test003UtilitySystems()
    {
        print("Ship Test 3: Utility Systems");
        print("Ship Test 3-1: Utility System Assignment");
        Ship s = new Ship();
        s.setUtilityCount(3);
        UtilitySystem u = new UtilitySystem();
        s.addUtility(0, u);
        DebugUtil.Assert(s.getUtility(0) == u);
        print("Ship Test 3-2: Utility System Replacement");
        UtilitySystem v = new UtilitySystem();
        DebugUtil.Assert(s.addUtility(0, v) == u && s.getUtility(0) == v);
        print("Ship Test 3-3: Utility System Removal");
        DebugUtil.Assert(s.removeUtility(0) == v && s.getUtility(0) == null);
        print("Ship Test 3-4: Control System out-of-bounds");
        DebugUtil.Assert(s.addUtility(3, v) == v && s.getUtilityCount() == 3);
        DebugUtil.Assert(s.addUtility(-1, u) == u && s.getUtilityCount() == 3);
        print("Ship Test 3-5: Get System out-of-bounds");
        DebugUtil.Assert(s.getUtility(-1) == null && s.getUtilityCount() == 3);
        DebugUtil.Assert(s.getUtility(3) == null && s.getUtilityCount() == 3);
        print("Ship Test 3-6: Removal System out-of-bounds");
        DebugUtil.Assert(s.removeUtility(-1) == null && s.getUtilityCount() == 3);
        DebugUtil.Assert(s.removeUtility(3) == null && s.getUtilityCount() == 3);
        print("Ship Test 3-7: Duplicating a utility system on a ship.");
        u = new UtilitySystem();
        v = new UtilitySystem();
        s = new Ship(); s.setUtilityCount(2);
        Ship t = new Ship(); t.setUtilityCount(2);
        s.addUtility(0, u);
        DebugUtil.Assert(s.addUtility(1, u) == u && s.getUtility(1) == null);
        s.removeUtility(0);
        print("Ship Test 3-8: Adding one utility system to multiple ships.");
        s.addUtility(0, v);
        DebugUtil.Assert(t.addUtility(0, v) == v && t.getUtility(0) == null);
        print("Ship Test 3 passed.");
    }

    /*
     * Propulsion System Clarification:
     * A ship's ability to move should be the sum of its engine abilities multiplied by its movement multiplier.
     * PropulsionSystem addition, access, and removal will behave identically to as implemented above for Utility and Control.
     * Moves aggregation should be computed as sum of total moves all PropulsionSystems are capable of.
     * MoveCost aggregation should be computed as follows:
     *      weighted average of moveCost of each propulsion system based on total moves the system can contribute.
     * TurnCost aggregation should be computed similarly.
     * If all engine systems are disabled, aggregate costs should be as follows:
     * Moves: 0, MoveCost: 0.1 (Minimum possible cost for an engine), TurnCost: 0
     */
    void test004PropulsionSystems()
    {
        print("Ship Test 4-1: Propulsion System Assignment");
        Ship s = new Ship();
        s.setPropulsionCount(1);
        PropulsionSystem u = new PropulsionSystem();
        s.addPropulsion(0, u);
        DebugUtil.Assert(s.getPropulsion(0) == u);
        print("Ship Test 4-2: Propulsion System Replacement");
        PropulsionSystem v = new PropulsionSystem();
        DebugUtil.Assert(s.addPropulsion(0, v) == u && s.getPropulsion(0) == v);
        print("Ship Test 4-3: Propulsion System Removal");
        DebugUtil.Assert(s.removePropulsion(0) == v && s.getPropulsion(0) == null);
        print("Ship Test 4-4: Propulsion System out-of-bounds");
        DebugUtil.Assert(s.addPropulsion(3, v) == v && s.getPropulsionCount() == 1);
        DebugUtil.Assert(s.addPropulsion(-1, u) == u && s.getPropulsionCount() == 1);
        print("Ship Test 4-5: Get System out-of-bounds");
        DebugUtil.Assert(s.getPropulsion(-1) == null && s.getPropulsionCount() == 1);
        DebugUtil.Assert(s.getPropulsion(3) == null && s.getPropulsionCount() == 1);
        print("Ship Test 4-6: Removal System out-of-bounds");
        DebugUtil.Assert(s.removePropulsion(-1) == null && s.getPropulsionCount() == 1);
        DebugUtil.Assert(s.removePropulsion(3) == null && s.getPropulsionCount() == 1);
        print("Ship Test 4-7: Aggregating total moves");
        s.setPropulsionCount(2);
        s.addPropulsion(0,u);
        s.addPropulsion(1,v);
        u.setMoves(3);
        v.setMoves(2);
        DebugUtil.Assert(s.getMoves() == 5);
        print("Ship Test 4-8: Aggregating total moveCost");
        u.setMoveCost(1);
        v.setMoveCost(2);
        // Should equal 1.4:
        double moveAverage = (u.getMoves()*u.getMoveCost() + v.getMoves()*v.getMoveCost()) / s.getMoves();
        DebugUtil.Assert(s.getMoveCost() == moveAverage);
        print("Ship Test 4-9: Aggregating total turnCost");
        u.setTurnCost(1);
        v.setTurnCost(2);
        double turnAverage = (u.getMoves() * u.getTurnCost() + v.getMoves() * v.getTurnCost()) / s.getMoves();
        DebugUtil.Assert(s.getTurnCost() == turnAverage);
        print("Ship Test 4-10: Aggregate costs and moves with disabled systems");
        u.setStatus(false); // disable engine u.
        DebugUtil.Assert(s.getMoves() == v.getMoves() && s.getMoveCost() == v.getMoveCost() && s.getTurnCost() == v.getTurnCost());
        u.setStatus(true);
        v.setStatus(false);
        DebugUtil.Assert(s.getMoves() == u.getMoves() && s.getMoveCost() == u.getMoveCost() && s.getTurnCost() == u.getTurnCost());
        u.setStatus(false);
        DebugUtil.Assert(s.getMoves() == 0 && s.getMoveCost() == 0.1 && s.getTurnCost() == 0);
        print("Ship Test 4-11: Duplicating a propulsion system on a ship");
        u = new PropulsionSystem();
        v = new PropulsionSystem();
        s = new Ship(); s.setPropulsionCount(2);
        Ship t = new Ship(); t.setPropulsionCount(2);
        s.addPropulsion(0, u);
        DebugUtil.Assert(s.addPropulsion(1, u) == u && s.getPropulsion(1) == null);
        s.removePropulsion(0);
        print("Ship Test 4-12: Adding one propulsion system to multiple ships.");
        s.addPropulsion(0, v);
        DebugUtil.Assert(t.addPropulsion(0, v) == v && t.getPropulsion(0) == null);
        print("Ship Test 4 passed.");
    }

    void test005WeaponSystems()  
    {
    //    print("Ship Test 5: Weapon Systems");
    //    Ship s = new Ship();
    //    Ship t = new Ship();
    //    s.setMaxHP(10); s.setHP(10);
    //    t.setMaxHP(10); t.setHP(10);
    //    WeaponSystem w = new WeaponSystem();
    //    w.setDamage(1);
    //    s.setUtilityCount(3);
    //    s.addUtility(0, w);
    //    print("Ship Test 5-1: Fire at another ship");
    //    s.fire(t, true);
    //    DebugUtil.Assert(t.getHP() == 9);
    //    print("Ship Test 5-2: Miss another ship");
    //    s.fire(t, false);
    //    DebugUtil.Assert(t.getHP() == 9);
    //    print("Test 5 passed.");
    }

    /*
     * Shield Clarifications:
     *      Max and ShieldHP should be calculated as aggregate of all shields.
     *      Shield Recharge should be assessed individually to each shield.
     *      Damage to shields should be spread across multiple shields as follows:
     *          damageToShield = damageFromWeapon / totalShields
     *      When a shield HP goes to 0, damage dealt to it will bleed through to the hull ONLY IF no shields have health.
     *          If a shield becomes disabled while another shield is active, damage should go to that shield.
     */
    void test006ShieldSystems()
    {
        print("Ship Test 6: Shield Systems");
        Ship s = new Ship();
        Ship t = new Ship();
        WeaponSystem w = new WeaponSystem();
        w.setDamage(1);
        s.setUtilityCount(3);
        s.addUtility(0, w);
        ShieldSystem d = new ShieldSystem();
        d.setMaxShieldHP(1.5);
        d.setShieldHP(1.5);
        d.setRecharge(0.5);
        t.setUtilityCount(3);
        t.addUtility(0, d);
        print("Ship Test 6-1: Shield Aggregation Methods");
        DebugUtil.Assert(t.getShieldHP() == 1.5 && t.getMaxShieldHP() == 1.5 && t.getShieldRecharge() == 0.5);
        print("Ship Test 6-2: Damage Shield");
        s.fire(t, true);
        DebugUtil.Assert(t.getShieldHP() == 0.5 && t.getMaxShieldHP() == 1.5);
        print("Ship Test 6-3: Penetrate Shield");
        s.fire(t, true);
        DebugUtil.Assert(t.getShieldHP() == 0 && t.getHP() == 9.5);
        print("Ship Test 6-4: Disabled Shield");
        d.setShieldHP(1.5);
        d.setStatus(false);
        s.fire(t, true);
        DebugUtil.Assert(t.getShieldHP() == 1.5 && t.getHP() == 8.5);
        print("Ship Test 6-5: Recharge Shield");
        d.setShieldHP(0.5);
        t.repair();
        DebugUtil.Assert(t.getShieldHP() == 1);
        print("Ship Test 6-5: Multiple Shields");
        d.setMaxShieldHP(2);
        d.setShieldHP(2);
        ShieldSystem e = new ShieldSystem();
        e.setMaxShieldHP(3);
        e.setShieldHP(3);
        e.setRecharge(0.1);
        t.addUtility(1, e);
        DebugUtil.Assert(t.getShieldHP() == 5 && t.getMaxShieldHP() == 5 && t.getShieldRecharge() == (2 * 0.5 + 3 * 0.1) / 5);
        print("Ship Test 6-6: Damage Multiple Shields");
        s.fire(t, true);
        DebugUtil.Assert(t.getShieldHP() == 4 && d.getShieldHP() == 1.5 && e.getShieldHP() == 2.5);
        print("Ship Test 6-7: Penetrating Some of Multiple Shields");
        d.setShieldHP(0.1);
        e.setShieldHP(2.9);
        t.setHP(t.getMaxHP());
        s.fire(t, true);
        DebugUtil.Assert(t.getShieldHP() == 2 && d.getShieldHP() == 0 && e.getShieldHP() == 2 && t.getHP() == t.getMaxHP());
        print("Ship Test 6-8: Penetrating All Shields");
        e.setShieldHP(0.1);
        d.setShieldHP(0.1);
        s.fire(t, true);
        DebugUtil.Assert(t.getShieldHP() == 0 && d.getShieldHP() == 0 && e.getShieldHP() == 0 && t.getHP() == t.getMaxHP()-0.8);
        print("Ship Test 6-9: Recharge Multiple Shields");
        t.repair();
        DebugUtil.Assert(t.getShieldHP() == 0.6 && d.getShieldHP() == 0.5 && e.getShieldHP() == 0.1);
        print("Ship Test 6 passed.");
    }

}
