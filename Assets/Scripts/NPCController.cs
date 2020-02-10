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
    //Not public
    float waitTillNextFire = 0f;
    Transform target;
    private int randomSpot;
    [HideInInspector]
    public List<Transform> navPoints;
    GameObject map;

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

    // Update is called once per frame
    void Update()
    {
        //SET RANDOM POSITION FOR NPC TO MOVE TO
        if (Vector3.Distance(transform.position, navPoints[randomSpot].position) < 10f)
        {
            randomSpot = Random.Range(0, navPoints.Count);
        }
       
        if (this.GetComponent<FieldOfView>().attackTarget) //IF PLAYER IS IN ATTACK RANGE
        {
            //Stop agent from moving
            agent.isStopped = true;

            //Face the target
            FaceTarget();

            //FIRE RATE LIMIT
            if (waitTillNextFire <= 0)
            {
                FindObjectOfType<AudioManager>().Play("FireSound");
                Shoot();
                waitTillNextFire = 1;
            }

            //FIRESPEED IS A PUBLIC VARIABLE SET IN EDITOR TO LIMIT FIRE RATE.
            waitTillNextFire -= Time.deltaTime * fireSpeed;
        }
        else if (this.GetComponent<FieldOfView>().followTarget) //IF PLAYER IS IN FOLLOW RANGE
        {
            //Make sure player is not stopped
            agent.isStopped = false;

            //GOTO PLAYER POS
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
        else if (this.GetComponent<FieldOfView>().lastPos != Vector3.zero && !(Vector3.Distance(transform.position, this.GetComponent<FieldOfView>().lastPos) < 10f)) //IF LAST POSITION IS NOT 0 AND NPC HAS NOT REACHED lastPos
        {
            //Make sure player is not stopped
            agent.isStopped = false;

            //GO TO LAST POSITION OF PLAYER
            agent.SetDestination(this.GetComponent<FieldOfView>().lastPos);
        }
        else
        {
            //IF WANDERING, SET LASTPOS TO 0
            this.GetComponent<FieldOfView>().lastPos = Vector3.zero;

            //Make sure player is not stopped
            agent.isStopped = false;

            //Move NPC to random position set at beginning of Update()
            agent.SetDestination(navPoints[randomSpot].position);
        }



    }



    /*
     * FaceTarget() turns the NPCs gun towards the player
     */
    void FaceTarget()
    {
        Vector3 direction = (target.position - Gun.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        Gun.transform.rotation = Quaternion.Slerp(Gun.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    /*
     * Shoot() fires a bullet in a straight line, then detects whether or it's heading towards the player.
     */
    void Shoot()
    {
        //Spawns a bullet at the firepoint object location and adds a force forward.
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(FirePoint.forward * bulletForce, ForceMode.Impulse);

        //////////////////////////////////////////////////////
        // Raycast that detects what object a bullet will hit
        //////////////////////////////////////////////////////
        RaycastHit hit;
        Vector3 rayOrigin = bullet.transform.position;
        Vector3 rayDirection = bullet.transform.TransformDirection(Vector3.forward);
        float rayRange = 1000, rayTime = 0.5f;
        Debug.DrawRay(rayOrigin, rayDirection * rayRange, Color.magenta, rayTime);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayRange))
        {
        
            if (hit.collider)
            {
                if (hit.collider.tag == "PlayerTank")
                {
                    Debug.Log("Bullet heading towards player");
                }
            }

        }
    }
}

