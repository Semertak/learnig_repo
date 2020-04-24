using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeEvents : MonoBehaviour
{
    public void StartGame(int level_number){
        SceneManager.LoadScene(string.Format("Level_{0}", level_number));
    }
}
