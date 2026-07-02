using System.Collections;
using UnityEngine;

//ideal for background music in option scene(music fades in at random spot on track)
public class MusicManager : MonoBehaviour {

	public AudioSource musicSource;
	public float fadeInLength = .01f;
    public float volumeMax = 0.8f;
    //public bool fadeOutFinished = false;

	public static MusicManager instance = null; //allow other scripts to call functions

	// Use this for initialization
	void Awake () {
		//check if instance already exists
		if (instance == null) {
			instance = this;
		}//if instance already exists 
		else if (instance != this) {
			//destroy this to enfore our Singleton pattern
			Destroy (gameObject);
		}

		//prevents manager from  being destroyed everytime reload screen
		DontDestroyOnLoad (gameObject);
	}

    private void Start()
    {
        musicSource.clip = GetComponent<AudioSource>().clip;
        Debug.Log("music source check " + musicSource.clip);
    }


    //simple basic test function
    public void PlaySingle(AudioClip _clip){
        //fadeOutFinished = false;
        musicSource.clip = _clip;
		musicSource.Play ();
	}

	//starts at a random place between start and end of track
	public void RandomizeAudioStart(AudioClip _clip){
        //assings clip to musicSource
		musicSource.clip = _clip;
        //find a point somewhere in first half of track
		float randomIndex = Random.Range (0, _clip.length * .5f);
        //musicSource.
        //musicSource.time = randomIndex;
        //GetComponent<AudioSource>().time = GetComponent<AudioSource>().clip.length * .5f;
        GetComponent<AudioSource>().time = randomIndex;

		musicSource.Stop();//prevent multiple tracks
       // fadeOutFinished = false;

        StopCoroutine(FadeInVolume());
		//musicSource.Play ();//add fadein ability
		StartCoroutine( FadeInVolume());

        Debug.Log("play randomize " + randomIndex);
    }

	public IEnumerator FadeInVolume(){     

        musicSource.volume = 0.0f;
		//float tempVolume;
		musicSource.Play ();
		while (musicSource.volume < volumeMax) {
			musicSource.volume += fadeInLength * 1;

			Debug.Log("Fading In: " + musicSource.volume.ToString());

			yield return new WaitForSeconds (0.01f);
			//tempVolume = musicSource.volume;
		}
		//just a precaution
		musicSource.volume = volumeMax;
	}

    public IEnumerator FadeOutVolume()
    {

        while(musicSource.volume > 0)
        {
            musicSource.volume -= fadeInLength*2;

            yield return new WaitForSeconds(0.01f);
        }
        musicSource.volume = 0;
        musicSource.Stop();
        //fadeOutFinished = true;
        Debug.Log("fadeOutFinished");
    }

    public void QuickMusicFadeOut()
    {
        // StopCoroutine(FadeInVolume());//not this-wierd
        StopAllCoroutines();//this works!

        Debug.Log("button press quick music fade out");
        //without the above line, user can stall the coroutine by clicking too fast.(before Ienumertor function timer expires)
        //upon scene re-entry, music wont start again.
        StartCoroutine(FadeOutVolume());
    }
}
