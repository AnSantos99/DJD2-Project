using UnityEngine;

public class LockerInputController : MonoBehaviour
{
    /// <summary>
    /// Get acees to instances of class codelocker
    /// </summary>
    private CodeLocker codeLock;
    [SerializeField]
    private Transform cameraTransform;

    /// <summary>
    /// Variable of type animator to get access to the components of the game
    /// object
    /// </summary>
    private Animation anim;

    // Defined unchangeable range for raycast
    private const int reachRange = 100;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CheckHitObj();
        }
    }

    /// <summary>
    /// Hit object with raycast
    /// </summary>
    private void CheckHitObj()
    {

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
        out RaycastHit hit, reachRange))
        {
            //Witch codelock is beeing pressed
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLocker>();
            anim = hit.transform.gameObject.GetComponentInParent<Animation>();

            if (codeLock != null)
            {
                string value = hit.transform.name;
                codeLock.SetValue(value);
                anim.Play();
            }
        }
    }
}
