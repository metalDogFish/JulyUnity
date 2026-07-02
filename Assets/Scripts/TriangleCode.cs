using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this code is not nessesary!
//triangle is labelled Checkpoint,
//Player checks collisions under explode.cs

public class TriangleCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			//award points, then destroy
			target.gameObject.GetComponent<Player>().AwardWin();
			//if (pickupSound)
				//AudioSource.PlayClipAtPoint (pickupSound, transform.position);
			//Destroy (gameObject);
		}
	}

}
