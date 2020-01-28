using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    Transform target;
    public Transform visibleTarget;

    public bool attackTarget = false;
    

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
        //Transform visibleTarget;
       
        
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
        
       
    }
}
 