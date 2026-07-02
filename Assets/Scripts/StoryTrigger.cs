using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour {

	public StoryManager storyManager;
	public int secondsToWait;
    public string triggerFunc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D target){
        if (target.gameObject.tag == "Player")
        {
            if (triggerFunc == "gamePauseTimer")
            {
                //storyManager.isPaused = true;
                storyManager.StartCoroutine("GamePauseTimer", secondsToWait);
            }
            else if(triggerFunc == "pauseAndSwitchScene")
            {

                storyManager.PauseAndSwitchScene();

            }
            else if (triggerFunc == "firstContactScene")
            {

                storyManager.FirstContactScene( secondsToWait);

            }
            else if(triggerFunc == "hiddenCaveScene")
            {
                storyManager.HiddenCaveScene(secondsToWait);
            }
            //if (pickupSound)
            //	AudioSource.PlayClipAtPoint (pickupSound, transform.position);
            Destroy (gameObject);
		}
	}
}
