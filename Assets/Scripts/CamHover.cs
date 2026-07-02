using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHover : MonoBehaviour {

    Camera cam;
    public int scanTimeInterval = 10;
    // public GameObject backGroundObj;
    public float speed = 0.02f;
    private Transform t;
    private Vector3 originVec;
    private SpriteRenderer bgSpRe;
    private bool isYNegative = true;

    // Use this for initialization
    void Awake() {
         cam = FindObjectOfType<Camera>();
        bgSpRe = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        t = transform;
        originVec = transform.position;
        Debug.Log("posistion " + originVec);
    }
    // Update is called once per frame
    void Update () {

        //transform.position = new Vector3((t.position.x), t.position.y, transform.position.z);
        IncrementPosition();
    }

    public void IncrementPosition()
    {
        //inch the bg forward in xydirection
        // this.transform.position.x += 1;
        if (isYNegative == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            if (transform.position.y <= -100)
            {
                isYNegative = !isYNegative;
            }
        }
        else if (isYNegative == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            if (transform.position.y >= 100)
            {
                isYNegative = !isYNegative;
            }
        }

    }
    public Vector3 RandomPositioner()
    {

        return new Vector3(t.position.x + Random.Range(-5,5),t.position.y +Random.Range(-5,5),t.position.z +0);
    }
}
/*take in bg for boundry calls
 * when btm left side of bg gets in range, reposition so that top right is in view. make a loop
 * 
 * */