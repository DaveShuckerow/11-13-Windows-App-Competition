using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float multiplier = 1;
    public int minZoom = 100;
    public int maxZoom = 5;
    public float zoomAmount = 5;
    private float zoomLevel = 1;
    private float startZoomLevel = 1;
    private Vector3 prevMousePosition;

	// Use this for initialization
	void Start () {
        zoomLevel = Camera.main.orthographicSize;
        startZoomLevel = zoomLevel;
        prevMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
	    /* move with mouse */
        if (Input.touchCount == 1)
        {
            Vector3 newPos = new Vector3(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
            newPos = newPos * Time.deltaTime * multiplier * zoomLevel / startZoomLevel;
            transform.position += Vector3.forward * newPos.y;
            transform.position += Vector3.right * newPos.x;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 newPos = (prevMousePosition - Input.mousePosition)*Time.deltaTime*multiplier*zoomLevel/startZoomLevel;
            transform.position += Vector3.forward*newPos.y;
            transform.position += Vector3.right * newPos.x;
        }
        prevMousePosition = Input.mousePosition;
        /* Zoom with mousewheel */
        zoomLevel = Mathf.Min(Mathf.Max(zoomLevel-Input.GetAxis("Mouse ScrollWheel")*zoomAmount, maxZoom), minZoom);
        Camera.main.orthographicSize = zoomLevel;
	}
}
