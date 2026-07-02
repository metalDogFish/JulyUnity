using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public AudioClip pickupSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
				//award points, then destroy
			target.gameObject.GetComponent<Player>().AwardPoints();
			if (pickupSound)
				AudioSource.PlayClipAtPoint (pickupSound, transform.position);
			Destroy (gameObject);
		}
	}
}
