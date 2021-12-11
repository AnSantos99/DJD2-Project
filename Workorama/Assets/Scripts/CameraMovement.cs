using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cam2D;
    [SerializeField] private Transform player;
    [SerializeField] private Transform helpCamNorth;
    [SerializeField] private Transform helpCamSouth;
    [SerializeField] private Transform helpCamEast;
    [SerializeField] private Transform helpCamWest;


    private int camMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    public void SwitchWest()
    {
        transform.position = cam2D.position;
        transform.rotation = cam2D.rotation;
        camMask = -1;
        camMask &=  ~(1 << LayerMask.NameToLayer("WestWall"));
        this.GetComponent<Camera>().fieldOfView = 4.8f;
    }

    public void SwitchEast()
    {
        transform.position = cam2D.position;
        transform.rotation = cam2D.rotation;
        camMask = -1;
        camMask &=  ~(1 << LayerMask.NameToLayer("EastWall"));
        this.GetComponent<Camera>().fieldOfView = 4.8f;
    }

    public void SwitchNorth()
    {
        transform.position = cam2D.position;
        transform.rotation = cam2D.rotation;
        camMask = -1;
        camMask &=  ~(1 << LayerMask.NameToLayer("NorthWall"));
        this.GetComponent<Camera>().fieldOfView = 4.8f;
    }

    public void SwitchSouth()
    {
        transform.position = cam2D.position;
        transform.rotation = cam2D.rotation;
        camMask = -1;
        camMask &=  ~(1 << LayerMask.NameToLayer("SouthWall"));
        this.GetComponent<Camera>().fieldOfView = 4.8f;
    }

    public void ReturnToPlayer()
    {
        transform.position = player.position;
        transform.rotation = player.rotation;
        camMask = -1;
        this.GetComponent<Camera>().fieldOfView = 60f;
    }

    private void UpdateCamera()
    {
        this.GetComponent<Camera>().cullingMask = camMask;
    }
}
