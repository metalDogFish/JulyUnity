using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBouncer : MonoBehaviour {

    //attach the background sprite
    public GameObject go_BG;

    public bool isMovingLeft = true;
    private int intTimer, intTimerMax;
	// Use this for initialization
	void Start () {
        intTimer = intTimerMax = 3200;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMovingLeft)
        {
            intTimer--;

            go_BG.transform.Translate(Vector2.left * Time.deltaTime);

           // if(go_BG.transform.position.x < -484)
           if(intTimer < 0)
            {
                isMovingLeft = false;
                intTimer = intTimerMax;
                print("moving BG to the right now");
            }
        }
        else
        {
            intTimer--;

            go_BG.transform.Translate(Vector2.right* Time.deltaTime);

            if (intTimer < 0)
            {
                isMovingLeft = true;
                intTimer = intTimerMax;
                print("moving BG to the left now");
            }
        }
	}
}
