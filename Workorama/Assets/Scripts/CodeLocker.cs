using UnityEngine;

public class CodeLocker : MonoBehaviour
{
    //The required code lenght
    private int codeLength;

    //The player's input lenght
    private int placeInCode;    

    /// <summary>
    /// Desired code
    /// </summary>
    [SerializeField] private string code;

    /// <summary>
    /// Receive the players input
    /// </summary>
    [SerializeField] private string attemptedCode;

    /// <summary>
    /// Variable of type animator to get access to the components of the game
    /// object
    /// </summary>
    [SerializeField]private Animator doorAnim;
    private Animator animator;



    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        codeLength = code.Length;
    }

    private void Update()
    {
        Cheat();
    }

    /// <summary>
    /// Method to pass the padlock puzzle
    /// </summary>
    private void Cheat() 
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Right");
            doorAnim.SetTrigger("Interact");
        }
    }

    /// <summary>
    /// Method that check if the inserted code is correct and resets and starts
    /// the animations
    /// </summary>
    private void CheckCode()
    {
        if (attemptedCode == code)
        {
            animator.SetTrigger("Right"); 
            
            //FindObjectOfType<SoundManager>().Play("PassW_Comb_Right");

            doorAnim.SetTrigger("Interact");
        }

        else
        {
            animator.SetTrigger("Wrong"); 
            //FindObjectOfType<SoundManager>().Play("PassW_Comb_Wrong");
        }
    }

    /// <summary>
    /// Method that gets the value and compares with the desired code
    /// </summary>
    /// <param name="value"> Get value </param>
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
