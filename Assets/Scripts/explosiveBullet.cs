using UnityEngine;
using System.Collections;

public class explosiveBullet : MonoBehaviour {

	public GameObject bullet;
	public float bulletSpeed = 5;
	public float explosionTime = 1f;

	public bool isEnemy = false;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Explode");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * Time.deltaTime * bulletSpeed;//moving the bullet forward regardless of rotation
	}

	IEnumerator Explode() {
		yield return new WaitForSeconds (explosionTime);

		bulletHandler shotNeg7 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, 180))).GetComponent<bulletHandler> ();
		shotNeg7.destroyTime = 0.5f;
		bulletHandler shotNeg6 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, 150))).GetComponent<bulletHandler> ();
		shotNeg6.destroyTime = 0.5f;
		bulletHandler shotNeg5 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, 120))).GetComponent<bulletHandler> ();
		shotNeg5.destroyTime = 0.5f;
		bulletHandler shotNeg4 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90))).GetComponent<bulletHandler> ();
		shotNeg4.destroyTime = 0.5f;
		bulletHandler shotNeg3 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, 60))).GetComponent<bulletHandler> ();
		shotNeg3.destroyTime = 0.5f;
		bulletHandler shotNeg2 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, 30))).GetComponent<bulletHandler> ();
		shotNeg2.destroyTime = 0.5f;
		bulletHandler shot = ((GameObject)Instantiate (bullet, transform.position, transform.rotation)).GetComponent<bulletHandler> ();;
		shot.destroyTime = 0.5f;
		bulletHandler shotPos2 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, -30))).GetComponent<bulletHandler> ();
		shotPos2.destroyTime = 0.5f;
		bulletHandler shotPos3 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, -60))).GetComponent<bulletHandler> ();
		shotPos3.destroyTime = 0.5f;
		bulletHandler shotPos4 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, -90))).GetComponent<bulletHandler> ();
		shotPos4.destroyTime = 0.5f;
		bulletHandler shotPos5 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, -120))).GetComponent<bulletHandler> ();
		shotPos5.destroyTime = 0.5f;
		bulletHandler shotPos6 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, -150))).GetComponent<bulletHandler> ();
		shotPos6.destroyTime = 0.5f;
		bulletHandler shotPos7 = ((GameObject)Instantiate (bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, -180))).GetComponent<bulletHandler> ();
		shotPos7.destroyTime = 0.5f;

		if (isEnemy == true) {
			shotNeg7.isEnemyBull = true;
			shotNeg6.isEnemyBull = true;
			shotNeg5.isEnemyBull = true;
			shotNeg4.isEnemyBull = true;
			shotNeg3.isEnemyBull = true;
			shotNeg2.isEnemyBull = true;
			shot.isEnemyBull = true;
			shotPos2.isEnemyBull = true;
			shotPos3.isEnemyBull = true;
			shotPos4.isEnemyBull = true;
			shotPos5.isEnemyBull = true;
			shotPos6.isEnemyBull = true;
			shotPos7.isEnemyBull = true;
		}

		Destroy (gameObject);
	}
}
