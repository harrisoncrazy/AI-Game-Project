﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	//Info text
	public Text healthText;
	public Text killedText;

	//Upgrade stuffs
	public Text fireRateText;
	public Text upgradedText;
	public int upgradeCap = 10;
	private bool turretUpgraded = false;

	//Pause Screen toggles
	public GameObject pauseMain;
	private bool isPause= false;
	public GameObject insFundsText;

	//UI Weapon Toggles
	public GameObject rifleWep;
	public GameObject shotgunWep;
	public GameObject launcherWep;
	public GameObject rifleWep2;
	public GameObject shotgunWep2;
	public GameObject launcherWep2;

	//Upgrade Toggles
	public GameObject upgradeScreenTurret;
	private bool upgradeSwaped = false;
	public GameObject upgradeScreenAI;
	public Text upgradeText;

	//Stuff grabbed from player turret
	private float health;
	private Transform location;
	public float turretFireRate;

	//Left AI turret values
	public float fireRateL = 3;
	public float upgradeCapL = 15;
	public Text purchasedLText;
	public Text upgradedLText;

	//Right AI turret values
	public float fireRateR = 3;
	public float upgradeCapR = 15;
	public Text purchasedRText;
	public Text upgradedRText;

	public GameObject turret2;
	public GameObject turretAI;
	public Transform turretAILeft;
	public Transform turretAIRight;
	private bool isLeftSpawned = false;
	private bool isRightSpawned = false;

	//Game Over stuff
	public GameObject deathScreen;
	public Text timesurviedText;
	public Text enemyKilledText;
	public float timeSurvived;
	public int numKilled;


	// Use this for initialization
	void Start () {
		Instance = this;
		turretFireRate = turretHandler.Instance.bulletDelay;


		rifleWep = GameObject.Find ("rifleImage");
		shotgunWep = GameObject.Find ("shotgunImage");
		launcherWep = GameObject.Find ("launcherImage");
		rifleWep2 = GameObject.Find ("rifleImage2");
		shotgunWep2 = GameObject.Find ("shotgunImage2");
		launcherWep2 = GameObject.Find ("launcherImage2");

		rifleWep.SetActive (false);
		rifleWep2.SetActive (true);
		shotgunWep2.SetActive (false);
		launcherWep2.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		timeSurvived += Time.deltaTime;

		if (GameObject.FindGameObjectWithTag("Player") != null) {
			healthText.text = "Player Health: " + turretHandler.Instance.health;//Text box displaying player health
		}
		killedText.text = "Minions Killed: " + spawnEnemy.Instance.numberKilled;//Text box displaying minions killed

		if (Input.GetKeyDown (KeyCode.Escape)) {//Game pause function
			if (isPause == false) {
				Time.timeScale = 0;
				pauseMain.SetActive(true);
				isPause = true;
			} else {
				Time.timeScale = 1;
				pauseMain.SetActive(false);
				insFundsText.SetActive (false);
				isPause = false;
			}
		}
		if (isLeftSpawned == true) {
			purchasedLText.text = "Already Purchased!";
		}
		if (isRightSpawned == true) {
			purchasedRText.text = "Already Purchased!";
		}
	}

	public void upgradeFireRate() {
		if (upgradeCap >= 4) {
			if (spawnEnemy.Instance.numberKilled >= 10) {
				spawnEnemy.Instance.numberKilled -= 10;
				turretFireRate -= .1f;
				turretHandler.Instance.bulletDelay -= .1f;
				upgradeCap--;
				insFundsText.SetActive (false);
			} else {
				insFundsText.SetActive (true);
			}
		} else if (upgradeCap <= 4) {
			fireRateText.text = "Max Upgrade Reached!";
		}
	}

	public void upgradeTurretType(){//Upgrading to the Second turret Type
		if (turretUpgraded == false) {
			if (spawnEnemy.Instance.numberKilled >= 100) {
				spawnEnemy.Instance.numberKilled -= 100;
				health = turretHandler.Instance.health;
				location = turretHandler.Instance.transform;

				//Toggling all weapon images on
				rifleWep.SetActive (true);
				shotgunWep.SetActive (true);
				launcherWep.SetActive (true);
				rifleWep2.SetActive (true);
				shotgunWep2.SetActive (true);
				launcherWep2.SetActive (true);

				Destroy (GameObject.Find("turretMain")); //destroying old turret
				turretHandler turretSecondForm = ((GameObject)Instantiate (turret2, location.position, location.rotation)).GetComponent<turretHandler> (); //instanciating new turret
				turretSecondForm.bulletDelay = turretFireRate; //setting new turret values equal to old values
				turretSecondForm.health = health;
				turretSecondForm.wepSelect = 1;
				turretSecondForm.rifleWep = rifleWep;
				turretSecondForm.shotgunWep = shotgunWep;
				turretSecondForm.launcherWep = launcherWep;
				turretSecondForm.rifleWep2 = rifleWep2;
				turretSecondForm.shotgunWep2 = shotgunWep2;
				turretSecondForm.launcherWep2 = launcherWep2;

				//reseting weapon highlight to rifle config
				rifleWep.SetActive (false);
				rifleWep2.SetActive (true);
				shotgunWep2.SetActive (false);
				launcherWep2.SetActive (false);

				turretUpgraded = true;
			}
		} else if (turretUpgraded == true) {
			upgradedText.text = "Turret already upgraded!";
		}
	}

	public void switchPauseMenu() {//Switching the upgrade menues
		if (isPause == true) {
			if (upgradeSwaped == false) {
				upgradeSwaped = true;
				upgradeScreenTurret.SetActive (false);
				upgradeScreenAI.SetActive (true);
				upgradeText.text = "Turret Upgrades";
			} else if (upgradeSwaped == true) {
				upgradeSwaped = false;
				upgradeScreenTurret.SetActive (true);
				upgradeScreenAI.SetActive (false);
				upgradeText.text = "AI Turret Upgrades";
			}
		}
	}

	public void spawnLeftAITurret() { //Making left AI turret
		if (isLeftSpawned == false) {
			if (spawnEnemy.Instance.numberKilled >= 100) {
				spawnEnemy.Instance.numberKilled -= 100;
				AITurretHandler AITurret = ((GameObject)Instantiate (turretAI, turretAILeft.position, turretAILeft.rotation)).GetComponent<AITurretHandler> ();
				isLeftSpawned = true;
			}
		} else {
			
		}
	}

	public void upgradeLeftAITurret() { //Upgrading left AI turret
		if (upgradeCapL >= 6) {
			if (spawnEnemy.Instance.numberKilled >= 10) {
				spawnEnemy.Instance.numberKilled -= 10;
				fireRateL -= .2f;
				upgradeCapL--;
				insFundsText.SetActive (false);
			}
		} else {
			upgradedLText.text = "Max Upgrade Reached!";
		}
	}

	public void spawnRightAITurret() { //Making right AI turret
		if (isRightSpawned == false) {
			if (spawnEnemy.Instance.numberKilled >= 100) {
				spawnEnemy.Instance.numberKilled -= 100;
				AITurretHandler AITurret = ((GameObject)Instantiate (turretAI, turretAIRight.position, turretAIRight.rotation)).GetComponent<AITurretHandler> ();
				isRightSpawned = true;
			}
		} else {
			
		}
	}

	public void upgradeRightAITurret() { //Upgrading right AI turret
		if (upgradeCapR >= 6) {
			if (spawnEnemy.Instance.numberKilled >= 10) {
				spawnEnemy.Instance.numberKilled -= 10;
				fireRateR -= .2f;
				upgradeCapR--;
				insFundsText.SetActive (false);
			}
		} else {
			upgradedRText.text = "Max Upgrade Reached!";
		}
	}

	public void PlayerDead() {
		deathScreen.SetActive (true);
		Time.timeScale = 0;
		int roundedTime = (Mathf.RoundToInt (timeSurvived)/60);

		timesurviedText.text = "You survived for: " + roundedTime + " minute(s).";
		enemyKilledText.text = "You Killed: " + numKilled + " enemies.";
	}
}