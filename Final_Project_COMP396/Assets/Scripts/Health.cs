using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;

    //Newer implementations use templates
    //NetworkVariable<int> currentHealth

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    // Start is called before the first frame update

    //UI related
    public RectTransform healthBar;

    public void TakeDamage(int damage)
    {

        if (!this.isServer)
        {
            return;
        }

        currentHealth -= damage;
        if (currentHealth <=0)
        {
            currentHealth = maxHealth;
            RpcReSpawn();
            //Debug.Log("Player" + this.gameObject.GetInstanceID() + " is dead!");
        }
        Debug.Log("Player" + this.gameObject.GetInstanceID() + ": Health=" + currentHealth);

    }

    [ClientRpc]
    private void RpcReSpawn()
    {
        //throw new NotImplementedException();
        transform.position = Vector3.zero;
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }
}
