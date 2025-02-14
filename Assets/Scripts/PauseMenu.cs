using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false;

    public void MainMenu()
    {

        SceneManager.LoadScene("Main-Menu");
        Time.timeScale = 1;
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            if (isPaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
}
