using UnityEngine;

public class LockerInputController : MonoBehaviour
{

    private CodeLocker codeLock;
    private Animation anim;

    private const int reachRange = 100;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CheckHitObj();
        }
    }

    private void CheckHitObj()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, reachRange))
        {
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLocker>();//Witch codelock is beeing pressed
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
