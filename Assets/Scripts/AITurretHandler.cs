using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AITurretHandler : MonoBehaviour {

	public GameObject closestGameObject;

	public float timer;
	private float delay;
	public GameObject bullet;

	public Transform barrelEnd;

	public bool isLeft;

	//fire select bools
	public bool defaultMode = true;
	public bool bossMode = false;
	public bool lieutenantMode = false;

	void Start() {
		delay = timer;
	}

	void Update () {
		if (isLeft == true) {//checking if it is the left or right turret
			timer = GameManager.Instance.fireRateL;
		} else if (isLeft == false) {
			timer = GameManager.Instance.fireRateR;
		}

		if (defaultMode == true) {
			closestGameObject = GameObject.FindGameObjectsWithTag ("Enemy").OrderBy (go => Vector3.Distance (go.transform.position, transform.position)).FirstOrDefault (); //finding closest tagged enemy
		}
		if (bossMode == true) {
			closestGameObject = GameObject.FindGameObjectsWithTag ("bossEnemy").OrderBy (go => Vector3.Distance (go.transform.position, transform.position)).FirstOrDefault (); //finding closest tagged enemy
		}
		if (lieutenantMode == true) {
			closestGameObject = GameObject.FindGameObjectsWithTag ("lieutenantEnemy").OrderBy (go => Vector3.Distance (go.transform.position, transform.position)).FirstOrDefault (); //finding closest tagged enemy
		}


		if (closestGameObject != null) {
			transform.LookAt (transform.position + new Vector3 (0, 0, 1), closestGameObject.transform.position - transform.position); //looking at closest enemy
			delay -= Time.deltaTime;//firing bullet timer
			if (delay < 0) {
				fireBullet ();
				delay = timer;
			}
		}
	}

	void fireBullet () {
		bulletHandler shot = ((GameObject)Instantiate (bullet, barrelEnd.position, transform.rotation)).GetComponent<bulletHandler> ();//firing bullet
	}
}
