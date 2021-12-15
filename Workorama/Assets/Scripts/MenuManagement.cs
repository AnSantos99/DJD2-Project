using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that manages the switches between scenes
/// </summary>
public class MenuManagement : MonoBehaviour
{
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
        Debug.Log("Closing game");
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
}
