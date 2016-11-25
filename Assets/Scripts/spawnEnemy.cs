using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnEnemy : MonoBehaviour {

	public static spawnEnemy Instance;

	public Vector2 spawnPos;

	//Minion Stuff
	public GameObject minonEnemy;
	public float timerSet = 1f;
	private float realTimer;
	public float maxEnemies = 10;
	public Transform player;
	public float spawnNumber = 0;
	public float numberKilled = 0;

	//LieutenantStuff
	public GameObject lieutenantEnemy;
	public Transform lieutenantSpawn1;
	public Transform lieutenantSpawn2;
	public Transform lieutenantSpawn3;
	public Transform lieutenantSpawn4;
	public Transform lieutenantSpawn5;
	public Transform lieutenantSpawn6;
	public bool Lspawn1Disabled = false;
	public bool Lspawn2Disabled = false;
	public bool Lspawn3Disabled = false;
	public bool Lspawn4Disabled = false;
	public bool Lspawn5Disabled = false;
	public bool Lspawn6Disabled = false;

	private Transform LspawnPos;
	public Transform lieutenantRotPoint;
	public float LtimerSet = 5f;
	private float realLTimer;
	public float maxLieutenants = 4;
	public float spawnedLNum = 0;
	private int spawnedPos;

	//Boss Stuff
	public GameObject bossEnemy;
	public bool isBossSpawned = false;
	public float bossTimerSet = 15f;
	private float realBossTimer;


	private bool checkfornewPlayer;

	public List <Transform> lieutenantPoints = new List<Transform>();

	// Use this for initialization
	void Start () {
		Instance = this;
		player = GameObject.FindWithTag ("Player").transform;
		realTimer = timerSet;
		realLTimer = LtimerSet;
		realBossTimer = bossTimerSet;
		//adding lieutenant Spawn points to an array
		lieutenantPoints [0] = lieutenantSpawn1;
		lieutenantPoints [1] = lieutenantSpawn2;
		lieutenantPoints [2] = lieutenantSpawn3;
		lieutenantPoints [3] = lieutenantSpawn4;
		lieutenantPoints [4] = lieutenantSpawn5;
		lieutenantPoints [5] = lieutenantSpawn6;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			if (checkfornewPlayer == false) {
				checkfornewPlayer = true;
				player = GameObject.FindWithTag ("Player").transform;
			}
		}
		if (player != null) {
			//MINION SPAWNING
			spawnPos = new Vector2 (Random.Range (-7.5f, 7.5f), 5.5f);//randomizing the x spawn point
			if (spawnNumber <= maxEnemies) {//spawn restriction
				realTimer -= Time.deltaTime;
				if (realTimer < 0) {
					SpawnMinionGuy ();
					spawnNumber++;
					realTimer = timerSet;
				}
			}
			//Lieutenant Spawning
			if (spawnedLNum < maxLieutenants) {//spawn restriction
				realLTimer -= Time.deltaTime;
				if (realLTimer < 0) {
					SpawnLieutenantGuy ();
					spawnedLNum++;
					realLTimer = LtimerSet;
				}
			}

			//Boss spawning
			if (isBossSpawned == false) {
				realBossTimer -= Time.deltaTime;
				if (realBossTimer < 0) {
					SpawnBossGuy ();
					isBossSpawned = true;
					realBossTimer = bossTimerSet;
				}
			}
		}
	}

	void SpawnMinionGuy() {//spawning minion
		if (player != null) {
			minonHandler minionPrefab = ((GameObject)Instantiate (minonEnemy, spawnPos, player.rotation)).GetComponent<minonHandler> ();
			if (spawnPos.x >= 6.5f || spawnPos.x <= -6.5f) {
				minionPrefab.isOutOfBounds = true;//making the minion move down the right or left of the screen if it is in the right position
			}

		}
	}

	void SpawnLieutenantGuy() {//spawning lieutenants
		if (player != null) {
			top:
			int rando = Random.Range (0, 7); //randomly selecting a spawn point for the lieutenant, then disabling that spawn point until the lietunant is killed
			if (rando == 1 && Lspawn1Disabled == false) {
				LspawnPos = lieutenantSpawn1;
				spawnedPos = 1;
			} else if (rando == 2 && Lspawn2Disabled == false) {
				LspawnPos = lieutenantSpawn2;
				spawnedPos = 2;
			} else if (rando == 3 && Lspawn3Disabled == false) {
				LspawnPos = lieutenantSpawn3;
				spawnedPos = 3;
			} else if (rando == 4 && Lspawn4Disabled == false) {
				LspawnPos = lieutenantSpawn4;
				spawnedPos = 4;
			} else if (rando == 5 && Lspawn5Disabled == false) {
				LspawnPos = lieutenantSpawn5;
				spawnedPos = 5;
			} else if (rando == 6 && Lspawn6Disabled == false) {
				LspawnPos = lieutenantSpawn6;
				spawnedPos = 6;
			} else {
				goto top;
			}

			lieutenantHandler lieutenantPrefab = ((GameObject)Instantiate (lieutenantEnemy, LspawnPos.position, lieutenantRotPoint.rotation)).GetComponent<lieutenantHandler> ();
			lieutenantPrefab.spawnedPos = spawnedPos;
			lieutenantPrefab.name = "lieutenantEnemy";
		}
	}

	void SpawnBossGuy() {
		bossHandler bossPrefab = ((GameObject)Instantiate (bossEnemy, new Vector3 (0, 7.5f, 0), player.rotation)).GetComponent<bossHandler> ();
	}
}
