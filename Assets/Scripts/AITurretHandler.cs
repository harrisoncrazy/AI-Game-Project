using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AITurretHandler : MonoBehaviour {

	public float timer;
	private float delay;
	public GameObject bullet;

	public Transform barrelEnd;

	public bool isLeft;

	void Start() {
		delay = timer;
	}

	void Update () {
		if (isLeft == true) {//checking if it is the left or right turret
			timer = GameManager.Instance.fireRateL;
		} else if (isLeft == false) {
			timer = GameManager.Instance.fireRateR;
		}

		var closestGameObject = GameObject.FindGameObjectsWithTag ("Enemy") //finding closest tagged enemy
			.OrderBy(go => Vector3.Distance(go.transform.position, transform.position))
				.FirstOrDefault();
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
