using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Opens Level Select Screen
    public void OpenLevels()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Ends Application
    public void QuitGame()
    {
        Application.Quit();
    }
}
