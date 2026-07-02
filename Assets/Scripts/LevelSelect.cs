using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Text missionText;
	public UnityEngine.UI.Text highScoreName;
	public GameObject panel;
	//public UnityEngine.UI.Image[] stars;
	private MusicManager mm;
	private string hsName;
	//keep track of level scores-move to playerStats class
	/*
	public static Dictionary<string, int> levelScoresDict = new Dictionary<string, int>()
	{
		{ "level1", 0},
		{ "level2", 0},
		{ "level3", 0}
	};
*/
	[System.Serializable]
	public struct ButtonPlayerPrefs
	{
		public GameObject gameObject;
		public string playerPrefKey;
	}

	public ButtonPlayerPrefs[] buttons;

	// Use this for initialization
	void Start () {

		//Debug.Log (levelScoresDict["level1"]);

		nameText.text = hsName = PlayerPrefs.GetString ("PlayerName");

        //mm = FindObjectOfType<MusicManager>();
        mm = MusicManager.instance;
			//nameText.text = PlayerStats.PlayerName;

		for (int i = 0; i < buttons.Length; i++) {
		
			int score = PlayerPrefs.GetInt (buttons [i].playerPrefKey, 0);

			for (int starIdx = 1; starIdx <= 3; starIdx++) {
				Transform star = buttons [i].gameObject.transform.Find ("star" + starIdx);

				if (starIdx <= score) {
					star.gameObject.SetActive (true);
				} else {
					star.gameObject.SetActive (false);
				}
			}
		}
	}
	


	public void OnButtomPress(string levelName){
		//stop the music
		//GetComponent<MusicManager>().musicSource.Stop();
		//mm.musicSource.Stop();

        mm.QuickMusicFadeOut();
        
		//switch scene
		//UnityEngine.SceneManagement.SceneManager.LoadScene (levelName);
        //improve the above code by adding transition tween
        FindObjectOfType<LevelLoader>().LoadLevelByName(levelName);
	}

	public void OnExitPress(){
		Debug.Log ("quitting");
        //save high score to playerPref
        //if(PlayerStats.Points > PlayerPrefs.GetInt("TopScore")){

//PlayerPrefs.SetInt("TopScore", PlayerStats.Points);
//	}
//PlayerPrefs.SetInt("TopScore", 0);
//PlayerPrefs.SetInt("LevelA", PlayerStats.levelScoresDict ["SceneA"]);
//PlayerPrefs.SetInt("LevelB", PlayerStats.levelScoresDict ["SceneB"]);
//PlayerPrefs.SetInt("LevelC", PlayerStats.levelScoresDict ["SceneC"]);

//System.Diagnostics.Process.GetCurrentProcess().Kill();//alternative to Quit
//I thought quit wasn't working- because i could still see the game in background apps.
//research tells me this is simply a new loading feature in phones used for faster loading times.
//the game HAS quit as expected, 
/*
 * Yes, the "background apps" are actually called "recent apps" ;) If you quit the game with Application.Quit it's really closed.
 * When you tab it again it will completely restart. The feature of recent apps is an OS feature. You can't remove your app from this list.
Just check the real list of running applications. Go into settings/Apps/running and you see a list of currently running apps and services.
You will never see your Untiy app there because it's suspended when you press the home button. 
tab the button on the top right "show cached processes". If you didn't quit your application you will see a cached process there 
which will be reloaded when you switch back to your app.
If you quit your app, it won't be listed there. The recent apps is really just a list of recently used apps which includes the ones that are currently running. You can't distinguish between them in the "recent apps view", so i usually keep the list short / empty as a user ;)
Cached processes are usually removed when the OS runs out of memory, but they have a hit on performance either way.
So cleaning up the memory is one of the main tasks of the user on an Android tablet :D
edit btw your settings menu might look differently. I have a nexus7 tablet, just in case you can't find the menu items i've mentioned.
*/
Application.Quit ();

}

public void DisplayMission(int num){

//panel.SetActive (true);
//display message
if (num == 1) {
    missionText.text = "prepare for mission " + "\n" + " best score " + "\n" + PlayerPrefs.GetInt ("SceneAScore");//PlayerStats.levelScoresDict ["SceneA"];//PlayerStats.Points;
    //hignScoreName = PlayerPrefs.GetString("playerName");
    highScoreName.text = hsName;
} else if (num == 2) {
    missionText.text = "prepare for mission  " + "\n" + " top score " + "\n" + PlayerPrefs.GetInt ("SceneBScore");//PlayerStats.levelScoresDict ["SceneB"];//PlayerPrefs.GetInt("TopScore");
    highScoreName.text = hsName;
} else if (num == 3) {
    missionText.text = "prepare for mission " + "\n" + " best score " + "\n" + PlayerPrefs.GetInt ("SceneCScore");// PlayerStats.levelScoresDict ["SceneC"];
    highScoreName.text = hsName;
}else if (num == 4) {
    missionText.text = "prepare for mission " + "\n" + " best score " + "\n" + PlayerPrefs.GetInt ("SceneFScore");
    highScoreName.text = hsName;
}else if (num == 5) {
    missionText.text = "prepare for mission " + "\n" + " best score " + "\n" + PlayerPrefs.GetInt ("SceneEScore");
    highScoreName.text = hsName;
}
else if (num == 6)
{
    missionText.text = "prepare for mission " + "\n" + " best score " + "\n" + PlayerPrefs.GetInt("SceneDoorScore");
    highScoreName.text = hsName;
}
else {
    missionText.text = "error";
}
}

public void ClearMessage(){
//fade out over time
missionText.text = "";
highScoreName.text = "";
//panel.SetActive (false);
}


}
