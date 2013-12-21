using UnityEngine;
using System.Collections;

public class LaserEffectBehavior : WeaponEffectBehavior {
    private bool effect = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (start != null && end != null)
        {
            Vector3 endPos = end.position;
            if (!doHit)
                endPos = 100*(end.position-start.position);
            LineRenderer ln = GetComponent<LineRenderer>();
            ln.SetPosition(0, start.position + Vector3.up);
            ln.SetPosition(1, endPos + Vector3.up);
            Color color = renderer.material.color;
            color.a = Mathf.Min(Mathf.Sin((float)(myLife / lifetime * Mathf.PI))*(float)lifetime/2,1);
            renderer.material.color = color;
            Vector2 offset = renderer.material.mainTextureOffset;
            offset.x -= 5*Time.deltaTime;
            renderer.material.mainTextureOffset = offset;
            Vector2 scale = renderer.material.mainTextureScale;
            scale.x = Vector3.Distance(start.position, endPos)/10;
            renderer.material.mainTextureScale = scale;
        }
        myLife += Time.deltaTime;
        if (myLife > lifetime / 2 && !effect && doHit)
        {
            effect = true;
            //if (end.parent.GetComponent<ShipController>().myShip.getShieldHP() > 0)
                Instantiate(Resources.Load("ShieldEffect"), end.position + Vector3.up / 2, Quaternion.identity);
        }
        if (myLife >= lifetime)
            Destroy(gameObject);
        
	}
}
