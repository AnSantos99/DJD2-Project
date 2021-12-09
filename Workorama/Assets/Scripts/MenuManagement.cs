using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PedroScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowOptions()
    {
        
    }
}
