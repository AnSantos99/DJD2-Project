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
    private float currentTime;

    void Start()
    {
        playerHeight = 1.5f;
        playerLook = GetComponent<PlayerLook>();
        playerMov = (PlayerMov)gameObject.GetComponentInParent(typeof(PlayerMov));
    }

    void Update()
    {
        UpdateCamera();
    }

    void LateUpdate()
    {
        // If state of camera is switching to player make it smoth transition until get close enough to switch to player
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

        // If state of camera is switching to 2D view make it smoth transition until get close enough to switch to 2D view
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

    /// <summary>
    /// Switch cam to player
    /// </summary>
    public void ReturnToPlayer()
    {
        transitToPlayer = true;

        camMask = -1;
        this.GetComponent<Camera>().fieldOfView = 60f;
    }

    /// <summary>
    /// Check if the cam need to be switch to 2D or 3D view
    /// </summary>
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

    /// <summary>
    /// Check if the cam need to be switch to 2D view
    /// </summary>
    private bool Check2DSwitch()
    {
        if (transform.position.x < 250)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Switch to 2D view, and check witch wall have to be ignored
    /// </summary>
    private void SwitchTo2D()
    {
        float northDif = cam2D.position.x - helpCamNorth.position.x; 
        float southDif = cam2D.position.x - helpCamSouth.position.x; 
        float eastDif = cam2D.position.x - helpCamEast.position.x; 
        float westDif = cam2D.position.x - helpCamWest.position.x; 

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

    /// <summary>
    /// Switch to 3D view
    /// </summary>
    private void SwitchTo3D()
    {
        ReturnToPlayer();
    }

    /// <summary>
    /// //Update camera Culling mask to ignore walls
    /// </summary>
    private void UpdateCamera()
    {
        this.GetComponent<Camera>().cullingMask = camMask;
    }


    public void CamShakeDown(float timeRotating)
    {
        StartCoroutine(ShakeCamera(timeRotating));
    }

    //Corroutine to shake the camera, making a eathquake efect
    private IEnumerator ShakeCamera(float timeShaking)
    {
        Vector3 position = transform.localPosition;
        Quaternion rotation = transform.localRotation;

        while (currentTime < timeShaking)
        {
            float rotX = Random.Range(-1f, 1f) * 0.001f;
            float rotY = Random.Range(-1f, 1f) * 0.01f;
            float rotZ = Random.Range(-1f, 1f) * 0.001f;

            transform.localPosition = new Vector3(position.x, position.y - 2f, position.z);
            transform.localRotation = new Quaternion(rotX, rotY, rotZ, 1);

            currentTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = position;
        transform.localRotation = rotation;

        playerLook.enabled = true;
        playerMov.enabled = true;
        transform.position = new Vector3(player.position.x, player.position.y + playerHeight, player.position.z);
        currentTime = 0.0f;
    }
}
