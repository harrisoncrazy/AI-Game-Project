using UnityEngine;
using System.Collections;

public class barbedWireHandler : MonoBehaviour {

	public float rotationSpeed = 250f;
	private Transform playerLook;

	public bool prePlace = false;

	void Start() {
		playerLook = GameObject.FindGameObjectWithTag ("Blocker").transform;
	}

	void Update ()
	{
		transform.LookAt (transform.position + new Vector3 (0, 0, 1), playerLook.transform.position - transform.position);    
	}
}