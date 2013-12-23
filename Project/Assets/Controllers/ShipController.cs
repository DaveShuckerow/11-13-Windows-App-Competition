using UnityEngine;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {
    public GameboardController board;
    public HexController hex;

    public Ship myShip;

    private int animType = 0;
    private double animPos = 0;
    private List<ShipControllerLocation> motion;
    private int motionIndex = 0;
    public double animSpeed = 0.75;
    public int controlSystems, propulsionSystems, utilitySystems, shieldRadius;

    // Tell the shipcontroller to animate a motion to a position.
    public void move(string path)
    {
        makeMove(myShip.followPath(path));
    }
    
    private void makeMove(List<ShipLocation> positions)
    {
        motion = new List<ShipControllerLocation>();
        animType = 1;
        animPos = 0.0;
        motionIndex = 0;
        foreach (ShipLocation l in positions)
        {
            motion.Add(new ShipControllerLocation(l, board));
        }
        if (motion.Count < 2)
        {
            animType = 3;
            return;
        }

        if (motion[0].direction == motion[1].direction)
            animType = 2;
    }

    // Tell the shipcontroller to fire at another ShipController.
    public void fire(ShipController target)
    {
        bool[] hits = myShip.fire(target.myShip);
        Debug.Log(target.myShip.getHP());
        UtilityMarker[] ts = gameObject.GetComponentsInChildren<UtilityMarker>();
        Transform[] targetSystems = target.gameObject.GetComponentsInChildren<Transform>();
        System.Array.Sort<UtilityMarker>(ts, SystemNameComparer.compareGameObjectNames);
        int counter = 0;
        for (int i = 0; i < myShip.getUtilityCount(); i++)
        {
            if (myShip.getUtility(i) is WeaponSystem)
            {
                // There is firing to do.
                bool doHit = hits[counter];
                GameObject effect;
                WeaponEffectBehavior behavior;
                if (myShip.getUtility(i) is LaserSystem)
                    effect = (GameObject)Instantiate(Resources.Load<GameObject>("LaserEffect"));
                else
                    effect = (GameObject)Instantiate(Resources.Load<GameObject>("TorpEffect"));
                behavior = effect.GetComponent<WeaponEffectBehavior>();
                effect.transform.position = transform.position + Vector3.down;
                behavior.setup(ts[i].transform, targetSystems[Random.Range(0, targetSystems.Length)], doHit, target.myShip.getShieldHP() > 0);
                counter += 1;
            }
        }
    }

    public bool isDoneMoving()
    {
        return animType <= 0;
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
                motionIndex = 0;
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
                    transform.rotation = Quaternion.FromToRotation(-Vector3.forward, Vector3.Slerp(motion[motionIndex].direction, motion[motionIndex + 1].direction, (float)animPos));
                    animPos += Time.deltaTime*animSpeed;
                    break;
                }
                break;
            case 2:
                if (animPos >= 1)
                {
                    animPos = 0.0;
                    motionIndex += 1;
                    transform.position = motion[motionIndex].position.transform.position;
                    if (motionIndex >= motion.Count-1)
                        animType = 3;
                    else if (motion[motionIndex].position != motion[motionIndex + 1].position)
                        animType = 2;
                    else
                        animType = 1;
                }
                else
                {
                    transform.position = Vector3.Lerp(motion[motionIndex].position.transform.position, motion[motionIndex+1].position.transform.position, (float)animPos);
                    animPos += Time.deltaTime*animSpeed;
                    break;
                }
                break;
            case 3:
                animType = 0;
                hex = board.findHexController(myShip.getPosition());
                motionIndex = 0;
                print(myShip.getDirection());
                break;
        }

        if (myShip.getHP() <= 0 && !GetComponent<DeathTimer>())
        {
            die();
        }
    }

    void die() 
    {
        myShip.destroy();
        gameObject.AddComponent<DeathTimer>();
        GetComponent<DeathTimer>().lifetime = 7;
        GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("BigExplosion"));
        explosion.transform.position = transform.position + Vector3.up;
    }

    void OnDestroy()
    {
        board.onShipDestroyed(this);
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