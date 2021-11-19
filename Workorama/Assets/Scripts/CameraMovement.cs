using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform north;
    [SerializeField] private Transform south;
    [SerializeField] private Transform east;
    [SerializeField] private Transform west;
    [SerializeField] private Transform player;

    private int camMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwitchCamera();
        UpdateCamera();
    }

    private void CheckSwitchCamera()
    {
        if(Input.GetKey(KeyCode.N))
        {
            transform.position = north.position;
            transform.rotation = north.rotation;
            camMask = -1;
            camMask &=  ~(1 << LayerMask.NameToLayer("NorthWall"));
            this.GetComponent<Camera>().fieldOfView = 4.8f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position = south.position;
            transform.rotation = south.rotation;
            camMask = -1;
            camMask &=  ~(1 << LayerMask.NameToLayer("SouthWall"));
            this.GetComponent<Camera>().fieldOfView = 4.8f;
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.position = west.position;
            transform.rotation = west.rotation;
            camMask = -1;
            camMask &=  ~(1 << LayerMask.NameToLayer("WestWall"));
            this.GetComponent<Camera>().fieldOfView = 4.8f;
        }
        if(Input.GetKey(KeyCode.E))
        {
            transform.position = east.position;
            transform.rotation = east.rotation;
            camMask = -1;
            camMask &=  ~(1 << LayerMask.NameToLayer("EastWall"));
            this.GetComponent<Camera>().fieldOfView = 4.8f;
        }
        if(Input.GetKey(KeyCode.P))
        {
            transform.position = player.position;
            transform.rotation = player.rotation;
            camMask = -1;
            this.GetComponent<Camera>().fieldOfView = 60f;
        }
    }
    private void UpdateCamera()
    {
        this.GetComponent<Camera>().cullingMask = camMask;
    }
}
