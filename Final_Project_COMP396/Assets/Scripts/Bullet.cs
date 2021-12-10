using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public int maxDamageInflicted = 20;
    public GameObject enemyHealth;
    Transform topparent;

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        if (hit.tag == "Player" || hit.tag == "NPC")
        {
            Debug.Log(hit.tag);
            if (hit.tag == "NPC")
            {
                other.transform.parent.GetComponent<Health>().TakeDamage(maxDamageInflicted);
            }
            else
            {
                var health = hit.GetComponent<Health>();
                health.TakeDamage(maxDamageInflicted);
            }
        }

        Destroy(this.gameObject);
    }
}
