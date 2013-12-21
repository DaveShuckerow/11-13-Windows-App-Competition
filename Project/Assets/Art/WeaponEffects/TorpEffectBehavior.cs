using UnityEngine;
using System.Collections;

public class TorpEffectBehavior : WeaponEffectBehavior {

    // Update is called once per frame
    void Update()
    {
        if (start != null && end != null)
        {
            Vector3 endPos = end.position;
            if (!doHit)
                endPos = (end.position - start.position) * 10;
            transform.position = Vector3.Lerp(start.position + Vector3.up, endPos + Vector3.up, (float)(myLife / lifetime));
        }
        myLife += Time.deltaTime;
        if (!doHit)
            myLife -= 9* Time.deltaTime / 10;
        if (myLife >= lifetime)
        {
            if (doHit)
                Instantiate(Resources.Load("ShieldEffect"), end.position + Vector3.up / 2, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
