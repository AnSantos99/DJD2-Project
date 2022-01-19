using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that manages the switches between scenes and menus
/// </summary>
public class MenuManagement : MonoBehaviour
{
    /// <summary>
    /// Method for start button to start game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
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
}
