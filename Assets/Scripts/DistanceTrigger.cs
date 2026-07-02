using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour {

	//takes a gameobject and triggers nested function when close
	public GameObject gObject;

	public float distanceTrig;

	private SMask mask;

	// Use this for initialization
	void Start () {
		//smask is attached to camera
		mask = FindObjectOfType<SMask> ();
	}
	
	// Update is called once per frame
	void Update () {
		//find distance between gObject and this
		if (gObject) {
			var distance = Vector3.Distance (gObject.transform.position, transform.position);
			if (distance < distanceTrig) {
				mask.enabled = true;
				//print ("distance to other: " + distance);
			} else {
				mask.enabled = false;
			}
		}
		
	}
}
