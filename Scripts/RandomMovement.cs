using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range = 8f;
    public Transform centrePoint;

    void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!centrePoint) centrePoint = transform;
    }

    void Update()
    {
        
        if (!agent || !agent.enabled || !agent.isOnNavMesh) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (RandomPoint(centrePoint.position, range, out var p))
                agent.SetDestination(p);
        }
    }

    bool RandomPoint(Vector3 center, float r, out Vector3 result)
    {
        var random = center + Random.insideUnitSphere * r;
        if (NavMesh.SamplePosition(random, out var hit, 2f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}
