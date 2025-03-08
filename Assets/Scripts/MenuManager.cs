using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPauseMenuOpen = false;
    [SerializeField] GameObject gearMenu;
    private bool isGearMenuOpen = false;

    private void ToggleTime(bool isPaused)
    {
        Time.timeScale = isPaused ? 0.0f : 1.0f;
    }

    public void TogglePauseMenu()
    {
        //Closes GearMenu if it is open
        if (isGearMenuOpen) { ToggleGearMenu(); }

        isPauseMenuOpen = !isPauseMenuOpen;
        pauseMenu.SetActive(isPauseMenuOpen);
        ToggleTime(isPauseMenuOpen);
    }

    public void ToggleGearMenu()
    {
        //Does not allow GearMenu to open when PauseMenu is open
        if (isPauseMenuOpen) { return; }

        isGearMenuOpen = !isGearMenuOpen;
        gearMenu.SetActive(isGearMenuOpen);
        ToggleTime(isGearMenuOpen);
    }
}
