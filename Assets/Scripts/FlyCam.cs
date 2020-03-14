using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script enables a free flying camera. It is controlled with 
    a mouse and keyboard.
    -Mouse controls the camera rotation in 3D space
    -WASD controls the camera position in the world
    -SHIFT speeds up the camera while held
 */

public class FlyCam : MonoBehaviour
{
    //Private variables----------------------------------------------------
    [SerializeField]
    private float camSpeed = 15f;//modify normal cam speed
    [SerializeField]
    private float fastCamSpeed = 50f;//modify cam speed when shift is held
    [SerializeField]
    private float cameraSensitivity = 50f;
    [SerializeField]
    private KeyCode freezeKey;
    [SerializeField]
    private KeyCode slomokey;
    [SerializeField]
    private float slowMoRate = 0.5f;

    private bool zoomingIn;
    private bool isFast;
    private float zoomAmount;
    private float rotationX= 0f;
    private float rotationY = 0f;
    private bool isFreezeEnabled = false;
    //--------------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        //Reset variables
        zoomingIn = false;
        isFast = false;
        rotationX = 0f;
        rotationY = 0f;
        isFreezeEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the camera rotation based on mouse movement
        rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.fixedDeltaTime;
        rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.fixedDeltaTime;
        //Clamp the rotation in y axis to prevent inverted controls
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        //Rotate the camera
        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        /*
         Move the camera
         If Shift is held, camera moves faster.
         Both normal and fast camera movement can be changed in the editor
         */
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * camSpeed * Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;
            transform.position += transform.right * camSpeed * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        }else
        {
            transform.position += transform.forward * fastCamSpeed * Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;
            transform.position += transform.right * fastCamSpeed * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        }    
        
        /*
         Call SetupCam() if switching from main camera to free fly cam
         else call DisableCam() when switching back to main camera
         */
        if(!Camera.main)
        {
            SetupCam();
        }else
        {
            DisableCam();
        }
        
        //Freeze the game with a chosen key
        if(Input.GetKeyDown(freezeKey))
        {
            if (Time.timeScale == 0f)
            {
                isFreezeEnabled = false;
                Time.timeScale = 1f;
            }
            else if (Time.timeScale > 0f)
            {
                isFreezeEnabled = true;
                Time.timeScale = 0f;
            }
        }

        if (!isFreezeEnabled)
        {
            if (Input.GetKey(slomokey))
            {
                Time.timeScale = slowMoRate;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02F;
            }
        }
    }

    /*
    Call this function when switching to the free cam
    -Hide and lock the cursor
    -Disable tank controls
    */
    private void SetupCam()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("PlayerTank").GetComponent<TankMovement>().enabled = false;
    }

    /*
    Call this function when switching to the free cam
    -Re-enable the cursor
    -Enable tank controls
    */
    private void DisableCam()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("PlayerTank").GetComponent<TankMovement>().enabled = true;
    }
}