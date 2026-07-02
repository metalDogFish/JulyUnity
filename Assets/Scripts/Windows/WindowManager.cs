using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour {

	public GenericWindow[] windows;
	public int currentWindowsID;
	public int defaultWindowID;

	//get reference to window in array
	public GenericWindow GetWindow(int value){
		return windows [value];
	}

	//make sure only one window is visible
	private void ToggleVisibility(int value){
		var total = windows.Length;

		for (var i = 0; i < total; i++) {
			var window = windows [i];
			if (i == value)
				window.Open ();
			else if (window.gameObject.activeSelf)
				window.Close();
		}
	}

	//open window based on ID
	public GenericWindow Open(int value){

		if (value < 0 || value >= windows.Length)
			return null;

		currentWindowsID = value;
		ToggleVisibility (currentWindowsID);

		return GetWindow (currentWindowsID);
	}

	void Start(){
		GenericWindow.manager = this;
		//if playerstatus == gameover, do
		if (PlayerStats.IsGameOver != true) {
			
			Open (defaultWindowID);
		} else {
			Open (3);
		}
	}
}
