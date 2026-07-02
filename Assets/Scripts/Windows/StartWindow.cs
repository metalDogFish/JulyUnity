using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartWindow :GenericWindow {

	public Button continueButton;

	public override void Open(){

		var canContinue = true;

		continueButton.gameObject.SetActive (canContinue);

		if (continueButton.gameObject.activeSelf) {
			firstSelected = continueButton.gameObject;
		}

		base.Open ();
	}

	public void NewGame(){
		Debug.Log ("New Game Pressed");
		manager.Open (1);
		//erase playerPrefs
		PlayerPrefs.DeleteAll();
	}

	public void Continue(){
		Debug.Log ("Continue pressed");
		SceneManager.LoadScene ("TopScene");
	}

	public void Options(){
		Debug.Log ("Options pressed");
	}

}
