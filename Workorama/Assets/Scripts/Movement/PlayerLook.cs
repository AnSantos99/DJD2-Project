using UnityEngine;

/// <summary>
/// Class that handles where the player is looking at
/// </summary>
public class PlayerLook : MonoBehaviour
{
    /// <summary>
    /// Set up X and Y mouse coordinates
    /// </summary>
    [SerializeField]
    private string mouseXInputName, mouseYInputName;

    /// <summary>
    /// Setup mouse Sensitivity
    /// </summary>
    [SerializeField]
    private float mouseSensitivity;

    /// <summary>
    /// Get player transform
    /// </summary>
    [SerializeField]
    private Transform playerBody;

    /// <summary>
    /// Get the x Axis clamp of the degrees
    /// </summary>
    private float xAxisClamp;

    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }

    /// <summary>
    /// Lock the cursor
    /// </summary>
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() => CameraRotation();


    /// <summary>
    /// Rotate camera according to mouse input and sensitivity with its given
    /// limits.
    /// </summary>
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity 
            * Time.deltaTime;

        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity 
            * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue (90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// To garante that the player is not able to turn 360 degrees while
    /// looking up or down
    /// </summary>
    /// <param name="value"> get the value </param>
    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
