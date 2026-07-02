using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyWindow : GenericWindow {

	public ToggleGroup difficultyGroup;

	public float inputDelay;

	private float delay;

	public int difficulty{
		get{

			return 0;
		}
		set{

			value = (int)Mathf.Repeat (value, difficultyGroup.transform.childCount);

			//var currentSelection = difficultyGroup.ActiveToggles ().FirstOrDefault ();

			//if (currentSelection != null) {
			//	currentSelection.isOn = false;
			//}

			Debug.Log ("dificulty " + value);
		}
	}

	public void OnSelect(){

		//OnNextWindow ();
		manager.Open (2);
		//
		//SceneManager.LoadScene ("SplashScreen");
		PlayerPrefs.SetInt ("Difficulty", difficulty);
	}

	void Update(){
		
		delay += Time.deltaTime;

		if (delay > inputDelay) {

			var newDifficulty = difficulty;
			var hDir = Input.GetAxis ("Horizontal");

			if (hDir > 0) {
				newDifficulty++;
			} else if (hDir < 0) {
				newDifficulty--;
			}
			if (newDifficulty != difficulty) {
				difficulty = newDifficulty;
			}
			delay = 0;
		}

		if (Input.GetButtonDown ("Submit")) {
			OnSelect ();
		}
	}

}
