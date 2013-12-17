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
 * WeaponSystem.hitProbability(Ship target): Get probability of a hit against target.
 * WeaponSystem.computeHit(Ship target): Flip a coin weighted on hitProbability and see if it hits.
 * WeaponSystem.fire(Ship target): Use computeHit to determine whether or not a shot hits.
 *      Create BeamSystem and ProjectileSystem subclasses of WeaponSystem.
 *      
 * Make sure ships cannot stop in an unreachable hex, but that they can move through it...
 * ShipController.simulateMove(string path): Simulate your ship following the given path and 
 *      see what Hex it would end up in.  Make sure the Ship remains in the same location as before the call.
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
