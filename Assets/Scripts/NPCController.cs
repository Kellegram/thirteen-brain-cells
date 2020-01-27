using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public Transform Gun;
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
                agent.isStopped = true;
                FaceTarget();

                if (waitTillNextFire <= 0)
                {
                    Shoot();
                    waitTillNextFire = 1;
                }

                waitTillNextFire -= Time.deltaTime * fireSpeed;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(moveSpots[randomSpot].position);
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - Gun.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        Gun.rotation = Quaternion.Slerp(Gun.rotation, lookRotation, Time.deltaTime * 7f);
       
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
    }
}
