using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Respawn")]
    public Transform[] respawnPoints;   
    public float respawnDelay = 0f;     

    NavMeshAgent agent;
    Collider[] cols;
    Renderer[] rends;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cols  = GetComponentsInChildren<Collider>(true);
        rends = GetComponentsInChildren<Renderer>(true);
    }

    public void Kill()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        
        foreach (var r in rends) r.enabled = false;
        foreach (var c in cols)  c.enabled = false;

        if (agent) agent.enabled = false;

        if (respawnDelay > 0f)
            yield return new WaitForSeconds(respawnDelay);

        
        Vector3 targetPos = transform.position;
        if (respawnPoints != null && respawnPoints.Length > 0)
        {
            Transform p = respawnPoints[Random.Range(0, respawnPoints.Length)];
            targetPos = p.position;
        }

        
        if (NavMesh.SamplePosition(targetPos, out var hit, 5f, NavMesh.AllAreas))
            targetPos = hit.position;

        
        if (agent)
        {
            agent.enabled = true;
            agent.Warp(targetPos);
        }
        else
        {
            transform.position = targetPos;
        }

        
        yield return null;

        
        foreach (var c in cols)  c.enabled = true;
        foreach (var r in rends) r.enabled = true;
    }
}
