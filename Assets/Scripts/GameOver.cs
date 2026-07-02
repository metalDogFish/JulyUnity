using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public UnityEngine.UI.Text scoreText;
	public GameObject screenParent;
	// Use this for initialization
	void Start () {
		screenParent.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowLose(){
		screenParent.SetActive (true);
		//scoreText
		scoreText.text = "You Lose!";

		Animator animator = GetComponent<Animator>();

		if (animator) {
			animator.Play ("GameOverShow");

		}
	}

	public void ShowWin(int score){
		screenParent.SetActive (true);
		scoreText.text = " You Win!! " + "\n Level Cleared.";

		Animator animator = GetComponent<Animator>();

		if (animator) {
			animator.Play ("GameOverShow");

		}
	}
}
