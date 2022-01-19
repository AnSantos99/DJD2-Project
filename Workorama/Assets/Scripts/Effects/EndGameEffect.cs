using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameEffect : MonoBehaviour
{
    /// <summary>
    /// Unlock cursor
    /// </summary>
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForEnd());
        }
    }

    /// <summary>
    /// Wait a few second before disabling a object
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(3.5f);

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);

        UnlockCursor();

        SceneManager.LoadScene("Endgame");
    }
}
