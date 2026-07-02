using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChoices : MonoBehaviour {

	private string sceneName;
	private Player player;

	// Use this for initialization
	void Start () {
		sceneName = SceneManager.GetActiveScene ().name;	
		//player = FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetGame(){
		Debug.Log ("resetting game");
		SceneManager.LoadScene (sceneName);
	}

	public void BackToIntro(){
		Debug.Log ("return to intro");
		SceneManager.LoadScene ("GameWindow");
	}

	public void ChangeScene(){
		if (sceneName == "SplashScreen") {

            //fade out the wind during transition
            FindObjectOfType<FaderAudio>().QuickSoundFadeOut();

            //start change scene process
            FindObjectOfType<LevelLoader>().LoadTopSideScene();
            //the above code replaces bottom code by adding a LevelLoader script that tweens between scenes.
			//SceneManager.LoadScene ("TopScene");
		}
		//Debug.Log("playerLevel" +player.playerLevel);
	}

	public void GameOverScene(){
		//PlayerStats.IsGameOver (true);
		PlayerStats.IsGameOver = true;
		//SceneManager.LoadScene ("GameWindow");
		//SceneManager.LoadScene("TopScene");
        FindObjectOfType<LevelLoader>().LoadTopSideScene();
        //manager.Open (3);
    }
}
