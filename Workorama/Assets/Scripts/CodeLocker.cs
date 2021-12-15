using UnityEngine;

public class CodeLocker : MonoBehaviour
{
    private int codeLength;     //The required code
    private int placeInCode;    //The player's input

    [SerializeField] private string code;
    [SerializeField] private string attemptedCode;

    [SerializeField]private Animator doorAnim;
    private Animator animator;



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
            animator.SetTrigger("Right");

            doorAnim.SetTrigger("Interact");

            //What does it do when the input is right
        }

        else
        {
            animator.SetTrigger("Wrong");
        }
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
