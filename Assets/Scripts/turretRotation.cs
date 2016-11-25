using UnityEngine;
using System.Collections;

public class turretRotation : MonoBehaviour {

	public float rotationSpeed = 250f;

	void Update ()
	{

		// Get mouse position in the world
		Vector3 mousePosition = Input.mousePosition;    
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

		// Rotate the turret towards the mouse location
		Quaternion targetRotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward );
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);           

		// Limit rotation to Z axis
		transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);

	}
}
