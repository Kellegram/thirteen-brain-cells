using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

   // public Vector3 centerOfMass;


    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSpeed = 180f;

    private Rigidbody tankRigidBody;
    private float movementFactor;
    private float turnFactor;

    /*
     *Get tank body on script load
     */
    private void Awake()
    {
        tankRigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Ensure tank is not kinematic on game start
        tankRigidBody.isKinematic = false;

        //Reset the movement and turn inputs
        movementFactor = 0f;
        turnFactor = 0f;
    }

    private void OnDisable()
    {
        tankRigidBody.isKinematic = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
      //  tankRigidBody.centerOfMass = centerOfMass;
      tankRigidBody.maxAngularVelocity = 0f;
    }

    /*
     *
     *
     */
    private void LateUpdate()
    {
        movementFactor = Input.GetAxis("Vertical");
        turnFactor = Input.GetAxis("Horizontal");
        tankRigidBody.velocity = new Vector3(0f, 0f, 0f);
    }


    private void FixedUpdate()
    {
        Move();
        Turn();
        // tankRigidBody.angularVelocity = new Vector3(0f, 0f, 0f);
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementFactor * speed * Time.deltaTime;
        tankRigidBody.MovePosition(tankRigidBody.position + movement);
    }

    private void Turn()
    {
        //Calculate how many degrees to turn
        float turn = turnFactor * turnSpeed * Time.deltaTime;

        //Rotate only in the y axis
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        //Apply rotation
        tankRigidBody.MoveRotation(tankRigidBody.rotation * turnRotation);
    }
}
