using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MenuManagerTests
{
    private MenuManager menuManager;
    private GameObject pauseMenu;
    private GameObject gearMenu;

    [SetUp]
    public void SetUp()
    {
        menuManager = new GameObject().AddComponent<MenuManager>();

        pauseMenu = new GameObject();
        menuManager.pauseMenu = pauseMenu;
        gearMenu = new GameObject();
        menuManager.gearMenu = gearMenu;

        pauseMenu.SetActive(false);
        gearMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    [TearDown]
    public void TearDown()
    {
        pauseMenu.SetActive(false);
        gearMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }


    // Black box unit test
    // Unit being the function TogglePauseMenu
    [Test]
    public void TogglePauseMenu_SuccessfulToggle()
    {
        // Case 1: From closed to open, with gear open.
        pauseMenu.SetActive(false);
        gearMenu.SetActive(true);
        menuManager.TogglePauseMenu();

        Assert.IsTrue(pauseMenu.activeSelf, "Pause menu should be open after toggle from closed to open (Case 1)");

        Assert.IsFalse(gearMenu.activeSelf, "Gear menu should not be open after toggle while open (Case 1)");

        Assert.AreEqual(0.0f, Time.timeScale, "Time should be stopped (0.0f) when toggled open (Case 1)");

        // Case 2: From closed to open, with gear closed, no need to check if gear is closed here but did it anyway.
        pauseMenu.SetActive(false);
        gearMenu.SetActive(false);
        menuManager.TogglePauseMenu();

        Assert.IsTrue(pauseMenu.activeSelf, "Pause menu should be open after toggle from closed to open (Case 2)");

        Assert.IsFalse(gearMenu.activeSelf, "Gear menu should remain closed after toggle (Case 2)");         //probably redundant

        Assert.AreEqual(0.0f, Time.timeScale, "Time should be stopped (0.0f) when toggled open (Case 2)");

        // Case 3: From open to closed, with gear closed, no need to check if gear is closed here but did it anyway.
        pauseMenu.SetActive(true);
        gearMenu.SetActive(false);
        menuManager.TogglePauseMenu();

        Assert.IsFalse(pauseMenu.activeSelf, "Pause menu should not be open after toggle (Case 3)");

        Assert.IsFalse(gearMenu.activeSelf, "Gear menu should remain closed after toggle (Case 3)");         //probably redundant

        Assert.AreEqual(1.0f, Time.timeScale, "Time should be normal (1.0f) when toggled closed (Case 3)");

        // Case 4: From open to close, with gear open, state should be impossible as when pauseMenu is opened gearMenu is forced to close so no need to test.
    }
}