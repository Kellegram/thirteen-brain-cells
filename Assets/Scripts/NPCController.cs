using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public NavMeshAgent agent;
    public float lookRadius = 10f;
    public float fireSpeed = 2f;
    float waitTillNextFire = 0f;
    Transform target;
    public float bulletForce = 80f;

    public Transform[] moveSpots;
    private int randomSpot;

    private void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
;       target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 10f)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
        }

        if (target && Vector3.Distance(target.position, transform.position) <= lookRadius)
        {
            if (PlayerManager.instance.enemy.GetComponent<FieldOfView>().attackTarget)
            {
                agent.isStopped = true;
                FaceTarget();

                if (waitTillNextFire <= 0)
                {
                    Shoot();
                    waitTillNextFire = 1;
                }

                waitTillNextFire -= Time.deltaTime * fireSpeed;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(moveSpots[randomSpot].position);
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Shoot()
    {
        //Spawns a bullet at the firepoint object location and adds a force forward.
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(FirePoint.forward * bulletForce, ForceMode.Impulse);

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
