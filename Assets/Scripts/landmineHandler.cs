using UnityEngine;
using System.Collections;

public class landmineHandler : MonoBehaviour {

	public GameObject explosive;

	public bool prePlace = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (prePlace == false) {
			if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "lieutenantEnemy" || col.gameObject.tag == "bossEnemy") { //if the minion reaches the firing position
				explosiveBullet shot = ((GameObject)Instantiate (explosive, transform.position, transform.rotation)).GetComponent<explosiveBullet> ();//instanciating enemy bullet
				shot.explosionTime = 0;
				Destroy (this.gameObject);
			}
		}
	}
}
