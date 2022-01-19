using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameEffect : MonoBehaviour
{
    [SerializeField]private Image fadeImg;
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

        for (float i = 0; i <= 1.5f; i += Time.deltaTime)
        {
            // set color with i as alpha
            fadeImg.color = new Color(0, 0, 0, i);
            yield return null;
        }

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);

        UnlockCursor();

        SceneManager.LoadScene("Endgame");
    }
}
