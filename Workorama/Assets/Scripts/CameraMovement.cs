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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.N))
        {
            transform.position = north.position;
            transform.rotation = north.rotation;
            this.GetComponent<Camera>().cullingMask &=  ~(1 << LayerMask.NameToLayer("NorthWall"));
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position = south.position;
            transform.rotation = south.rotation;
            this.GetComponent<Camera>().cullingMask &=  ~(1 << LayerMask.NameToLayer("SouthWall"));
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.position = west.position;
            transform.rotation = west.rotation;
            this.GetComponent<Camera>().cullingMask &=  ~(1 << LayerMask.NameToLayer("WestWall"));
        }
        if(Input.GetKey(KeyCode.E))
        {
            transform.position = east.position;
            transform.rotation = east.rotation;
            this.GetComponent<Camera>().cullingMask &=  ~(1 << LayerMask.NameToLayer("EastWall"));
        }
        if(Input.GetKey(KeyCode.P))
        {
            transform.position = player.position;
            transform.rotation = player.rotation;
            this.GetComponent<Camera>().cullingMask = -1;
        }
    }
}
