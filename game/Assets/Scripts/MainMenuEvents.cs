using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour {
	public void StartGame() {
		SceneManager.LoadScene("MainGameScene");
	}
}
