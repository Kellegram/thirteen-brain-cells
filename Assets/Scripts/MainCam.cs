using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    //Private variables-----------------------------------------
    private Vector3 defaultCamPos;
    private Quaternion defaultCamRot;
    private Vector3 defaultCamLookAt;
    GameObject playerTank;
    private Vector3 targetPos;
    private bool isOrbiting;
    private Vector3 offset;
    Transform playerTankTransform;
    //----------------------------------------------------------

    //Editor variables------------------------------------------
    [SerializeField]
    private float offsety = 30.0f;
    [SerializeField]
    private float offsetz = 16.0f;
    [SerializeField]
    private float rotSpeed = 30f;
    //----------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //Save default camera state to restore on round start
        defaultCamPos = Camera.main.transform.position;
        defaultCamRot = Camera.main.transform.rotation;
        

        //Get the player tank object and transform
        playerTank = GameObject.Find("PlayerTank");
        playerTankTransform = playerTank.transform;

        //Find an offset away from tank position and move the camera to the calculated point
        offset = new Vector3(playerTankTransform.position.x, playerTankTransform.position.y + offsety, playerTankTransform.position.z + offsetz);
        transform.position = targetPos + offset;

        //Set the target for the camera to the tank's position
        targetPos = playerTankTransform.position;

        //Make the camera look at the tank
        transform.LookAt(targetPos);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Run this when the level is loaded, orbits the camera around the player
        if (isOrbiting)
        {

            transform.RotateAround(targetPos, Vector3.up, rotSpeed * Time.deltaTime);
        }else
        {
            transform.position = defaultCamPos;
            transform.rotation = defaultCamRot;
        }
    }

    public void setOrbit(bool tf)
    {
        isOrbiting = tf;
    }
}
