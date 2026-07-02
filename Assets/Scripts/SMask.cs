using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMask : MonoBehaviour {

	[Range(0.05f,0.2f)]
	public float flickTime;

	[Range(0.02f,0.09f)]
	public float addSize;

	private bool killFlicker = false;
	float timer = 0;
	private bool bigger = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!killFlicker) {
			timer += Time.deltaTime;

			if (timer > flickTime) {
				if (bigger) {
					transform.localScale = new Vector3 (transform.localScale.x + addSize, transform.localScale.y + addSize, transform.localScale.z);
				} else {
					transform.localScale = new Vector3 (transform.localScale.x - addSize, transform.localScale.y - addSize, transform.localScale.z);
				}

				timer = 0;
				bigger = !bigger;
			}
		}
	}

	public void SwitchFlicker(){
		Debug.Log ("switching flicker");
		killFlicker = !killFlicker;
	}
}
//notes using for tile mask
//camera needs smask_player attached to it.
//tiles need empty 2 empty tilemaps attached to it.
//smask_player has script attached which sets empty tilemaps to background size
//sorting order must match!
//-------------!carefull using different tiles sets on map----------
//masks will work correctly if bg tile size is same as foreground tile size!

