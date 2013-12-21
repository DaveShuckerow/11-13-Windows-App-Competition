/*
 * README:
 * Planning:
 * 
 * Backend Elements:
 * Hex.getHexDistance(Hex other): Get distance between hexes to allow for weapon use
 *      As well as other developments later on such as gravity and nebula effects.
 * Hex.isPassable(): If it is possible to pass through a given hex.  Any hex containing an impassable
 *      obstacle should be impassable.
 * Hex.isReachable(): If it is possible to get to a given hex.  Any hex containing a unit or obstacle
 *      Should not be reachable.
 * 
 * Subsystem.setLevel/getLevel(): Set an integer defining how advanced a system is.
 * Subsystem.maxLevel: An integer defining the maximum level a system can have.
 * 
 * WeaponSystem.hitProbability(Ship target): Get probability of a hit against target.
 * WeaponSystem.computeHit(Ship target): Flip a coin weighted on hitProbability and see if it hits.
 * WeaponSystem.fire(Ship target): Use computeHit to determine whether or not a shot hits.
 *      Create LaserSystem and TorpedoSystem subclasses of WeaponSystem.
 *      
 * Ship.destroy(): Called when a ship has been destroyed.
 * 
 * LaserSystem.hitProbability:
 *      90% at 1 hex
 *      70% at 2 hex
 *      50% at 3 hex
 *      0% beyond 3 hex
 * TorpedoSystem.hitProbability:
 *      70% at 3 hex or less
 *      0% beyond 3 hex
 *      
 * Make sure ships cannot stop in an unreachable hex, but that they can move through it...
 * Ship.simulateMove(string path): Simulate your ship following the given path and 
 *      see what Hex it would end up in.  Make sure the Ship remains in the same location as before the call.
 * If a Ship's path ends in an unReachable Hex, stop one Hex before.
 * 
 * Team: A class representing a team of ships.
 * Team.add(Ship) adds a Ship to the list.  Replaces a Ship's current team if Ship has one.
 * Team.contains(Ship) checks if a Ship is part of itself.
 * Ship.getTeam() returns the team a ship has been added to.
 * TODO: Team.setAI(AI) tells the team how to run AI. -- TODO!!!
 * 
 * GameController: A class representing a graphical game.
 * Is responsible for managing turns and victory conditions.
 * 
 * Graphical elements:
 * construct layouts for...
 *      Move instruction
 *      Fire instruction
 *      Pause menu
 *      Unit Status menu
 *      Unit customization menus
 *      
 * Icons for different systems?
 *      
 * effects for weapon use:
 *      
 * Organization into teams.
 * 
 * AI
 * 
 * Developing obstacles.
 */

public class README {
}
