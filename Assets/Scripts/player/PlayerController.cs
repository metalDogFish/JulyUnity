 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//encapsulated player controls!

public class PlayerController : MonoBehaviour {


    public LayerMask blockingLayer;//collision check on this layer
	public Vector2 moving = new Vector2();
	public bool isPaused;

	protected bool canSwing; 
	private bool facingLeft = false;
	private bool isThrusting;
	public bool isMovingRight;
	public  bool isMovingLeft;

	private CircleCollider2D collider2d;

	// Use this for initialization
	void Start () {
		collider2d = GetComponent<CircleCollider2D> ();
		isThrusting = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isPaused) {
			Check4KeyEntry ();
		}
	}

	//protected void CastLine(){
	protected void CastLine(string direction){
		
		Vector2 startCast = transform.position;
		Vector2 endCast = startCast;
		if (direction == "left") {
			endCast.x -= 10f;
		} else if (direction == "right") {
			endCast.x += 10f;
		}

		//Gizmos.DrawLine (startCast, endCast);
		Debug.DrawLine(startCast, endCast, Color.yellow);
		//dissable our box collider before casting
		collider2d.enabled = false;
		RaycastHit2D hit = Physics2D.Linecast (startCast, endCast);
		collider2d.enabled = true;

		if (hit.collider != null) {
			Debug.Log ("casting "+hit.collider.name);
			//hit.collider.GetComponent<DestructableWall> ().DamageWall (1,endCast);
			//GetComponent<SwapFrontSprite>().SwapTiles(direction);
			//GetComponent<TileHelper>().SwapTiles(direction, transform.position);
			FindObjectOfType<TileHelper>().SwapTiles(direction, transform.position);
		}
	}

	public void Check4KeyEntry(){
		
		//RaycastHit2D hit;
		moving.x = moving.y = 0;
        //prevent movement after death
        if (this.gameObject.GetComponent<Player>().isGameOver == false)
        {
            if (Input.GetKey("right") || isMovingRight == true)
            {
                moving.x = 1;
                facingLeft = false;
            }
            else if (Input.GetKey("left") || isMovingLeft == true)
            {
                moving.x = -1;
                facingLeft = true;
                //Debug.Log ("Left?");
            }

            if (Input.GetKey("up") || isThrusting == true)
            {
                if (GetComponentInParent<Player>().hasFuel)
                {

                    moving.y = 1;
                    //decrease fuel tank
                    GetComponentInParent<Player>().BreathAir(0.1f, "emptyTank");
                }

            }
            else if (Input.GetKey("down"))
            {
                moving.y = -1;
            }
        }
		//when player stops pressing button..
		//if (Input.GetButtonUp ("up")) {
		//isThrusting = false;
		//	ThrustOff();
		//Debug.Log ("lifting finger---------");
		//	}
		if(Input.GetKeyDown(KeyCode.I)){
			GetComponentInParent<Player> ().BreathAir (2f,"emptyTank");
		}

		//if (Input.GetKey ("space") ) {
		if(Input.GetKeyUp("space")){
			//RaycastHit2D hit;
			bool test;
			//AttemptMove(moving.x, moving.y);
			//bool contact = Swing(moving.x, moving.y, out hit);
			if (facingLeft) {
				CastLine ("left");
				//test = FireRayCast(Vector2.left,out hit);
			} else {
				CastLine ("right");
				//test = FireRayCast(Vector2.right,out hit);
			}
			//Debug.Log ("hitting "+test);
			//CastLine();
		}
	}

	public void ThrustOn(){
		isThrusting = true;

	}
	public void ThrustOff(){
		isThrusting = false;
		//turn off thruster sound

	}
	public void MoveLeft(){
		isMovingLeft = true;
	}
	public void MoveRight(){
		isMovingRight = true;
	}
	public void StopMove(){
		isMovingLeft = false;
		isMovingRight = false;
	}
	/*
	//protected void FireRaycast(bool isLeft){
	protected bool FireRayCast(Vector2 direction, out RaycastHit2D hit){	
		int maxDistance = 1;
		//RaycastHit2D hit;

		//dissable our box collider before casting
		collider2d.enabled = false;
			//cast a line from start to end position checking collision on blockinglayer
			//hit = Physics2D.Linecast(start,end, blockingLayer);
		hit = Physics2D.Raycast(transform.position,direction,maxDistance);

		collider2d.enabled = true;
		//check if we got a hit
		if (hit.transform != null) {
			Debug.DrawLine (transform.position, hit.point, Color.red);
			return true;
		} else {
			return false;
		}

		//Debug.DrawLine (transform.position, hit.point);
	}

	/*
	protected void AttemptMove<T>(int xDir, int yDir)where T : Component{
	//protected void AttemptMove(int xDir, int yDir){
		//hit will store whatever our linecast hits
		RaycastHit2D hit;

		bool canSwingIt = Swing(xDir, yDir, out hit);


		if (hit.transform == null) {
			return;
		}

		T hitComponent = hit.transform.GetComponent<T> ();


		//if (canSwingIt && hitComponent != null) {
			//DestructableWall hitWall = Component as DestructableWall;

			//hitWall.DamageWall (1);

			//Animator.SetTrigger ("playeChop");
		//}

	}

	
	//returns true if able to swing at solid. false if not
	protected bool Swing(int xDir, int yDir, out RaycastHit2D hit){
		Vector2 start = transform.position;
		//calculate end position based on the direction paremeters
		Vector2 end = start + new Vector2(xDir, yDir);
		//dissable our box collider before casting
		//BoxCollider2D.enabled = false;
		collider2d.enabled = false;
		//cast a line from start to end position checking collision on blockinglayer
		hit = Physics2D.Linecast(start,end, blockingLayer);
		//reset collider
		//BoxCollider2D.enabled =true;
		collider2d.enabled = true;
		//check if we got a hit
		if (hit.transform != null) {
			//canSwing = false;
			return true;
		}

		//if something was hit, return true
		//canSwing = true;
		return false;
	}
*/

}
