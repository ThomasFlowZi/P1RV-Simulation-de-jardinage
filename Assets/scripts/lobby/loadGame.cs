using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("FarmingLAnds");
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("Lobby");
    }
}
