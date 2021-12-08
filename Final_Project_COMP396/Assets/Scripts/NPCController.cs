using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum EnemyType { x,y,z};

public class NPCController : NetworkBehaviour
{
    //bullet prefab
    public float speed = 1; // m/s
    public float angularSpeed = 60; // deg/s  (3 sewc fopr a full turn=360 deg => 120 deg/s)
    public EnemyType enemyType;
    

    //New as of Nov29th
    //public GameObject bulletPrefab;
    //public Transform bulletSpawn;
    //public float bulletSpeed=6; // m/s
    //public float bulletLife = 2; //sec


    //Do a FSM to control this NPC

    public override void OnStartLocalPlayer()
    {
        // base.OnStartLocalPlayer();
        //GetComponent<Renderer>().material.color = Color.blue;
    }

    // Start is called before the first frame update
    void Start()
    {
        //enemyType = EnemyType.x;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.x:
                //Debug.Log(enemyType);
                break;
            case EnemyType.y:
                //Debug.Log(enemyType);
                break;
            case EnemyType.z:
                //Debug.Log(enemyType);
                break;
            default:
                break;
        }


    }

}
