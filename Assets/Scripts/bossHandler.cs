using UnityEngine;
using System.Collections;

public class bossHandler : MonoBehaviour {

	public GameObject enemyShell; //explosive enemy shell
	public GameObject enemyBullet;//enemy bullet

	public GameObject minionPref; //minion prefab, to spawn the two side by side minons

	public Transform barrelEnd; //end of barrel

	public Transform lookLocation;

	public float moveSpeed = 0.3f;

	private bool stopped = false;

	//firing mode stuff
	private bool startingFire = false;
	private bool toggleFireMode = true;
	private int burstNum = 0;
	private float timer2 = .1f;

	//wandering script
	private float timer = 2f;
	private int xVal;
	private int yVal;
	private bool wandering = false;
	public Vector3 wanderPoint;

	//Healthbar Stuff
	public GameObject healthbar;
	public GameObject healthbarRed;
	public Transform healthLook;
	public int healthVal = 10; //Boss Health Amount
	public SpriteRenderer bar;
	public Sprite health9;
	public Sprite health8;
	public Sprite health7;
	public Sprite health6;
	public Sprite health5;
	public Sprite health4;
	public Sprite health3;
	public Sprite health2;
	public Sprite health1;

	// Use this for initialization
	void Start () {
		minonHandler minionLeft = ((GameObject)Instantiate (minionPref, new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), transform.rotation)).GetComponent<minonHandler> ();//spawing left minion
		minionLeft.leftBoss = true; //setting a bool on the minion to true
		minionLeft.isWithBoss = true; //a bool that indicates weahter or not the minion is in range to the boss
		minionLeft.name = "leftMinion"; //setting minion name
		minonHandler minionRight = ((GameObject)Instantiate (minionPref, new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z), transform.rotation)).GetComponent<minonHandler> ();//spawing right minion
		minionRight.leftBoss = false;
		minionRight.isWithBoss = true;
		minionRight.name = "rightMinion";

		spawnEnemy.Instance.spawnNumber += 2;//increasing spawn amount on seperate spawn manager
		spawnEnemy.Instance.maxEnemies += 2;

		bar = healthbar.GetComponent<SpriteRenderer>();//health bar component, rendered as a sprite above the tank
		lookLocation = GameObject.FindWithTag ("LPos").transform; //moving straight at the player
		healthLook = GameObject.FindWithTag ("LookLoc").transform;//far set health look location to orient the bar in the same direction for the whole time
	}
	
	// Update is called once per frame
	void Update () {
		//Health bar rotation
		healthbar.transform.LookAt(healthbar.transform.position + new Vector3 (0, 0, 1), healthLook.transform.position - healthbar.transform.position);
		healthbarRed.transform.LookAt(healthbarRed.transform.position + new Vector3 (0, 0, 1), healthLook.transform.position - healthbarRed.transform.position);

		if (healthVal == 10) {//Health bar values

		} else if (healthVal == 9) {//setting the health bar to the new lowered sprite when health is removed
			bar.sprite = health9;
		} else if (healthVal == 8) {
			bar.sprite = health8;
		} else if (healthVal == 7) {
			bar.sprite = health7;
		} else if (healthVal == 6) {
			bar.sprite = health6;
		} else if (healthVal == 5) {
			bar.sprite = health5;
		} else if (healthVal == 4) {
			bar.sprite = health4;
		} else if (healthVal == 3) {
			bar.sprite = health3;
		} else if (healthVal == 2) {
			bar.sprite = health2;
		} else if (healthVal == 1) {
			bar.sprite = health1;
		} else if (healthVal <= 0) {//when the boss dies
			if (GameObject.Find ("rightMinion") != null) {//seeing if paired minions are still alive, and reseting them to normal minion if they are
				GameObject.Find ("rightMinion").GetComponent<minonHandler> ().isWithBoss = false;
				GameObject.Find ("rightMinion").GetComponent<minonHandler> ().moveSpeed = 1;
			}
			if (GameObject.Find ("leftMinion") != null) {
				GameObject.Find ("leftMinion").GetComponent<minonHandler> ().isWithBoss = false;
				GameObject.Find ("leftMinion").GetComponent<minonHandler> ().moveSpeed = 1;
			}
			spawnEnemy.Instance.isBossSpawned = false; //reseting spawn manager bool
			spawnEnemy.Instance.numberKilled += 25; //adding score
			spawnEnemy.Instance.isBossSpawned = false;
			spawnEnemy.Instance.numberKilled += 25;
			GameManager.Instance.numKilled++;
			Destroy (this.gameObject);
		}

		if (Mathf.Round(transform.position.x) == xVal && Mathf.Round(transform.position.y) == yVal) {//stopping the wander
			moveSpeed = 0;
			transform.LookAt (transform.position + new Vector3 (0, 0, 1), lookLocation.transform.position - transform.position);
			wandering = false;
		}
		/*
		if (atWanderPoint == true) {
			Quaternion targetRotation = Quaternion.LookRotation(lookLocation.position - transform.position, Vector3.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * 250f);      
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
			if (transform.rotation == lookLocation.rotation) {
				wandering = false;
				atWanderPoint = false;
			}
		}*/

		if (stopped == false) { //moving towards the center of the screen before reaching the stopping points
			transform.LookAt (transform.position + new Vector3 (0, 0, 1), lookLocation.transform.position - transform.position); //Rotating towards
			transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
		} else if (stopped == true) {
			if (wandering == false) { //countdown to firing a shell
				timer -= Time.deltaTime;
				if (timer < 0) {
					lookLocation = GameObject.FindWithTag ("Player").transform; //looking at the player to fire
					if (toggleFireMode == false) {
						fireShell ();
						toggleFireMode = true;
					} else if (toggleFireMode == true) {
						startingFire = true; 
						toggleFireMode = false;
					}
					timer = Random.Range (1, 5);
					StartCoroutine("selectWanderPoint");//selecting a wander point
				}
			}
				
			//transform.LookAt (transform.position + new Vector3 (0, 0, 1), lookLocation.transform.position - transform.position); //Rotating towards the player
		}

		if (wandering == true) {
			Quaternion targetRotation = Quaternion.LookRotation(transform.position - wanderPoint, Vector3.forward );//Rotation Script
			transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * 150f);      
			transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);//restricting rotation
		}

		if (transform.position.y <= 3.0f && stopped == false) { //Stopping the boss when it reaches the stopping point
			moveSpeed = 0;
			lookLocation = GameObject.FindWithTag ("Player").transform;
			stopped = true;
		}

		if (startingFire == true) {
			BurstFire ();
		}
	}

	void BurstFire() {
		if (burstNum <= 7) { //if bullets fired less than 7
			timer2 -= Time.deltaTime;
			if (timer2 < 0) { 
				lookLocation = GameObject.FindWithTag ("Player").transform;
				fireBullet ();//fire bullet
				burstNum++;
				timer2 = .1f;
			}
		} else if (burstNum >= 8) { //ending fire sequence
			startingFire = false;
			burstNum = 0;
		}
	}

	void fireShell () {
		explosiveBullet shot = ((GameObject)Instantiate (enemyShell, barrelEnd.position, transform.rotation)).GetComponent<explosiveBullet> ();
		shot.explosionTime = 1.25f;
		shot.isEnemy = true;
	}

	void fireBullet() {
		bulletHandler shot = ((GameObject)Instantiate (enemyBullet, barrelEnd.position, transform.rotation)).GetComponent<bulletHandler> ();//instanciating enemy bullet
		shot.isEnemyBull = true;
	}

	IEnumerator selectWanderPoint() { //picking a random location to wander to fire at the player, within the constraints of x -5 to 5 and y 3, -1
		yield return new WaitForSeconds (0.75f);
		xVal = Random.Range(-5, 6);
		yVal = Random.Range (3, 5);
		moveSpeed = 0.3f;
		wandering = true;
		//Debug.Log (xVal);
		//Debug.Log (yVal);
		wanderPoint = new Vector3(xVal, yVal, 1);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Bullet") {
			healthVal--;
		} else if (col.gameObject.tag == "barbedWire") {//destroying any collided barbedwire
			if (col.gameObject.GetComponent<barbedWireHandler> ().prePlace == false) {
				Destroy (col.gameObject);
			}
		}
	}
}
