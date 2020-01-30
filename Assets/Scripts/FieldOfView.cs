using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float followRadius;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    Transform target;
    public Transform visibleTarget;

    public bool attackTarget = false;
    public bool followTarget = false;
    

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        StartCoroutine("FindTargetsWithDelay", .2f);   
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        Collider[] targetsInFollowRadius = Physics.OverlapSphere(transform.position, followRadius, targetMask);
        //Transform visibleTarget;

        if (targetsInViewRadius.Length <= 0)
        {
            attackTarget = false;
        }
        if (targetsInFollowRadius.Length <= 0)
        {
            followTarget = false;
        }


        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            visibleTarget = targetsInViewRadius[i].transform;
            Vector3 targetDirection = (visibleTarget.position - transform.position).normalized;

            float distanceToTarget = Vector3.Distance(transform.position, visibleTarget.position);

            if (!Physics.Raycast(transform.position, targetDirection, distanceToTarget, obstacleMask))
            {
                attackTarget = true;
            }
            else
            {
                attackTarget = false;
            }
        }

        for (int i = 0; i < targetsInFollowRadius.Length; i++)
        {
            visibleTarget = targetsInFollowRadius[i].transform;
            Vector3 targetDirection = (visibleTarget.position - transform.position).normalized;

            float distanceToTarget = Vector3.Distance(transform.position, visibleTarget.position);

            if (!Physics.Raycast(transform.position, targetDirection, distanceToTarget, obstacleMask))
            {
                followTarget = true;
            }
            else
            {
                followTarget = false;
            }
        }
    }
}
 