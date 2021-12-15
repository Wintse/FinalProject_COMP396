using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public int maxDamageInflicted = 20;
    public GameObject enemyHealth;
    Transform topparent;
    public GameObject collectEffect;
    GameObject effect;
    private void Start()
    {
       
    }
    private void Update()
    {
        if (collectEffect)
        {
            effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            
        }
    }
    void destroyEffect()
    {
        Destroy(this.effect);
    }
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
        destroyEffect();
        Destroy(this.gameObject);
    }
}
