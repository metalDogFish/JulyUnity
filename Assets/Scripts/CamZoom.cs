using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour {

    public float camIncrement = 0.5f;
    private int camSize = 60;
    private Camera cam;


    private void Awake()
    {
        //cam = FindObjectOfType<Camera>();
        cam = GetComponent<Camera>();
    }
    // Use this for initialization
    void Start () {

        if (cam == null)
        {
            Debug.Log("camera missing");
        }

        StartCoroutine(ZoomIn());
	}
	
    IEnumerator ZoomIn()
    {
        while (cam.orthographicSize > camSize)
        {
            cam.orthographicSize -= camIncrement;
            yield return new WaitForEndOfFrame();
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
