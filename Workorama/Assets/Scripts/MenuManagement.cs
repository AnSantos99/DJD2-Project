using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that manages the switches between scenes and menus
/// </summary>
public class MenuManagement : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;
    private bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    
    void Update()
    {
        Debug.Log("Am here");
        //ActivatePauseMenu();
    }

    /// <summary>
    /// Method for start button to start game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Prototype2");
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

    public void ActivatePauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            
            PauseGame();
        } 

        else ResumeGame();      
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
