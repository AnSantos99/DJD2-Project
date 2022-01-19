using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private string horizontalInputName = "Horizontal";
    private string verticalInputName = "Vertical";

    [SerializeField]private float walkSpeed, runSpeed;
    [SerializeField]private float runBuildUpSpeed;
    [SerializeField]private KeyCode runKey;

    private float movementSpeed;

    private CharacterController charController;

    /// <summary>
    /// Animation controller
    /// </summary>
    private Animator anim;

    private AudioSource sm;

    private bool walking;

    private void Awake()
    {
        sm = this.GetComponent<AudioSource>();
        charController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    /// <summary>
    /// 
    /// </summary>
    private void PlayerMovement()
    {
         
        // Receive input from "Horizontal" & "Vertical"
        // Convert it into Charactercontroller
        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + 
            rightMovement, 1.0f) * movementSpeed);

        SetMovementSpeed();
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetMovementSpeed()
    {
        // Update speed if player is running
        // Animation on 1 speed movement
        if (Input.GetKey(runKey) && walking == true)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, 
                Time.deltaTime * runBuildUpSpeed);

            anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);

            sm.volume = 1f;
            sm.pitch = 1.5f;
        }

        // Update speed if player is walking
        // Animation on 0.5 speed movement
        else if(Input.GetAxis(horizontalInputName ) < 0 || 
            Input.GetAxis(verticalInputName) < 0 || 
            Input.GetAxis(horizontalInputName) > 0 || 
            Input.GetAxis(verticalInputName) > 0)
        {
            walking = true;
            movementSpeed = Mathf.Clamp(movementSpeed, walkSpeed, 
                Time.deltaTime * runBuildUpSpeed);

            anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);

            sm.volume = 0.6f;
            sm.pitch = 1f;
        }

        // Check if player is Idle
        // Animation on 0 speed movement
        else
        {
            anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);

            sm.volume = 0f;
        }
    }
}
