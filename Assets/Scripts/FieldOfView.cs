using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{ 
    [Header("Look Radius")]
    public float attackRadius = 50;
    public float followRadius = 100;

    /*
     * targetMask is the player
     * obstacleMask is any obstacles that the npc should not be able to see through
     */
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    //Variables for tracking states
    [HideInInspector]
    public Transform visibleTarget;
    [HideInInspector]
    public bool attackTarget = false;
    [HideInInspector]
    public bool followTarget = false;
    [HideInInspector]
    public Vector3 lastPos = Vector3.zero;

    private void Start()
    {
        //Coroutines allow you to pause the execution of that function for a delay using yield
        StartCoroutine("FindTargetsWithDelay", .2f);   
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            //wait .2f seconds before running FindVisibleTargets() again
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    /*
     * tracks the attackTarget and followTarget state
     * by checking whether or not a target is within the
     * view of the NPC
     * 
     * (Also accounts for obstacles using masks)
     */
    void FindVisibleTargets()
    {
        //Creates colliders with a given radius that trigger if anything in targetMask enters the circle
        Collider[] targetsInAttackRadius = Physics.OverlapSphere(transform.position, attackRadius, targetMask);
        Collider[] targetsInFollowRadius = Physics.OverlapSphere(transform.position, followRadius, targetMask);

        //States are set to false if there are no targetMask targets within either radius
        if (targetsInAttackRadius.Length <= 0)
        {
            attackTarget = false;
        }
        if (targetsInFollowRadius.Length <= 0)
        {
            followTarget = false;
        }

        //For each targetMask target in the attackRadius
        for (int i = 0; i < targetsInAttackRadius.Length; i++)
        {
            visibleTarget = targetsInAttackRadius[i].transform;
            Vector3 targetDirection = (visibleTarget.position - transform.position).normalized;

            //Calculate distance between NPC and player
            float distanceToTarget = Vector3.Distance(transform.position, visibleTarget.position);

            /*
             * If there are no obstacles in between the NPC and player
             * set attackTarget state to true
             * 
             * If there are obstacles in between the NPC and player
             * set attackTarget to false
             */
            if (!Physics.Raycast(transform.position, targetDirection, distanceToTarget, obstacleMask))
            {
                attackTarget = true;
            }
            else
            {
                attackTarget = false;
            }
        }

        //For each targetMask target in the followRadius
        for (int i = 0; i < targetsInFollowRadius.Length; i++)
        {
            visibleTarget = targetsInFollowRadius[i].transform;
            Vector3 targetDirection = (visibleTarget.position - transform.position).normalized;

            //Calculate distance between NPC and player
            float distanceToTarget = Vector3.Distance(transform.position, visibleTarget.position);

            /*
             * If there are no obstacles in between the NPC and player
             * set followTarget state to true
             * 
             * If there are obstacles in between the NPC and player
             * set followTarget to false
             */
            if (!Physics.Raycast(transform.position, targetDirection, distanceToTarget, obstacleMask))
            {
                followTarget = true;
                lastPos = visibleTarget.position;
            }
            else
            {
                followTarget = false;
            }
        }
    }
}