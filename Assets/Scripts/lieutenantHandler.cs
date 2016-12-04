using UnityEngine;
using System.Collections;

public class lieutenantHandler : MonoBehaviour {

	public GameObject enemyBullet; //Fired Bullet

	public Transform barrelEnd; //End of the barrel, where the bullets are instanciated

	public Transform lookLocation; //Original look location, set way behind the player so as to not move with the player

<<<<<<< HEAD
	public float moveSpeed = 0.5f; //Movespeed, what are you dumb?
=======
	private float moveSpeed = 1f;
>>>>>>> origin/master

	public int spawnedPos; //the number in the array of points that the guy is spawned at

	public bool leftMove;
	public float moveSwapTimer = 3;

	//BurstFire stuff
	private float timer = 1f;
	private bool stopped;
	private bool startingFire = false;
	private int burstNum = 0;
	private float timer2 = .1f;

	//Wander values
	private bool wandering = false;
	public int xVal;
	public int yVal;
	private Vector3 wanderPoint;

	private bool slowed = false;

	private bool checkfornewPlayer = false;

	// Use this for initialization
	void Start () {
		lookLocation = GameObject.FindWithTag ("LPos").transform; //the original point to move towards
		StartCoroutine ("stopMoving"); //starting the countdown to stopping the original move
	}
	
	//So what this does is, the lieutenant spawns at one of the 6 selected points, then moves towards the player stopping after a set amount of seconds
	//then the guy goes into firing mode, firing a burst of 3 bullets, then selecting a randomized wanderpoint, heading towards it, stopping then firing again, etc etc
	void Update () {
<<<<<<< HEAD
		if (Mathf.Round(transform.position.x) == xVal && Mathf.Round(transform.position.y) == yVal) {//stopping the wander when it reaches the destination NEEDS TO BE ROUNDED OR IT WONT "ACTUALLY" reach the specific point
=======
		if (lookLocation == null) {//checking for null player
			if (checkfornewPlayer == false) {
				checkfornewPlayer = true;
				lookLocation = GameObject.FindWithTag ("Player").transform;
			}
		}

		if (Mathf.Round(transform.position.x) == xVal && Mathf.Round(transform.position.y) == yVal) {//stopping the wander
>>>>>>> origin/master
			moveSpeed = 0;
			wandering = false;
		}

		if (wandering == true) {//bool for wandering, and not wandering
			transform.LookAt (transform.position + new Vector3 (0, 0, 1), wanderPoint - transform.position); //Rotating towards wanderpoint
			transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
		} else if (wandering == false) {
			transform.LookAt (transform.position + new Vector3 (0, 0, 1), lookLocation.transform.position - transform.position); //Rotating towards the player
			transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
		}

		if (spawnedPos == 1) {//array of lieutenant spaws stored in a seperate spawning script, enabled or disabled as they spawn
			spawnEnemy.Instance.Lspawn1Disabled = true;
		} else if (spawnedPos == 2) {
			spawnEnemy.Instance.Lspawn2Disabled = true;
		} else if (spawnedPos == 3) {
			spawnEnemy.Instance.Lspawn3Disabled = true;
		} else if (spawnedPos == 4) {
			spawnEnemy.Instance.Lspawn4Disabled = true;
		} else if (spawnedPos == 5) {
			spawnEnemy.Instance.Lspawn5Disabled = true;
		} else if (spawnedPos == 6) {
			spawnEnemy.Instance.Lspawn6Disabled = true;
		}

		if (stopped == true) {//begining firing once stopped
			if (wandering == false) {
				timer -= Time.deltaTime;
				if (timer < 0) {
<<<<<<< HEAD
					lookLocation = GameObject.FindWithTag ("Player").transform;//looks at player to fire
					timer = Random.Range (1, 5);//new firing cooldown
=======
					lookLocation = GameObject.FindWithTag ("Player").transform;
					timer = Random.Range (1, 3);
>>>>>>> origin/master
					startingFire = true;
					StartCoroutine("selectWanderPoint");//new wanderpoint
				}
			}
		}
		if (startingFire == true) {
			BurstFire ();
		}
	}

	void BurstFire() {
		if (burstNum <= 2) { //if bullets fired less than 3
			timer2 -= Time.deltaTime;
			if (timer2 < 0) { 
				lookLocation = GameObject.FindWithTag ("Player").transform;
				fireBullet ();//fire bullet
				burstNum++;
				timer2 = .1f;//toggling this code segment on and off 3 times in quick sucession
			}
		} else if (burstNum >= 3) { //ending fire sequence
			startingFire = false;
			burstNum = 0;
		}
	}

	IEnumerator selectWanderPoint() { //Lieutenant picking a random location to wander to fire at the player, within the constraints of x -5 to 5 and y 3, -1
		yield return new WaitForSeconds (.75f);
		xVal = Random.Range(-5, 6);
		yVal = Random.Range (-1, 4);
		moveSpeed = 1f;
		wandering = true;
		//Debug.Log (xVal);
		//Debug.Log (yVal);
		wanderPoint = new Vector3(xVal, yVal, 1);//setting the vector 3 wander point
	}

	void fireBullet() {
		//yield return new WaitForSeconds (.1);
		bulletHandler shot = ((GameObject)Instantiate (enemyBullet, barrelEnd.position, transform.rotation)).GetComponent<bulletHandler> ();//instanciating enemy bullet
		shot.isEnemyBull = true;
	}

	IEnumerator stopMoving () { //stopping the movement after a certain amount of time
		yield return new WaitForSeconds (4);
		stopped = true;
		moveSpeed = 0;
	} 

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Bullet") {
			if (spawnedPos == 1) {
				spawnEnemy.Instance.Lspawn1Disabled = false;
			} else if (spawnedPos == 2) {
				spawnEnemy.Instance.Lspawn2Disabled = false;
			} else if (spawnedPos == 3) {
				spawnEnemy.Instance.Lspawn3Disabled = false;
			} else if (spawnedPos == 4) {
				spawnEnemy.Instance.Lspawn4Disabled = false;
			} else if (spawnedPos == 5) {
				spawnEnemy.Instance.Lspawn5Disabled = false;
			} else if (spawnedPos == 6) {
				spawnEnemy.Instance.Lspawn6Disabled = false;
			}

			Destroy (col.gameObject);
			Destroy (this.gameObject);
		}
		else if (col.gameObject.tag == "barbedWire") { //slowing down in barbed wire
			if (slowed == false) {
				if (col.gameObject.GetComponent<barbedWireHandler> ().prePlace == false) {//making sure the wire isnt in preplacemode as to not prematurily interact
					moveSpeed = moveSpeed / 2;
					slowed = true;
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "barbedWire") { //speeding up after leaving wire
			if (col.gameObject.GetComponent<barbedWireHandler> ().prePlace == false) {
				moveSpeed = moveSpeed * 2;
				slowed = false;
			}
		}
	}
}
