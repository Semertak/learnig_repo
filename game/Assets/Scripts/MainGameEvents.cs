using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameEvents : MonoBehaviour {
	public void SwitchToMainMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}
}
