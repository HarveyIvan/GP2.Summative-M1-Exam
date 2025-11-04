using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogger : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        float distanceTraveled = Vector3.Distance(initialPosition, transform.position);
        Debug.Log("Projectile traveled: " + distanceTraveled.ToString("F2"));
    }
}
