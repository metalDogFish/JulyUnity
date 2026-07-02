using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeCanvas : MonoBehaviour {

	public Text name;
	// on start, display players name
	void Start () {
		name.text = PlayerStats.PlayerName;
	}
	

}
