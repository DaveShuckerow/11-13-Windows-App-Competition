// Animation Behavior handler written for a project in game design last spring.
using UnityEngine;
using System.Collections;

public class AnimationBehavior : MonoBehaviour {

    public Material[] frames;
    public float rate = 1.0f;
    public bool repeat = false;
    private float myTime = 0.0f;
    private int myFrame = 0;

    void Start () {
 
    }
 
    void Update () {
        myTime += Time.deltaTime*rate;
        if (myTime >= 1 && rate > 0){
            myTime = 0;
            myFrame += 1;
        }
         
        if (myFrame >= frames.Length) {
            if (repeat) myFrame = 0;
            else
            {
                myFrame = frames.Length - 1;
                Destroy(gameObject);
            }
        }
         
        // Give the desired frame to our mesh renderer.
        MeshRenderer rend = gameObject.GetComponent<MeshRenderer>();
        rend.material = frames[myFrame];
    }
 
    void ResetAnim() {
        myTime = 0; myFrame = 0;
    }
 
    void Overwrite(AnimationBehavior other) {
        if (other.frames != frames) myFrame = 0;
            frames = other.frames;
        rate   = other.rate;
        repeat = other.repeat;
        // Change the animation NOW.
        if (myFrame >= frames.Length) {
            if (repeat) myFrame = 0;
            else myFrame = frames.Length - 1;
        }
        MeshRenderer rend = gameObject.GetComponent<MeshRenderer>();
        rend.material = other.frames[myFrame];
    }

}
