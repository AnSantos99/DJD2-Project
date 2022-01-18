using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private string horizontalInputName = "Horizontal";
    private string verticalInputName = "Vertical";

    [SerializeField]private float walkSpeed, runSpeed;
    [SerializeField]private float runBuildUpSpeed;
    [SerializeField]private KeyCode runKey;

    private Animator anim;

    private float movementSpeed;

    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + 
            rightMovement, 1.0f) * movementSpeed);

        SetMovementSpeed();
    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey))
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, 
                Time.deltaTime * runBuildUpSpeed);

            anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        }

        else if(Input.GetAxis(horizontalInputName ) < 0 || 
            Input.GetAxis(verticalInputName) < 0 || 
            Input.GetAxis(horizontalInputName) > 0 || 
            Input.GetAxis(verticalInputName) > 0)
        {
            movementSpeed = Mathf.Clamp(movementSpeed, walkSpeed, 
                Time.deltaTime * runBuildUpSpeed);

            anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        }

        else
            anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }
}
