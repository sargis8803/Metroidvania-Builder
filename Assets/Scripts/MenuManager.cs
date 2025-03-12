using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject gearMenu;

    private void ToggleTime(bool isPaused)
    {
        Time.timeScale = isPaused ? 0.0f : 1.0f;
    }

    public void TogglePauseMenu()
    {
        //Closes GearMenu if it is open
        if (gearMenu.activeSelf) { ToggleGearMenu(); }

        bool isPauseMenuOpen = pauseMenu.activeSelf;
        pauseMenu.SetActive(!isPauseMenuOpen);
        ToggleTime(!isPauseMenuOpen);
    }

    public void ToggleGearMenu()
    {
        //Does not allow GearMenu to open when PauseMenu is open
        if (pauseMenu.activeSelf) { return; }

        bool isGearMenuOpen = gearMenu.activeSelf;
        gearMenu.SetActive(!isGearMenuOpen);
        ToggleTime(!isGearMenuOpen);
    }
}
