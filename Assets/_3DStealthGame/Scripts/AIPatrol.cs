using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    public float wanderRadius = 3f;
    public float wanderTimer = 1.5f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavPosition(transform.position, wanderRadius, NavMesh.AllAreas);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
    Vector3 RandomNavPosition(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask))
        {

            return navHit.position;
        }
        else
            return origin;
    }
}