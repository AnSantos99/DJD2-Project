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

    [SerializeField] private float playerHeight;

    private PlayerLook playerLook;
    private PlayerMov playerMov;
    private int camMask = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerHeight = 1.1f;
        playerLook = GetComponent<PlayerLook>();
        playerMov = (PlayerMov)gameObject.GetComponentInParent(typeof(PlayerMov));
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
        this.GetComponent<Camera>().fieldOfView = 7f;
    }

    public void SwitchEast()
    {
        transform.position = cam2D.position;
        transform.rotation = cam2D.rotation;
        camMask = -1;
        camMask &=  ~(1 << LayerMask.NameToLayer("EastWall"));
        this.GetComponent<Camera>().fieldOfView = 7f;
    }

    public void SwitchNorth()
    {
        transform.position = cam2D.position;
        transform.rotation = cam2D.rotation;
        camMask = -1;
        camMask &=  ~(1 << LayerMask.NameToLayer("NorthWall"));
        this.GetComponent<Camera>().fieldOfView = 7f;
    }

    public void SwitchSouth()
    {
        transform.position = cam2D.position;
        transform.rotation = cam2D.rotation;
        camMask = -1;
        camMask &=  ~(1 << LayerMask.NameToLayer("SouthWall"));
        this.GetComponent<Camera>().fieldOfView = 7f;
    }

    public void ReturnToPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y +
            playerHeight, player.position.z + 0.2f);

        transform.rotation = player.rotation;
        camMask = -1;
        this.GetComponent<Camera>().fieldOfView = 60f;
    }

    public void SwitchCam()
    {
        if (Check2DSwitch())
        {
            SwitchTo2D();
            //Stop Player movement, cam movement
        }
        else
        {
            SwitchTo3D();
        }
    }


    private bool Check2DSwitch()
    {
        if (transform.position.x < 250)
        {
            return true;
        }
        return false;
    }

    private void SwitchTo2D()
    {
        float northDif = cam2D.position.x - helpCamNorth.position.x; 
        float southDif = cam2D.position.x - helpCamSouth.position.x; 
        float eastDif = cam2D.position.x - helpCamEast.position.x; 
        float westDif = cam2D.position.x - helpCamWest.position.x; 

        if (northDif == southDif)
        {
            if (westDif > eastDif)
            {
                SwitchEast();
            }
            else
            {
                SwitchWest();
            }
        }
        else if (westDif == eastDif)
        {
            if (northDif > southDif)
            {
                SwitchSouth();
            }
            else
            {
                SwitchNorth();
            }
        }
        else
        {
            Debug.Log("There is something wrong with the logic");
        }

        playerLook.enabled = false;
        playerMov.enabled = false;

    }

    private void SwitchTo3D()
    {
        ReturnToPlayer();
        playerLook.enabled = true;
        playerMov.enabled = true;
    }



    private void UpdateCamera()
    {
        this.GetComponent<Camera>().cullingMask = camMask;
    }
}
