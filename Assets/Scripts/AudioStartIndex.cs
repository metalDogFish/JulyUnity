using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStartIndex : MonoBehaviour {

    public AudioSource musicSource;
    public float timeFromStart = 0;
    // Use this for initialization
    void Start () {
        musicSource.clip = GetComponent<AudioSource>().clip;
        //allows us to start the track from diffrent point, (to help time it better)
        StartAudioAtTime(timeFromStart);

        Debug.Log("music source check " + musicSource.clip);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartAudioAtTime(float start)
    {
        musicSource.time = start;
        musicSource.Play();
    }
}
