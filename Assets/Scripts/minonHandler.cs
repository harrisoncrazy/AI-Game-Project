using UnityEngine;
using System.Collections;

public class minonHandler : MonoBehaviour {

	public GameObject enemeyBullet;

	public Transform barrelEnd;

	public bool isOutOfBounds = false;

	public Transform player;
	public Transform leftLook;
	public Transform rightLook;
	private bool reachedDest = false;

 	public float moveSpeed = 1.5f;
	private float timer = 1;
	public int rando;

	//Boss Minion stuff
	public bool isWithBoss = false;
	public bool leftBoss = false;
	private Transform minionSpot;
	public bool isOutofRange = false;
	public float dist;

	private bool checkfornewPlayer; //bool for swaping player

	// Use this for initialization
	void Start () {
		rando = Random.Range (1, 3);
		player = GameObject.FindWithTag ("Player").transform;
		leftLook = GameObject.FindWithTag ("leftLook").transform;
		rightLook = GameObject.FindWithTag ("rightLook").transform;

		if (leftBoss == true) {
			minionSpot = GameObject.FindWithTag ("leftMinionLook").transform;
			moveSpeed = .3f;
			timer = 1f;
		} else if (isWithBoss == true){
			minionSpot = GameObject.FindWithTag ("rightMinionLook").transform;
			moveSpeed = .3f;
			timer = 1f;
		}
	}

	// Update is called once per frame
	void Update () {
		if (player == null) {//checking for null player
			if (checkfornewPlayer == false) {
				checkfornewPlayer = true;
				player = GameObject.FindWithTag ("Player").transform;
			}
		}
		if (player != null) {
			if (isWithBoss == true) {//if minion spawned is one of the paired boss minions
				dist = Vector3.Distance(minionSpot.position, transform.position);
				if (dist >= 0.5f) {//keeping the minions close to the tank
					isOutofRange = true;
				} else {
					isOutofRange = false;
				}

				if (dist >= 1.0f) {
					moveSpeed = 1.0f;
				} else {
					moveSpeed = .3f;
				}

				if (leftBoss == true) {//left minion
					if (isOutofRange == true) {
						transform.LookAt (transform.position + new Vector3 (0, 0, 1), minionSpot.transform.position - transform.position); //Rotating towards the point
						transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
					} else if (isOutofRange == false) { //looking at the player and firing once stopped
						transform.LookAt (transform.position + new Vector3 (0, 0, 1), player.transform.position - transform.position); //Rotating towards the point
						timer -= Time.deltaTime;//firing bullet timer
						if (timer < 0) {
							fireBullet ();
							timer = 1.5f;

						}
					}

				} else if (leftBoss == false) {//right minon
					
					if (isOutofRange == true) {
						transform.LookAt (transform.position + new Vector3 (0, 0, 1), minionSpot.transform.position - transform.position); //Rotating towards the point
						transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
					} else if (isOutofRange == false) {//looking at the player and firing once stopped
						transform.LookAt (transform.position + new Vector3 (0, 0, 1), player.transform.position - transform.position); //Rotating towards the point
						timer -= Time.deltaTime;//firing bullet timer
						if (timer < 0) {
							fireBullet ();
							timer = 1;

						}
					}
				}
			} else {
				if (isOutOfBounds == false) {//Default Minion behavior, rotating and moving towards the player
				
					transform.LookAt (transform.position + new Vector3 (0, 0, 1), player.transform.position - transform.position); //Rotating towards the player
					transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward

				} else if (isOutOfBounds == true) {//if the minion spawns outside the screen, it moves down and then comes at the player from the sides
				
					transform.position += transform.up * Time.deltaTime * moveSpeed; //regular moving forward

					if (transform.position.x <= -6.5) { //if the minion is on the left side, rotate towards the left look anchor
						transform.LookAt (transform.position + new Vector3 (0, 0, 1), leftLook.transform.position - transform.position);

					} else if (transform.position.x >= 6.5) {//if the minion is on the right side, rotate towards the right look anchor
						transform.LookAt (transform.position + new Vector3 (0, 0, 1), rightLook.transform.position - transform.position);
					}
					if (transform.position.y <= 0) { //once the minion reaches the point, it turns back towards the player
						isOutOfBounds = false;
					}
				}
				if (reachedDest == true) {
					timer -= Time.deltaTime;//firing bullet timer
					if (timer < 0) {
						fireBullet ();
						timer = Random.Range (0, 3);
			
					}
				}
			}
		}
	}

	void fireBullet() {
		bulletHandler shot = ((GameObject)Instantiate (enemeyBullet, barrelEnd.position, transform.rotation)).GetComponent<bulletHandler> ();//instanciating enemy bullet
		shot.isEnemyBull = true;
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Blocker") { //if the minion reaches the firing position
			if (isWithBoss != true) {
				moveSpeed = 0;
			}
			reachedDest = true;
		} else if (col.gameObject.tag == "Bullet") {
			Destroy (this.gameObject);
		} else if (col.gameObject.tag == "barbedWire") {
			if (col.gameObject.GetComponent<barbedWireHandler> ().prePlace == false) {
				moveSpeed = moveSpeed / 2;
			}
		}
	}
		
	void OnCollisionStay2D (Collision2D col) {//collision script so minions dont bunch up *NEEDS WORK*
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "lieutenantEnemy" && col != null) {//randomly assigns a minion a minion at start a number between 1 and 2
			if (rando == 1) {
				if (timer >= 0) {
					transform.position += transform.right * Time.deltaTime;//moves to the right if 1
					timer -= Time.deltaTime;
				}

			} else if (rando == 2) {
				if (timer >= 0) {
					transform.position -= transform.right * Time.deltaTime;//moves to the right if 1
					timer -= Time.deltaTime;
				}
			}
		}
	
	}


	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "barbedWire") {
			if (col.gameObject.GetComponent<barbedWireHandler> ().prePlace == false) {
				moveSpeed = moveSpeed * 2;
			}
		}
	}
}
