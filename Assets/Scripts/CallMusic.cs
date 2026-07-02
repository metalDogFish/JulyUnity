using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMusic : MonoBehaviour {

	public AudioClip clip;
	private MusicManager mm;

	// Use this to call musicManager on any scene
	//place callmusic on the scene you want music
	//music randomly starts in with a fade.
	//plays any loaded/selected  music file clip

	void Start () {

        
        
        if (MusicManager.instance == null)//necessary? works without in LevelSelect
        {
            //mm = GetComponent<MusicManager> ();
            mm = FindObjectOfType<MusicManager>();

            //mm.PlayeSingle (clip);
            mm.RandomizeAudioStart(clip);
        }
        else
        {
            mm = MusicManager.instance;
            mm.RandomizeAudioStart(clip);
        }
        

	}
	

}
