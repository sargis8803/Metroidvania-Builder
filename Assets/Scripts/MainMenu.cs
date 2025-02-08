using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject menu; //need to link these later to the actual scenes

     public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (SettingsMenu.activeSelf)
            {
                closeSettings();
            }
        }
    }

    public void closeSettings()
    {
        SettingsMenu.SetActive(false);
        menu.SetActive(true);
    }

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
