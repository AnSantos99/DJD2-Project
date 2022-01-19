using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that manages the switches between scenes and menus
/// </summary>
public class MenuManagement : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused;

    private void Start()
    {
        HidePauseMenu();
    }

    private void Update()
    {
        ActivatePauseMenu();
    }

    /// <summary>
    /// To hide the pause menu panel
    /// </summary>
    public void HidePauseMenu() => pauseMenu.SetActive(false);

    /// <summary>
    /// Method for start button to start game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("KeypadFix");
    }

    /// <summary>
    /// Method for quit button
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Method for Options button menu
    /// </summary>
    public void ShowOptions()
    {
        // To be implemented
        Debug.Log("Options menu");
    }

    /// <summary>
    /// Method that load the main menu scene of the game
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    /// <summary>
    /// Method that activates pause menu on key press
    /// </summary>
    public void ActivatePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            if(isPaused) ResumeGame();

            else PauseGame();
        } 
    }

    /// <summary>
    /// Pause game and "freeze" time
    /// </summary>
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    /// <summary>
    /// Resume game and "unfreeze" time
    /// </summary>
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
