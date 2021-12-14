using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInputController : MonoBehaviour
{

    CodeLocker codeLock;

    int reachRange = 100;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CheckHitObj();
        }
    }

    void CheckHitObj()
    {
        Debug.Log("Clicked");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, reachRange))
        {
            Debug.Log("Hit!");
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLocker>();//Witch codelock is beeing pressed

            if (codeLock != null)
            {
                string value = hit.transform.name;
                codeLock.SetValue(value);
            }
        }
    }
}
