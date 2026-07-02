using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyboardWindow : GenericWindow {

	public Text inputField;
	public int maxCharacters = 7;

	private float delay = 0;
	private float curserDelay = .5f;
	private bool blink;
	private string _text = "";
	
	
	// Update is called once per frame
	void Update () {
		var text = _text;

		if (_text.Length < maxCharacters) {
			text += "_";

			if (blink) {
				text = text.Remove (text.Length - 1);
			}
		}

		inputField.text = text;

		delay += Time.deltaTime;
		if (delay > curserDelay) {
			delay = 0;
			blink =! blink;
		}
	}

	public void OnKeyPress(string key){
		if (_text.Length < maxCharacters) {
			_text += key;
		}
	}

	public void OnDelete(){
		if (_text.Length > 0) {
			_text = _text.Remove (_text.Length - 1);
		}
	}

	public void OnAccept(){
		//save name
		PlayerStats.PlayerName = _text;
		PlayerPrefs.SetString ("PlayerName", _text);
		//manager.Open (0);
		SceneManager.LoadScene ("TopScene");
	}

	public void OnCancel(){
		manager.Open (0);
	}
}
