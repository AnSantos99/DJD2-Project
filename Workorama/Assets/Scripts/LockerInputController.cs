using UnityEngine;

/// <summary>
/// Class that handles the input on the numpad
/// </summary>
public class LockerInputController : MonoBehaviour
{
    /// <summary>
    /// Get acees to instances of class codelocker
    /// </summary>
    private CodeLocker codeLock;

    /// <summary>
    /// Get access to the camera transform component
    /// </summary>
    [SerializeField]
    private Transform cameraTransform;

    /// <summary>
    /// Variable of type animator to get access to the components of the game
    /// object
    /// </summary>
    private Animation anim;

    // Defined unchangeable range for raycast
    private const int reachRange = 100;

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
            CheckHitObj();
    }

    /// <summary>
    /// Hit object with ray to be able to verify which key is being pressed by
    /// the user looking at the object.
    /// </summary>
    private void CheckHitObj()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
        out RaycastHit hit, reachRange))
        {
            //Which codelock is beeing pressed
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLocker>();
            anim = hit.transform.gameObject.GetComponentInParent<Animation>();

            if (codeLock != null)
            {
                string value = hit.transform.name;
                codeLock.SetValue(value);
                anim.Play();
                FindObjectOfType<SoundManager>().Play("KeyPress");
            }
        }
    }
}
