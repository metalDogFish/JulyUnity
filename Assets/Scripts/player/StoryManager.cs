using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * pause game at key spots when player gets near enough. display story dialogue, resume after a few secs.
 * triggered by objects with storyTrigger attached
 * */
public class StoryManager : MonoBehaviour {

	public UnityEngine.UI.Text storyText;
	public bool isPaused;
	public SMask smask;
	private Player player;
	// Use this for initialization
	void Start () {
		//to stop movement on player
		player = GameObject.FindObjectOfType<Player> ();
		//to stop movement on smask
		smask = GameObject.FindObjectOfType<SMask> ();
		isPaused = false;
		storyText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

	void PauseGame(){
		player.isPaused = true;
		smask.enabled = false;
        //set this in the calling function
		//storyText.text = "You've discovered a hidden cave!"+"\n"+"it looks like a whole new ecosystem down here";
		isPaused = true;
	}

    void ResumeGame()
    {
        player.isPaused = false;
        smask.enabled = true;
        storyText.text = "";
        isPaused = false;
    }


    public void FirstContactScene(int numToWait)
    {
        //player.isPaused = true;
        //smask.enabled = false;
        storyText.text = "Attention, our instruments have detected movement below!" + "\n" + "Be careful, avoid any dangers..";
       // isPaused = true;

        StartCoroutine("GamePauseTimer",numToWait);
    }

    public void HiddenCaveScene(int numToWait)
    {
        storyText.text = "You've discovered a hidden cave!"+"\n"+"it looks like a whole new ecosystem down here";

        StartCoroutine("GamePauseTimer", numToWait);
    }


    public void PauseAndSwitchScene()
    {
        player.isPaused = true;
        smask.enabled = false;
        storyText.text = "The ground has given way!" + "\n" + "lookout below!";
        isPaused = true;
        //stop camera follow behavior

        //switch scene
        StartCoroutine("GameSceneChanger");
    }

	 IEnumerator GamePauseTimer(int numToWait){
		//do first
		PauseGame ();
		//wait
		yield return new WaitForSeconds (numToWait);
		print ("coroutine is finished");
		//do second
		ResumeGame ();
	}

    IEnumerator GameSceneChanger()
    {
        //hold game for a few seconds, then switch scenes
        yield return new WaitForSeconds(1);
        print("switching scenes");
        //calls switch scene function
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
