using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

	public float speed = 150f;
	public Vector2 maxVelocity = new Vector2(60,100);
	public float jetSpeed = 200f;
	public bool standing;
	public float standingThreshold = 4f;
	public float airSpeedMultiplier = 0.3f;
	public int playerLevel = 0;
	public int ScoreGoal;
	//tweek the health for each level-adds to baselevel
	public int healthAdjust;
	[HideInInspector]
	public bool hasFuel;
	public bool isGameOver;
	public bool isPaused;
	public GameObject gameOverObj;
	//public GameObject pointText;
	public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Text replayText;
	public GameObject replayGroup;
	//public UnityEngine.UI.Slider airSlider;
	//sound
	public AudioClip thudSound;
	public AudioClip rocketSound;
	public AudioClip leftFootSound;
	public AudioClip rightFootSound;
    public AudioClip winSound;
    public AudioClip breathSound;

    AudioSource breathSrc;

    //for pushable box
    protected List<Pushable> m_CurrentPushables = new List<Pushable>(4);
    protected Pushable m_CurrentPushable;
    protected Vector2 m_MoveVector;
    protected Transform m_Transform;
    Vector2 m_PreviousPosition;
    Vector2 m_CurrentPosition;
    protected readonly int m_HashPushingPara = Animator.StringToHash("Pushing");
    //masking
    //public GameObject maskObject;
    //enum deathDangers{ Suffocation,Spikes,Alien,Acid};

    private Rigidbody2D body2D;
	private SpriteRenderer renderer2D;
	private PlayerController controller;
	private Animator animator;
	private int score = 0;

	[SerializeField]
	protected Stat health;
	private float initHealth = 100;

    
    // Use this for initialization
    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
        renderer2D = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        //-these should be in Awake()-----
        m_Transform = transform;
        m_CurrentPosition = body2D.position;
        m_PreviousPosition = body2D.position;
    }

    void Start () {
		health.Initialize (initHealth+healthAdjust, initHealth+healthAdjust);
		hasFuel = true;
		isGameOver = isPaused = false;	

		scoreText.text = "Score " + score;
		replayText.enabled = false;//turn off until gameover
		replayGroup.SetActive(false);
        //turn on breath
       // GameObject go = new GameObject("BreathSound");
       // AudioSource baSrc = go.AddComponent<AudioSource>();

        PlayBreathSound();


    }
	
	// Update is called once per frame
	void Update () {

		if (!isPaused) {
			var absVelX = Mathf.Abs (body2D.velocity.x);
			var absVelY = Mathf.Abs (body2D.velocity.y);

			if (absVelY <= standingThreshold) {
				standing = true;
			} else {
				standing = false;
			}

			var forceX = 0f;
			var forceY = 0f;

			if (controller.moving.x != 0) {
				//if (Input.GetKey ("right")) {//done inside controller
				if (absVelX < maxVelocity.x) {

					var newSpeed = speed * controller.moving.x;

					forceX = standing ? newSpeed : (newSpeed * airSpeedMultiplier);

					renderer2D.flipX = forceX < 0;
				}

				animator.SetInteger ("AnimState", 1);
			} else {//idle
				animator.SetInteger ("AnimState", 0);
			}

			if (controller.moving.y > 0) {
			
				playRocketSound ();
				//if (Input.GetKey ("up")) {
				if (absVelY < maxVelocity.y) {
					forceY = jetSpeed * controller.moving.y;
				}

				animator.SetInteger ("AnimState", 2);
			} else if (absVelY > 0 && !standing) {
				animator.SetInteger ("AnimState", 3);
			}

			body2D.AddForce (new Vector2 (forceX, forceY));
		}
	}
	//just for adding thud sound-possibly dust?
	void OnCollisionEnter2D(){

		if (!standing) {
			var absVelX = Mathf.Abs (body2D.velocity.x);
			var absVelY = Mathf.Abs (body2D.velocity.y);

			if (absVelX <= 0.1f || absVelY <= 0.1f) {
				if (thudSound) {
					AudioSource.PlayClipAtPoint (thudSound, transform.position);
				}
			}
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Pushable pushable = other.GetComponent<Pushable>();
        if (pushable != null)
        {
            m_CurrentPushables.Add(pushable);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Pushable pushable = other.GetComponent<Pushable>();
        if (pushable != null)
        {
            if (m_CurrentPushables.Contains(pushable))
                m_CurrentPushables.Remove(pushable);
        }
    }

    public void CheckForPushing()
    {
        bool pushableOnCorrectSide = false;
        Pushable previousPushable = m_CurrentPushable;

        m_CurrentPushable = null;

        if (m_CurrentPushables.Count > 0)
        {
            // bool movingRight = PlayerInput.Instance.Horizontal.Value > float.Epsilon;
            bool movingRight = controller.isMovingRight;
            //bool movingLeft = PlayerInput.Instance.Horizontal.Value < -float.Epsilon;
            bool movingLeft = controller.isMovingLeft;

            for (int i = 0; i < m_CurrentPushables.Count; i++)
            {
                float pushablePosX = m_CurrentPushables[i].pushablePosition.position.x;
                float playerPosX = m_Transform.position.x;
                if (pushablePosX < playerPosX && movingLeft || pushablePosX > playerPosX && movingRight)
                {
                    pushableOnCorrectSide = true;
                    m_CurrentPushable = m_CurrentPushables[i];
                    break;
                }
            }

            if (pushableOnCorrectSide)
            {
                Vector2 moveToPosition = movingRight ? m_CurrentPushable.playerPushingRightPosition.position : m_CurrentPushable.playerPushingLeftPosition.position;
                // moveToPosition.y = m_CharacterController2D.Rigidbody2D.position.y;
                moveToPosition.y = body2D.position.y;
                // m_CharacterController2D.Teleport(moveToPosition);
                Vector2 delta = moveToPosition - m_CurrentPosition;
                m_PreviousPosition += delta;
                m_CurrentPosition = moveToPosition;
                //m_Rigidbody2D.MovePosition(moveToPosition);
                body2D.MovePosition(moveToPosition);
            }
        }

        if (previousPushable != null && m_CurrentPushable != previousPushable)
        {//we changed pushable (or don't have one anymore), stop the old one sound
            previousPushable.EndPushing();
        }

        animator.SetBool(m_HashPushingPara, pushableOnCorrectSide);
    }

    public void MovePushable()
    {
        //we don't push ungrounded pushable, avoid pushing floating pushable or falling pushable.
        if (m_CurrentPushable && m_CurrentPushable.Grounded)
            m_CurrentPushable.Move(m_MoveVector * Time.deltaTime);
    }

    public void StartPushing()
    {
        if (m_CurrentPushable)
            m_CurrentPushable.StartPushing();
    }

    public void StopPushing()
    {
        if (m_CurrentPushable)
            m_CurrentPushable.EndPushing();
    }

   void StopBreathSound()
    {    

        breathSrc.Stop();
    }

    void PlayBreathSound()
    {
        if (!breathSound)
            return;

        
        GameObject go = new GameObject("BreathSound");
        breathSrc = go.AddComponent<AudioSource>();
        breathSrc.clip = breathSound;
        breathSrc.loop = true;
        breathSrc.volume = 0.4f;
        breathSrc.Play();

        //with repeat unitil death?
    }

    void stopRocketSound()
    {
        GameObject go = new GameObject("RocketSound");
        AudioSource aSrc = go.AddComponent<AudioSource>();
        aSrc.clip = rocketSound;
        aSrc.volume = 0.5f;
        aSrc.Stop();
    }

    void playRocketSound(){

		if (!rocketSound || GameObject.Find ("RocketSound"))
			return;

		GameObject go = new GameObject ("RocketSound");
		AudioSource aSrc = go.AddComponent<AudioSource> ();
		aSrc.clip = rocketSound;
		//aSrc.pitch = Random.Range (0.5f, 1.2f);
		aSrc.volume = 0.1f;
		aSrc.Play ();
		float duration = rocketSound.length;

		Debug.Log(duration + " rocketSound length");
		//this destroys clip 0.439 sec after creation. (shortening this amount causes thrust to sound 'choppy')
		Destroy (go, (duration - 0f));
	}

	void playFootStepLeft(){
		if (leftFootSound) {
			AudioSource.PlayClipAtPoint (leftFootSound, transform.position);
		}
	}

	void playFootStepRight(){
		if (rightFootSound) {
			AudioSource.PlayClipAtPoint (rightFootSound, transform.position);
		}
	}

	//triggered by collectables
	public void AwardPoints(){
		score += 1;
		if (score >= ScoreGoal) {
			PromptRadioBeacon ();
		} else {
			scoreText.text = "Score " + score;
		}
	}
	public void AwardDeath(string word){
		isGameOver = true;
		gameOverObj.GetComponent<GameOver>().ShowLose();
		//score = 0;
		//scoreText.text = "You dead now, "+PlayerStats.PlayerName;
		GetDeathQuips(word);
		replayText.enabled = true;
		replayGroup.SetActive(true);
		//kill flickerlight
		var script = GameObject.FindObjectOfType<GManagerScript>();
		script.maskObject.GetComponent<SMask> ().SwitchFlicker ();
        //maskObject.GetComponent<SMask> ().SwitchFlicker();
        //this will prevent key registery..stop player from walking while dead.
        // this.gameObject.GetComponent<PlayerController>().isPaused = true;
        // stopRocketSound();
        StopBreathSound();

    }
	public void AwardWin(){

		isGameOver = true;
		//gameOverObj.ShowWin (a);

		int numH = Mathf.FloorToInt (health.MyCurrentValue);
		scoreText.text = numH +" fuel remaining.";
		//find radioWaves and startBeacon()
		var beacon = FindObjectOfType<RadioWaves> ();
		beacon.StopBeacon ();
        //var beacon = FindObjectOfType<RadioWaves> ().StopBeacon ();
       // controller.ThrustOff();
        //stop player input
        controller.isPaused = true;
        //bug alert!- thrust sound can loop endlessly!
        //FindObjectOfType<PlayerController>().ThrustOff();//no effect
        // controller.ThrustOff();//no
        //play onother sound to override sound loop?
        //AudioSource.PlayClipAtPoint(winSound, transform.position);//no
        stopRocketSound(); //bingo!
        StopBreathSound();
        //play the win sound
        AudioSource.PlayClipAtPoint(winSound, transform.position);

        //kills physics engine
        body2D.isKinematic = true;
		//center player on beacon and stop momentum
		body2D.velocity = new Vector2(0,0);
		body2D.MovePosition (beacon.transform.position);
		//turn on button choices
		replayGroup.SetActive(true);
		//replayText.text = "Continue?";
		replayText.enabled = true;
		//save the points
		gameOverObj.GetComponent<GameOver>().ShowWin (numH);

			string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;

			Debug.Log("scene "+ sceneName);

			if(numH > PlayerPrefs.GetInt(sceneName, 0)){
				//starIdx > PlayerPrefs.GetInt(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,0)){
				//PlayerPrefs.SetInt(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,starIdx);
				PlayerPrefs.SetInt(sceneName,1);
				//PlayerPrefs.SetInt(sceneName + "Score", PlayerStats.levelScoresDict ["SceneA"]);
			}

		//if (numH > PlayerStats.Points) {
		if(numH > PlayerStats.levelScoresDict[sceneName]){
			//PlayerStats.Points = numH;
			PlayerStats.levelScoresDict[sceneName] = numH;//problem
			PlayerPrefs.SetInt(sceneName + "Score", PlayerStats.levelScoresDict [sceneName]);
			//
			//PlayerPrefs.SetInt("LevelA", PlayerStats.levelScoresDict ["SceneA"]);
			//PlayerPrefs.SetInt("LevelB", PlayerStats.levelScoresDict ["SceneB"]);
			//PlayerPrefs.SetInt("LevelC", PlayerStats.levelScoresDict ["SceneC"]);
		}

	}

	public void BreathAir(float damage, string danger){
		var h = health.MyCurrentValue -= damage;
		Debug.Log ("oo"+h);
		if (health.MyCurrentValue <= 0) {
			Debug.Log ("you cant breath anymore------------------dead.");
			hasFuel = false;
            //also works if flying outside mapgrid!
            if (danger == "emptyTank")
            {
                AwardDeath("Suffication");
                //explode-maybe something else-like floating with random spin.
                // this.gameObject.GetComponent<Explode>().OnExplode();
                //add animation here later
                CharacterSpecialAnim();
            }
            else if(danger == "acid")
            {
                AwardDeath("Acid");
                //explode
                this.gameObject.GetComponent<Explode>().OnExplode();
            }
		}
	}

    public void CharacterSpecialAnim()
    {
        //turn player horizontal, stop movement
        m_Transform.Rotate(0,0,90f,Space.World);
        //controller.isPaused = true;
    }
/*
    public void CharacterReset()//anyone calling?not necessary
    {
        m_Transform.Rotate(0, 0, 0, Space.World);
    }
    */

    public void GetDeathQuips(string word){
		//scoreText.text = ("Death by  " + deathDangers.Suffocation.ToString());
		scoreText.text = ("Death by  " + word.ToString());
	}

	public void PromptRadioBeacon(){
		scoreText.text = "find the beacon for the win..";
		FindObjectOfType<RadioWaves> ().StartBeacon ();
	}
}
