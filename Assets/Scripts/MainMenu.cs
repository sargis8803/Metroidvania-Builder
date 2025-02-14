using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //need to add display settings button functions: window and full-screen
    /*other possible settings implementations for the future: 
     volume controls
     key binds 
     gamepad support?
     tbd 
    */
    
    public void windowMode()
    {
        Screen.fullScreen = false;
    }

    public void fullScreen()
    {
        Screen.fullScreen = true;
    }

     public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(0);
        }
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
