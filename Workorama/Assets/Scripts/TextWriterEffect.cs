using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriterEffect : MonoBehaviour
{
    [SerializeField]
    private float delayBetweenChars;

    private Text fullText;

    private string charWriter;


    // Start is called before the first frame update
    private void Start()
    {
        
        fullText = GetComponent<Text>();

        charWriter = fullText.text;

        fullText.text = "";

        TextVerification();

        
    }

    private void TextVerification() 
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            if (fullText != null)
            {
                charWriter = fullText.text;
                fullText.text = "";
                StartCoroutine(DisplayText());
            }
        }
        
    }


    IEnumerator DisplayText() 
    {
        foreach (char c in charWriter)
        {
            if (fullText.text.Length > 0)
                fullText.text = fullText.text.Substring(0, fullText.text.Length);

            fullText.text += c;

            yield return new WaitForSeconds(delayBetweenChars);

            Debug.Log(c);
        }
    }
}
