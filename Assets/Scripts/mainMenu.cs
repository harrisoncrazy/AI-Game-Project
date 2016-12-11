using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	[SerializeField] private string levelName;

	public GameObject Main;
	public GameObject HowTo;
	public GameObject Credits;
	public bool howToOpen = false;
	public bool creditsOpen = false;

	// changes to Level scene
	public void LoadLevel () {
		SceneManager.LoadScene(levelName);
		Time.timeScale = 1;
	}

	public void HowToPlay () {
		if (howToOpen == false) {
			Main.SetActive(false);
			HowTo.SetActive (true);
			Credits.SetActive (false);
			howToOpen = true;
		} else if (howToOpen == true) {
			Main.SetActive(true);
			HowTo.SetActive (false);
			Credits.SetActive (false);
			howToOpen = false;
		}
	}

	public void CreditsOpen () {
		if (creditsOpen == false) {
			Main.SetActive(false);
			HowTo.SetActive (false);
			Credits.SetActive (true);
			creditsOpen = true;
		} else if (creditsOpen == true) {
			Main.SetActive(true);
			HowTo.SetActive (false);
			Credits.SetActive (false);
			creditsOpen = false;
		}
	}

	public void Exit() {
		Time.timeScale = 1;
		Application.Quit ();
	}
}