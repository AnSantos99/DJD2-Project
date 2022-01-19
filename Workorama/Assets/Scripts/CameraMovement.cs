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
    [SerializeField] private float     transitionSpeed;
    [SerializeField] private float     rotationSpeed;

    [SerializeField] private float playerHeight;

    private PlayerLook playerLook;
    private PlayerMov playerMov;
    private bool transitToPlayer = false;
    private bool transitTo2D = false;
    private int camMask = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerHeight = 1.5f;
        playerLook = GetComponent<PlayerLook>();
        playerMov = (PlayerMov)gameObject.GetComponentInParent(typeof(PlayerMov));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    void LateUpdate()
    {
        if (transitToPlayer)
        {
            transform.position = Vector3.Slerp(transform.position, player.position , Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation , Time.deltaTime * rotationSpeed);
            Debug.Log(Vector3.Distance(transform.position, player.position));
            if (Vector3.Distance(transform.position, player.position) < 2f)
            {
                transform.position = new Vector3(player.position.x, player.position.y + playerHeight, player.position.z);
                transform.rotation = player.rotation;
                transitToPlayer = false;
                playerLook.enabled = true;
                playerMov.enabled = true;
            }
        }

        if (transitTo2D)
        {
            transform.position = Vector3.Slerp(transform.position, cam2D.position , Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, cam2D.rotation , Time.deltaTime * rotationSpeed);
            Debug.Log(Vector3.Distance(transform.position, cam2D.position));
            if (Vector3.Distance(transform.position, cam2D.position) < 2f)
            {
                transform.position = cam2D.position;
                transform.rotation = cam2D.rotation;
                transitTo2D = false;

                this.GetComponent<Camera>().fieldOfView = 7f;
            }
        }
        

        
    }

    public void ReturnToPlayer()
    {
        //transform.position = new Vector3(player.position.x, player.position.y + 0.52f, player.position.z + 0.2f);
        //transform.rotation = player.rotation;
        transitToPlayer = true;

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

        //transform.position = cam2D.position;
        //transform.rotation = cam2D.rotation;
        transitTo2D = true;
        camMask = -1;

        if (northDif == southDif)
        {
            if (westDif > eastDif)
            {
                camMask &=  ~(1 << LayerMask.NameToLayer("EastWall"));
            }
            else
            {
                camMask &=  ~(1 << LayerMask.NameToLayer("WestWall"));
            }
        }
        else if (westDif == eastDif)
        {
            if (northDif > southDif)
            {
                camMask &=  ~(1 << LayerMask.NameToLayer("SouthWall"));
            }
            else
            {
                camMask &=  ~(1 << LayerMask.NameToLayer("NorthWall"));
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
    }



    private void UpdateCamera()
    {
        this.GetComponent<Camera>().cullingMask = camMask;
    }
}
