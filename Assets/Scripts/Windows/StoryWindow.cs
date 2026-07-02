using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryWindow : GenericWindow {

    public Text storyText;
    // Use this for initialization
    void Start () {
        UpdateText();
	}
	
	
    public void Continue()
    {
        // Debug.Log("Continue pressed");
        // SceneManager.LoadScene("TopScene");
        manager.Open(2);
    }
    public void OnCancel()
    {
        manager.Open(0);
    }
    private void UpdateText()
    {
        storyText.text = "Now that phase three is soon commencing, The offer of a lifetime is here and now.\n" + "Cyberdine industries is now offering huge reward incentives to any " +
            "employess willing to relocate to our brand new office " + "Crystal Palace" + " located on the moon.\n" + "Be one of the first in your school" +
            " or neihborhood to live, eat, laugh and explore Mars!\n" + "Image watching the glorious sunrise through the crimson red hue of the Martian athmosphere," +
            " or feeling the fine, fine grains of warm red sand between your toes as you slide down a giant sand dune.";
       // label.text += "Stat " + currentStat + "  ";
        //value.text += Random.Range(0, 1000).ToString("D4") + "  ";
    }
}
