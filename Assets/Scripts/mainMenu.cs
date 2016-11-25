using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	[SerializeField] private string levelName;

	public GameObject Main;
	public GameObject HowTo;
	public bool howToOpen = false;

	// changes to Level scene
	public void LoadLevel () {
		SceneManager.LoadScene(levelName);
		Time.timeScale = 1;
	}

	public void HowToPlay () {
		if (howToOpen == false) {
			Main.SetActive(false);
			HowTo.SetActive (true);
			howToOpen = true;
		} else if (howToOpen == true) {
			Main.SetActive(true);
			HowTo.SetActive (false);
			howToOpen = false;
		}
	}

	public void Exit() {
		Time.timeScale = 1;
		Application.Quit ();
	}
}