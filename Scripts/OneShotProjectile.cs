using UnityEngine;

public class OneShotProjectile : MonoBehaviour
{
    public float lifeTime = 5f;
    private GameObject owner; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    
    public void SetOwner(GameObject shooter)
    {
        owner = shooter;
    }

    void OnCollisionEnter(Collision c)
    {
        var target = c.collider.GetComponentInParent<EnemyHealth>();
        if (target != null)
        {
            target.Kill();

            
            if (owner != null)
            {
                var playerInventory = owner.GetComponent<PlayerInventory>();
                if (playerInventory != null)
                {
                    playerInventory.RegisterKill();
                }
            }
        }

        Destroy(gameObject);
    }
}

