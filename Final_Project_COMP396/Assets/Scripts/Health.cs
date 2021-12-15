using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;
    DoorController doorController;
    GameObject[] xDoors, yDoors, zDoors;
    public GameObject heartPrefab;
    public Transform heartSpwan;
    //Newer implementations use templates
    //NetworkVariable<int> currentHealth
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    // Start is called before the first frame update

    //UI related
    public RectTransform healthBar;

    private void FixedUpdate()
    {

    }
    public void TakeDamage(int damage)
    {

        if (!this.isServer)
        {
            return;
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (this.tag == "NPC")
            {
               
                NPCController npcController = this.GetComponent<NPCController>();
                if (npcController.enemyType == EnemyType.x)
                {
                    xDoors = GameObject.FindGameObjectsWithTag("DoorX");
                    foreach (GameObject door in xDoors)
                    {
                        doorController = door.GetComponent<DoorController>();
                        doorController.xEnemies -= 1;
                    }
                }
                if (npcController.enemyType == EnemyType.y)
                {
                    yDoors = GameObject.FindGameObjectsWithTag("DoorY");
                    foreach (GameObject door in yDoors)
                    {
                        doorController = door.GetComponent<DoorController>();
                        doorController.yEnemies -= 1;
                    }
                }
                if (npcController.enemyType == EnemyType.z)
                {
                    zDoors = GameObject.FindGameObjectsWithTag("DoorZ");
                    foreach (GameObject door in zDoors)
                    {
                        doorController = door.GetComponent<DoorController>();
                        doorController.zEnemies -= 1;
                    }
                }
                heart();
         
            }
            else
            {
                currentHealth = maxHealth;
                RpcReSpawn();
            }
            //Debug.Log("Player" + this.gameObject.GetInstanceID() + " is dead!");
        }
        Debug.Log("Player" + this.gameObject.GetInstanceID() + ": Health=" + currentHealth);
    }
    void heart()
    {
        Debug.Log("frog");
        var heart = Instantiate(heartPrefab, heartSpwan.position, Quaternion.identity);

        NetworkServer.Spawn(heart);
        Destroy(this.gameObject);
    }
    [ClientRpc]
    private void RpcReSpawn()
    {
        //throw new NotImplementedException();
        transform.position = PlayerController.pos;
    }
    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }
}
