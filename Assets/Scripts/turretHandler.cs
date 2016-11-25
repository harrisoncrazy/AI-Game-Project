using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class turretHandler : MonoBehaviour {

	public static turretHandler Instance;

	public GameObject bullet;
	public GameObject bulletShot;
	public GameObject explosive;
	public Transform barrelEnd;
	public Transform barrelEnd2;

	public float bulletDelay = 1f;
	private float bulletCountdown = 1f;
	private bool isBullDelayed = false;
	public bool upgradedGun = false;

	public float health = 100;
	private float speed = 3;

	//variable for weapon swapping
	public int wepSelect = 1;
	public GameObject rifleWep;
	public GameObject shotgunWep;
	public GameObject launcherWep;
	public GameObject rifleWep2;
	public GameObject shotgunWep2;
	public GameObject launcherWep2;


	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (isBullDelayed == true) {//bullet delay timer, once upgrades are added bullet delay will change according to upgrades
			BulletDelay();
		}
		if (Input.GetMouseButton (0) && isBullDelayed == false) {//checking if the bullet is not delayed
			if (upgradedGun != true) {//checking if gun is upgraded to the 2 barrel config
				if (barrelEnd != null) {
					if (wepSelect == 1) { //Firing Rifle weapon
						GameObject shot = GameObject.Instantiate (bullet, barrelEnd.position, barrelEnd.rotation) as GameObject;
						isBullDelayed = true;
					} else if (wepSelect == 2) { // Firing Shotgun Weapon
						GameObject shotNeg3 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, 25)) as GameObject;
						GameObject shotNeg2 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, 15)) as GameObject;
						GameObject shotNeg1 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, 5)) as GameObject;
						GameObject shotPos3 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, -5)) as GameObject;
						GameObject shotPos2 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, -15)) as GameObject;
						GameObject shotPos1 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, -25)) as GameObject;
						isBullDelayed = true;
					} else if (wepSelect == 3) { //Firing Explosive Launcher Weapon
						GameObject explo = GameObject.Instantiate (explosive, barrelEnd.position, barrelEnd.rotation) as GameObject;
						isBullDelayed = true;
					}
				}
			} else if (upgradedGun == true) {//if it is a 2 barrel config, it will fire 2 bullets instead
				if (wepSelect == 1) {//Firing Rifle weapon
					GameObject shot = GameObject.Instantiate (bullet, barrelEnd.position, barrelEnd.rotation) as GameObject;
					GameObject shot2 = GameObject.Instantiate (bullet, barrelEnd2.position, barrelEnd2.rotation) as GameObject;
					isBullDelayed = true;
				} else if (wepSelect == 2) {// Firing Shotgun Weapon
					GameObject shotNeg3 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, 25)) as GameObject;
					GameObject shotNeg2 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, 15)) as GameObject;
					GameObject shotNeg1 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, 5)) as GameObject;
					GameObject shotPos3 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, -5)) as GameObject;
					//GameObject shotPos2 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, -15)) as GameObject;
					//GameObject shotPos1 = GameObject.Instantiate (bulletShot, barrelEnd.position, barrelEnd.rotation * Quaternion.Euler(0, 0, -25)) as GameObject;

					//GameObject shot2Neg3 = GameObject.Instantiate (bulletShot, barrelEnd2.position, barrelEnd2.rotation * Quaternion.Euler(0, 0, 25)) as GameObject;
					//GameObject shot2Neg2 = GameObject.Instantiate (bulletShot, barrelEnd2.position, barrelEnd2.rotation * Quaternion.Euler(0, 0, 15)) as GameObject;
					GameObject shot2Neg1 = GameObject.Instantiate (bulletShot, barrelEnd2.position, barrelEnd2.rotation * Quaternion.Euler(0, 0, 5)) as GameObject;
					GameObject shot2Pos3 = GameObject.Instantiate (bulletShot, barrelEnd2.position, barrelEnd2.rotation * Quaternion.Euler(0, 0, -5)) as GameObject;
					GameObject shot2Pos2 = GameObject.Instantiate (bulletShot, barrelEnd2.position, barrelEnd2.rotation * Quaternion.Euler(0, 0, -15)) as GameObject;
					GameObject shot2Pos1 = GameObject.Instantiate (bulletShot, barrelEnd2.position, barrelEnd2.rotation * Quaternion.Euler(0, 0, -25)) as GameObject;
					isBullDelayed = true;
				} else if (wepSelect == 3) {//Firing Explosive Launcher Weapon
					GameObject explo = GameObject.Instantiate (explosive, barrelEnd.position, barrelEnd.rotation) as GameObject;
					GameObject explo2 = GameObject.Instantiate (explosive, barrelEnd2.position, barrelEnd2.rotation) as GameObject;
					isBullDelayed = true;
				}


			}
		}
		if (Input.GetKey ("1")) { //Switching to weapon 1
			RifleWeaponSelected ();
		}
		if (Input.GetKey ("2")) { //Switching to weapon 2
			ShotgunWeaponSelected ();
		}
		if (Input.GetKey ("3")) { //Switching to weapon 3
			LauncherWeaponSelected ();
		}
		if (Input.GetKey ("d")) {//moving right restricted by bounds
			if (transform.position.x <= 2.5) {
				transform.position = new Vector3 (transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
			}
		}
		if (Input.GetKey ("a")) {//moving left restricted by bounds
			if (transform.position.x >= -2.5) {
				transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
			}
		}
		if (health <= 0) {//once player health is zero, it will get destroyed
			GameManager.Instance.PlayerDead();
			Time.timeScale = 0;
			Destroy (this.gameObject);
		}
	}

	void BulletDelay() {
		bulletCountdown -= Time.deltaTime;
		if (bulletCountdown < 0) {
			isBullDelayed = false;
			if (wepSelect == 1) { //Adding different lengths of time to the bullet delay for different weapon types
				bulletCountdown = bulletDelay;
			} else if (wepSelect == 2) {
				bulletCountdown = bulletDelay * 2f; 
			} else if (wepSelect == 3) {
				bulletCountdown = bulletDelay * 4f;
			}
		}
	}

	void RifleWeaponSelected() {
		wepSelect = 1;
		rifleWep.SetActive (false);
		shotgunWep.SetActive (true);
		launcherWep.SetActive (true);
		rifleWep2.SetActive (true);
		shotgunWep2.SetActive (false);
		launcherWep2.SetActive (false);
	}

	void ShotgunWeaponSelected() {
		wepSelect = 2;
		rifleWep.SetActive (true);
		shotgunWep.SetActive (false);
		launcherWep.SetActive (true);
		rifleWep2.SetActive (false);
		shotgunWep2.SetActive (true);
		launcherWep2.SetActive (false);
	}

	void LauncherWeaponSelected() {
		wepSelect = 3;
		rifleWep.SetActive (true);
		shotgunWep.SetActive (true);
		launcherWep.SetActive (false);
		rifleWep2.SetActive (false);
		shotgunWep2.SetActive (false);
		launcherWep2.SetActive (true);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "EnemyBullet") {//getting hit by enemy bullets
			health -= 10;
		}
	}
}
