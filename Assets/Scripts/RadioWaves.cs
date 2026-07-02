using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioWaves : MonoBehaviour {
	//draw ever exanding radio waves from source point

	public ParticleSystem particleS;
    public bool isOn;
	// Use this for initialization
	void Start () {
		particleS.Stop ();
        isOn = false;
		Debug.Log ("particles present");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartBeacon(){
		Debug.Log ("start beacon");
		particleS.Play ();
        isOn = true;
	}

	public void StopBeacon(){
		Debug.Log ("stop beacon");
		particleS.Stop ();
        isOn = false;
	}
	//no effect?
	/*
	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			//award points, then destroy
			target.gameObject.GetComponent<Player>().AwardWin();
			//if (pickupSound)
				//AudioSource.PlayClipAtPoint (pickupSound, transform.position);
			//Destroy (gameObject);
		}
	}
	*/
}
