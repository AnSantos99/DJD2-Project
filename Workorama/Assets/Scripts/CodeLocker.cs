using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeLocker : MonoBehaviour
{
    private int codeLength;
    private int placeInCode;

    [SerializeField] private string code;
    [SerializeField] private string attemptedCode;

    private Animator animator;

    [SerializeField] private GameObject WrongPanel;
    [SerializeField] private GameObject RightPanel;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        codeLength = code.Length;
    }

    private void CheckCode()
    {
        if (attemptedCode == code)
        {
            StartCoroutine(ShowRightInputMessage());


            //What does it do when the input is right
        }

        else
        {
            StartCoroutine(ShowWrongInputMessage());
        }
    }

    IEnumerator ShowWrongInputMessage()
    {
        WrongPanel.SetActive(true);

        yield return new WaitForSeconds(2);

        WrongPanel.SetActive(false); ;
    }

    IEnumerator ShowRightInputMessage()
    {
        RightPanel.SetActive(true);

        yield return new WaitForSeconds(2);

        RightPanel.SetActive(false);

        Destroy(this);
    }

    public void SetValue(string value)
    {
        placeInCode++;

        if (placeInCode <= codeLength)
        {
            attemptedCode += value;
        }

        if (placeInCode == codeLength)
        {
            CheckCode();

            attemptedCode = "";
            placeInCode = 0;
        }
    }
}
