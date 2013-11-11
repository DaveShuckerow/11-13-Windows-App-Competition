using UnityEngine;
using System.Collections;

public class HexController : MonoBehaviour {
    [SerializeField]
    Material[] Materials;

    Hex myHex;
    Hex upHex, ulHex, dlHex, dnHex, drHex, urHex;
    bool initialized = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setHex(Hex h)
    {
        myHex = h;
        upHex = h.getUp();
        ulHex = h.getUl();
        dlHex = h.getDl();
        dnHex = h.getDn();
        drHex = h.getDr();
        urHex = h.getUr();
    }
}
