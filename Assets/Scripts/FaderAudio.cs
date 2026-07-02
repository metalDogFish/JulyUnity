using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderAudio : MonoBehaviour {

    public AudioSource musicSource;
    public float fadeInLength = .01f;
    public float volumeMax = 0.8f;

    // Use this for initialization
    void Start () {
        musicSource.clip = GetComponent<AudioSource>().clip;
        Debug.Log("music source check " + musicSource.clip);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FadeOutVolume()
    {

        while (musicSource.volume > 0)
        {
            musicSource.volume -= fadeInLength * 1;

            yield return new WaitForSeconds(0.01f);
        }
        musicSource.volume = 0;
        musicSource.Stop();
        //fadeOutFinished = true;
        Debug.Log("fadeOutFinished");
    }

    public void QuickSoundFadeOut()
    {
       
        StopAllCoroutines();//this works!

        Debug.Log("button press quick music fade out");
      
        StartCoroutine(FadeOutVolume());
    }
}
