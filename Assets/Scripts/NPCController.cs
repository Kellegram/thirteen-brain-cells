using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class NPCController : MonoBehaviour
{
    [Header("Prefab Objects")]
    public Transform FirePoint;
    public Transform Gun;
    public GameObject BulletPrefab;
    public NavMeshAgent agent;
    [Space(10)]

    [Header("NPC Attributes")]
    public float fireSpeed = 2f;
    public float bulletForce = 80f;
    [Space(20)]
    float waitTillNextFire = 0f;
    Transform target;
    private int randomSpot;
    public List<Transform> navPoints;
    GameObject map;

    /*
     * Start() begins when the object with this script on it is instatiated.
     * This script will assign all of the necessary variables in order for the enemy tank to operate correctly.
     */
    private void Start()
    {
        map = PlayerManager.instance.map;

        //The target is a transform of the player object (assigned in the PlayerManager in the Editor)
        target = PlayerManager.instance.player.transform;
        
        //Add all navpoints when enemy is spawned
        Transform[] allChildren = map.GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.tag == "Navpoint")
            {
                navPoints.Add(child);
            }
        }

        //When the game starts, pick a random number between 0 and navPoints.Count

        randomSpot = Random.Range(0, navPoints.Count);

        //agent is a NavMeshAgent who can navigate around a nav mesh.
        //GetComponent searches for the NavMeshAgent component assigned to this object.
        agent = GetComponent<NavMeshAgent>();
    }

    /*
     * Update() is called once per frame
     * This function manages the enemy's state machine AI
     */
    void Update()
    {
        //Set random position for agent to move to
        if (Vector3.Distance(transform.position, navPoints[randomSpot].position) < 10f)
        {
            randomSpot = Random.Range(0, navPoints.Count);
        }
        
        //State machine for NPC tank
        if (this.GetComponent<FieldOfView>().attackTarget)
        {
            agent.isStopped = true;
            FaceTarget();

            //For fire cooldown
            if (waitTillNextFire <= 0)
            {
                Shoot();
                waitTillNextFire = 1;
            }

            waitTillNextFire -= Time.deltaTime * fireSpeed;
        }
        else if (this.GetComponent<FieldOfView>().followTarget)
        {
            //Make sure player is not stopped
            agent.isStopped = false;
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
        else if (this.GetComponent<FieldOfView>().lastPos != Vector3.zero && !(Vector3.Distance(transform.position, this.GetComponent<FieldOfView>().lastPos) < 10f)) //IF LAST POSITION IS NOT 0 AND NPC HAS NOT REACHED lastPos
        {
            agent.isStopped = false;
            agent.SetDestination(this.GetComponent<FieldOfView>().lastPos);
        }
        else
        {
            this.GetComponent<FieldOfView>().lastPos = Vector3.zero;
            agent.isStopped = false;
            agent.SetDestination(navPoints[randomSpot].position);
        }
    }

    /*
     * FaceTarget() rotates the NPCs gun towards the player
     * Takes in no variables
     * Returns no variables
     */
    void FaceTarget()
    {
        Vector3 direction = (target.position - Gun.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //Transforms the Gun object's rotation to smoothly linearly interpolate between its current rotation and the target rotation.
        Gun.transform.rotation = Quaternion.Slerp(Gun.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    /*
     * Shoot() fires a bullet in a straight line, then detects whether or it's heading towards the player.
     * Takes in no variables
     * Returns no variables
     */
    void Shoot()
    {
        RaycastHit hit;
        Vector3 rayOrigin = FirePoint.position;
        Vector3 rayDirection = FirePoint.transform.TransformDirection(Vector3.forward);
        float rayRange = 1000, rayTime = 0.5f;
        Debug.DrawRay(rayOrigin, rayDirection * rayRange, Color.magenta, rayTime);

        //If a raycast hits an object from 
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayRange))
        {
            if (hit.collider)
            {
                if (hit.collider.tag == "PlayerTank")
                {
                    //Spawns a bullet at the firepoint object location and adds a force forward.
                    GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
                    bullet.transform.tag = "EnemyBullet";
            
                    rb.AddForce(FirePoint.forward * bulletForce, ForceMode.Impulse);
                    Debug.Log("Bullet heading towards player");
                }
            }
        }
    }
}

