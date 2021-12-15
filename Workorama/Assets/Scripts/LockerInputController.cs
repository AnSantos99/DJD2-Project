using UnityEngine;

public class LockerInputController : MonoBehaviour
{
    /// <summary>
    /// Get acees to instances of class codelocker
    /// </summary>
    private CodeLocker codeLock;

    /// <summary>
    /// Variable of type animator to get access to the components of the game
    /// object
    /// </summary>
    private Animation anim;

    // Defined unchangeable range for raycast
    private const int reachRange = 100;


    private void Update()
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, reachRange))
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
