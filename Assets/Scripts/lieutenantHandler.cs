using UnityEngine;
using System.Collections;

public class lieutenantHandler : MonoBehaviour {

	public GameObject enemyBullet;

	public Transform barrelEnd;

	public Transform lookLocation;

	public float moveSpeed = 1.0f;

	public int spawnedPos;

	public bool leftMove;
	public float moveSwapTimer = 3;

	//BurstFire stuff
	private float timer = 1f;
	private bool stopped;
	public bool startingFire = false;
	public int burstNum = 0;
	public float timer2 = .1f;

	//Wander values
	private bool wandering = false;
	public int xVal;
	public int yVal;
	private Vector3 wanderPoint;

	private bool slowed = false;

	// Use this for initialization
	void Start () {
		lookLocation = GameObject.FindWithTag ("LPos").transform;
		StartCoroutine ("stopMoving");
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Round(transform.position.x) == xVal && Mathf.Round(transform.position.y) == yVal) {//stopping the wander
			moveSpeed = 0;
			wandering = false;
		}

		if (wandering == true) {
			transform.LookAt (transform.position + new Vector3 (0, 0, 1), wanderPoint - transform.position); //Rotating towards the player
			transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
		} else if (wandering == false) {
			transform.LookAt (transform.position + new Vector3 (0, 0, 1), lookLocation.transform.position - transform.position); //Rotating towards the player
			transform.position += transform.up * Time.deltaTime * moveSpeed; //moving forward
		}

		if (spawnedPos == 1) {
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
					lookLocation = GameObject.FindWithTag ("Player").transform;
					timer = Random.Range (1, 3);
					startingFire = true;
					StartCoroutine("selectWanderPoint");
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
				timer2 = .1f;
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
		wanderPoint = new Vector3(xVal, yVal, 1);
	}

	void fireBullet() {
		//yield return new WaitForSeconds (.1);
		bulletHandler shot = ((GameObject)Instantiate (enemyBullet, barrelEnd.position, transform.rotation)).GetComponent<bulletHandler> ();//instanciating enemy bullet
		shot.isEnemyBull = true;
	}

	IEnumerator stopMoving () { //stopping the movement after a certain amount of time
		yield return new WaitForSeconds (5);
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
		else if (col.gameObject.tag == "barbedWire") {
			if (slowed == false) {
				if (col.gameObject.GetComponent<barbedWireHandler> ().prePlace == false) {
					moveSpeed = 0.5f;
					slowed = true;
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "barbedWire") {
			if (col.gameObject.GetComponent<barbedWireHandler> ().prePlace == false) {
				moveSpeed = 1;
				slowed = false;
			}
		}
	}
}
