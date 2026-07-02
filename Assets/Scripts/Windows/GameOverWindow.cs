using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow :GenericWindow {

	public Text leftStatsLabel;
	public Text leftStatsValue;
	public Text rightStatsLabel;
	public Text rightStatsValue;
	public Text scoreValue;
	public float statsDelay = 1f;
	public int totalStats = 6;
	public int statsPerColumn = 3;

	private int currentStat = 0;
	private float delay = 0;

	private void UpdateStatText(Text label, Text value){

		label.text += "Stat " + currentStat + "  ";
		value.text += Random.Range (0, 1000).ToString ("D4") + "  ";
	}

	private void ShowNextStat(){

		if (currentStat > totalStats - 1) {
			scoreValue.text = Random.Range (0, 1000000000).ToString ("D10");
			currentStat = -1;
			return;
		}

		if (currentStat < statsPerColumn) {

			UpdateStatText (leftStatsLabel, leftStatsValue);
		} else {
			UpdateStatText (rightStatsLabel, rightStatsValue);
		}

		currentStat++;
	}

	void Update(){

		delay += Time.deltaTime;

		if (delay > statsDelay && currentStat != -1) {

			ShowNextStat ();
			delay = 0;
		}
	}

	public void ClearText(){
		leftStatsLabel.text = "";
		leftStatsValue.text = "";
		rightStatsLabel.text = "";
		rightStatsValue.text = "";
		scoreValue.text = "";
	}

	public override void Open(){

		ClearText ();
		base.Open ();
	}

	public override void Close(){
		base.Close ();
		currentStat = 0;
	}

	public void OnExit(){
		//manager.Open (3);
		Debug.Log("exit game");
		Application.Quit ();
	}

	public void OnReplay(){
		PlayerStats.IsGameOver = false;
		manager.Open (0);
	}

}
