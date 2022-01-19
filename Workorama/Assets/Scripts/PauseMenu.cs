using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused;

    // Start is called before the first frame update
    private void Start()
    {
        HidePauseMenu();
    }

    // Update is called once per frame
    private void Update()
    {
        ActivatePauseMenu();
        SetTimeFreezer();
    }

    private void SetTimeFreezer() 
    {
        if (!isPaused)
            Time.timeScale = 1f;
    }

    /// <summary>
    /// To hide the pause menu panel
    /// </summary>
    public void HidePauseMenu() => pauseMenu.SetActive(false);

    /// <summary>
    /// Method that activates pause menu on key press
    /// </summary>
    public void ActivatePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) ResumeGame();

            else PauseGame();
        }
    }

    /// <summary>
    /// Pause game and "freeze" time
    /// </summary>
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        UnlockCursor();
    }

    /// <summary>
    /// Resume game and "unfreeze" time
    /// </summary>
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        LockCursor();
    }

    /// <summary>
    /// UnlockCursor to be visible on screen
    /// </summary>
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Lockcursor in game
    /// </summary>
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
