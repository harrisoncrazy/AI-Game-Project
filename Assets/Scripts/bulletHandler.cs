using UnityEngine;
using System.Collections;

public class bulletHandler : MonoBehaviour {

	public float bulletSpeed = 5;

	public bool isEnemyBull = false;

	public float destroyTime = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * Time.deltaTime * bulletSpeed;//moving the bullet forward regardless of rotation
		if (isEnemyBull == true) {
			this.gameObject.tag = "EnemyBullet";//tagging bullet as an enemy if it is fired by a minion
		}
		StartCoroutine ("DestroySelf");//destroying self after a while
	}

	IEnumerator DestroySelf(){
		yield return new WaitForSeconds (destroyTime);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Enemy" && isEnemyBull != true) {//killing an enemy minion
			spawnEnemy.Instance.spawnNumber--;
			spawnEnemy.Instance.numberKilled++;
			GameManager.Instance.numKilled++;
			Destroy (this.gameObject);
		} else if (col.gameObject.name == "lieutenantEnemy" && isEnemyBull != true) {
			spawnEnemy.Instance.spawnedLNum--;
			spawnEnemy.Instance.numberKilled += 10;
			GameManager.Instance.numKilled++;
			Destroy (this.gameObject);
		} else if (col.gameObject.tag == "bossEnemy" && isEnemyBull != true) {
			//spawnEnemy.Instance.isBossSpawned = false;
			//spawnEnemy.Instance.numberKilled += 50;
			//GameManager.Instance.numKilled++;
			Destroy (this.gameObject);
		} else if (col.gameObject.tag == "Player") {//enemy bullets hitting the player
			if (isEnemyBull == true) {
				Destroy (this.gameObject);
			}
		} else if (col.gameObject.tag == "Wall") {//bullets hitting the wall
			Destroy (this.gameObject);
		}
	}
}
