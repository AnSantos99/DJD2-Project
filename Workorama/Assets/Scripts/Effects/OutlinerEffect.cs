using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinerEffect : MonoBehaviour
{

    PuzzleItemType itemType;

    [SerializeField] 
    private Transform cameraTransform;

    private float hitRange;

    // Start is called before the first frame update
    private void Start()
    {
        hitRange = 3f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckObjectHit();
    }

    private void CheckObjectHit()
    {

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
        out RaycastHit hit, hitRange))
        {

        }
    }
}
