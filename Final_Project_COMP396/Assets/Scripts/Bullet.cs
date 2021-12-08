using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int maxDamageInflicted=20;

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        if (hit.tag == "Player" || hit.tag == "NPC")
        {
            var health = hit.GetComponent<Health>();
            health.TakeDamage(maxDamageInflicted);
        }

        Destroy(this.gameObject);
    }
}
