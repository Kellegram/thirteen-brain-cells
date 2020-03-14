using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSpeed = 180f;

    private Rigidbody tankRigidBody;
    private float movementFactor;
    private float turnFactor;
    private float collisionMultiplier;

    private void Awake()
    {
        //Get tank body on script load
        tankRigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Ensure tank is not kinematic on game start
        tankRigidBody.isKinematic = false;

        //Reset the movement and turn inputs
        movementFactor = 0f;
        turnFactor = 0f;
        collisionMultiplier = 1f;
    }

    private void OnDisable()
    {
        tankRigidBody.isKinematic = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Prevent Angular velocity of the rigid body from being modified on collision
        tankRigidBody.maxAngularVelocity = 0f;
    }

    /*
     *Receive input and reset velocity
     * to avoid unexpected behavior
     */
    private void Update()
    {
        movementFactor = Input.GetAxis("Vertical");
        turnFactor = Input.GetAxis("Horizontal");
        tankRigidBody.velocity = new Vector3(0f, 0f, 0f);
    }

    /*
     *Call movement functions in FixedUpdate
     * to avoid issues with physics
     */
    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    /*
     *This function calculates how much the tank should move
     * and then moves the tank based on set speed and whether
     * it's colliding with something or not
     */
    private void Move()
    {
        Vector3 movement = transform.forward * movementFactor * speed * collisionMultiplier * Time.deltaTime;
        tankRigidBody.MovePosition(tankRigidBody.position + movement);
    }

    /*
     *Rotate the tank body my turnSpeed degrees per second
     */
    private void Turn()
    {
        //Calculate how many degrees to turn
        float turn = turnFactor * turnSpeed * Time.deltaTime;

        //Rotate only in the y axis
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        //Apply rotation, bypasses rigid body constraints
        tankRigidBody.MoveRotation(tankRigidBody.rotation * turnRotation);
    }

    /*
     * When colliding with walls or tanks, slow down the tank significantly
     * to prevent the tank from entering walls and creating other unwanted behavior
     */
    private void OnCollisionEnter(Collision other){collisionMultiplier = 0.1f;}
    private void OnCollisionExit(Collision other){collisionMultiplier = 1.0f;}

}
