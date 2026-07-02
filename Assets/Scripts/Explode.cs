using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

	public Debris debris;
	public int totalDebris = 10;
	public AudioClip explodeSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Deadly") {
			OnExplode ();
            //spikes
            // Debug.Log(target + " target collider");
            this.gameObject.GetComponent<Player>().AwardDeath("Spikes");
        }
		//if (target.gameObject.tag == "Checkpoint" && target.gameObject.GetComponent<RadioWaves>().isOn) {//nope
        if(target.gameObject.tag == "Checkpoint") { 
            if(target.gameObject.GetComponentInChildren<RadioWaves>().isOn == true)
			    CheckpointReached();

		}
	}
	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Deadly") {
			OnExplode ();
            //alien
            this.gameObject.GetComponent<Player>().AwardDeath("Alien");
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Acid")
        {
            //deduct health till explode
            this.gameObject.GetComponent<Player>().BreathAir(1f, "acid");
            //show flashing color when losing health
            Debug.Log("AAAACID");
         }
    }

    public void OnExplode(){
	
		AudioSource.PlayClipAtPoint(explodeSound, transform.position);

		var t = transform;

		for (int i = 0; i < totalDebris; i++) {
			t.TransformPoint (0, -100, 0);
			var clone = Instantiate (debris, t.position, Quaternion.identity) as Debris;
			var body2d = clone.GetComponent<Rigidbody2D> ();
			body2d.AddForce (Vector3.right * Random.Range (-1000, 1000));
			body2d.AddForce (Vector3.up * Random.Range (500, 2000));
		}

		// this.gameObject.GetComponent<Player> ().AwardDeath ("Explosion");

		Debug.Log("boom");
		Destroy (gameObject);

	}
	void CheckpointReached(){
		
		FindObjectOfType<Player> ().AwardWin ();
	}
}
