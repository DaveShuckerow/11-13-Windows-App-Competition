using UnityEngine;
using System.Collections.Generic;

public class ShipController : MonoBehaviour
{
    public GameboardController board;
    public HexController hex;

    public Ship myShip;

    private int animType = 0;
    private double animPos = 0;
    private List<ShipControllerLocation> motion;
    private int motionIndex = 0;
    private double animSpeed = 0.5;
    public bool moved = false;
    // Tell the shipcontroller to animate a motion to a position.
    public void move(List<ShipLocation> positions)
    {
        motion = new List<ShipControllerLocation>();
        animType = 1;
        animPos = 0.0;
        foreach (ShipLocation l in positions)
        {
            motion.Add(new ShipControllerLocation(l, board));
        }
        print(positions.Count);
        moved = true;
        if (motion[0].direction == motion[1].direction)
            animType = 2;
        moved = true;
    }
    public void resetMove()
    {
        moved = false;
    }

    // Tell the shipcontroller to fire at another ShipController.
    // At some other point we will add firing animations based on the bool[] given by Ship.fire(target)
    public void fire(ShipController target)
    {
        myShip.fire(target.myShip);
    }

    void Update()
    {
        /* Animation types:
         * 1: turning.  Motion array will contain destinations.
         * 2: moving.
         * 3: At destination.  Broadcast our arrival.s
         * */
        switch (animType)
        {
            case 0:
                motion = null;
                animType = -1;
                animPos = 0.0;
                break;
            case 1:
                if (animPos >= 1)
                {
                    animPos = 0.0;
                    motionIndex += 1;
                    transform.LookAt(motion[motionIndex].position.transform);
                    if (motionIndex >= motion.Count - 1)
                        animType = 3;
                    else if (motion[motionIndex].direction != motion[motionIndex + 1].direction)
                        animType = 1;
                    else
                        animType = 2;
                }
                else
                {
                    //transform.Rotate(Vector3.up,Vector3.Angle(motion[motionIndex].direction, motion[motionIndex+1].direction)*Time.deltaTime*(float)animSpeed, Space.Self);
                    //float angle = Vector3.Angle(motion[motionIndex].direction,motion[motionIndex+1].direction);
                    transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Slerp(motion[motionIndex].direction, motion[motionIndex + 1].direction, (float)animPos));
                    animPos += Time.deltaTime * animSpeed;
                    break;
                }
                break;
            case 2:
                if (animPos >= 1)
                {
                    animPos = 0.0;
                    motionIndex += 1;
                    transform.position = motion[motionIndex].position.transform.position;
                    if (motionIndex >= motion.Count - 1)
                        animType = 3;
                    else if (motion[motionIndex].position != motion[motionIndex + 1].position)
                        animType = 2;
                    else
                        animType = 1;
                }
                else
                {
                    transform.position = Vector3.Lerp(motion[motionIndex].position.transform.position, motion[motionIndex + 1].position.transform.position, (float)animPos);
                    animPos += Time.deltaTime * animSpeed;
                    break;
                }
                break;
            case 3:
                animType = 0;
                break;
        }
    }
}

public class ShipControllerLocation
{
    public readonly HexController position;
    public readonly Vector3 direction;

    public ShipControllerLocation(ShipLocation l, GameboardController g)
    {
        position = g.findHexController(l.position);
        direction = HexController.hexDirToVector(l.direction);
    }

    public bool Equals(ShipControllerLocation other)
    {
        return position == other.position && direction == other.direction;
    }
}