using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public float scale = 8f;

	private Transform t;

	void Awake(){
		var cam = GetComponent<Camera> ();
        //not sure if code is having any effect-when in android, screen size is way smaller than editor, why?
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);

        //cam.orthographicSize = (Screen.height / 2f / scale);
        //cam.orthographicSize = (Screen.height / scale);


        Debug.Log(Screen.height + " screen hieght");
	}

	// Use this for initialization
	void Start () {
		t = target.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
            transform.position = new Vector3 ((t.position.x),t.position.y, transform.position.z);
            //try to reduce tilemap tearing by rounding to int..//creates a shaky cam effect
            //var xfactor = (int)t.position.x;
            //var yfactor = (int)t.position.y;
            //transform.position = new Vector3(xfactor, yfactor, transform.position.z);
        }
	}
}
//problem-lines will appear between tiles when camera is moving
//fixes-turn off anti aliasing, and uncheck allow msaa on camera, or (add default material on background tiles)-changes color
//set cell gap on tiles to (-.002) 