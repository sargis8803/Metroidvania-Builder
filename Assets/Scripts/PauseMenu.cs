using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;

    public void MainMenu()
    {

        SceneManager.LoadScene("Main-Menu");
        Time.timeScale = 1;
    }

    public void Buildmenu()
    {
        SceneManager.LoadScene("Build-Menu");
        Time.timeScale = 0;
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager.TogglePauseMenu();
        }
    }


}
