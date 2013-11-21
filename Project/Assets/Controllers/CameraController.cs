using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float multiplier = 1;
    public int minZoom = 100;
    public int maxZoom = 10;
    public float zoomAmount = 5;
    private Vector3 prevMousePosition;

	// Use this for initialization
	void Start () {
        prevMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
	    /* move with mouse */
        if (Input.GetMouseButton(0))
        {
            Vector3 newPos = (prevMousePosition - Input.mousePosition)*Time.deltaTime*multiplier;
            transform.position += Vector3.forward*newPos.y;
            transform.position += Vector3.right * newPos.x;
        }
        prevMousePosition = Input.mousePosition;
        /* Zoom with mousewheel */
        Camera.main.orthographicSize = Mathf.Min(Mathf.Max(Camera.main.orthographicSize-Input.GetAxis("Mouse ScrollWheel")*zoomAmount, maxZoom), minZoom);
	}
}
