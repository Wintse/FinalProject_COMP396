using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class EnemySpawner : NetworkBehaviour
{
    public GameObject npcPrefab;
    public int numOfNPCs;

    NPCController npcController;

    public Transform[] spawnX = new Transform[3];
    public Transform[] spawnY = new Transform[5];
    public Transform[] spawnZ = new Transform[5];

    public override void OnStartServer()
    {
        npcController = npcPrefab.GetComponent<NPCController>();

        GameObject plane = GameObject.Find("Plane");
        float size = plane.transform.localScale.x * 5;
        int spawnCounter = 0;

        for (int i = 0; i < spawnX.Length; i++)
        {
            float x = Random.RandomRange(-size, size);
            float z = Random.RandomRange(-size, size);

            float rot_y = Random.RandomRange(0, 180);

            var spawnPos = new Vector3(x, .5f, z);
            var spawnRot = Quaternion.Euler(0, rot_y, 0);

            npcController.enemyType = EnemyType.x;

            var npc = Instantiate(npcPrefab, spawnX[spawnCounter].transform.position, spawnRot);
            spawnCounter++;

            NetworkServer.Spawn(npc);

        }

        spawnCounter = 0;
        for (int i = 0; i < spawnY.Length; i++)
        {
            Debug.Log("yy");
            float x = Random.RandomRange(-size, size);
            float z = Random.RandomRange(-size, size);

            float rot_y = Random.RandomRange(0, 180);

            var spawnPos = new Vector3(x, .5f, z);
            var spawnRot = Quaternion.Euler(0, rot_y, 0);

            npcController.enemyType = EnemyType.y;

            var npc = Instantiate(npcPrefab, spawnY[spawnCounter].transform.position, spawnRot);
            spawnCounter++;

            NetworkServer.Spawn(npc);

        }

        spawnCounter = 0;
        for (int i = 0; i < spawnZ.Length; i++)
        {
            float x = Random.RandomRange(-size, size);
            float z = Random.RandomRange(-size, size);

            float rot_y = Random.RandomRange(0, 180);

            var spawnPos = new Vector3(x, .5f, z);
            var spawnRot = Quaternion.Euler(0, rot_y, 0);

            npcController.enemyType = EnemyType.z;

            var npc = Instantiate(npcPrefab, spawnZ[spawnCounter].transform.position, spawnRot);
            spawnCounter++;

            NetworkServer.Spawn(npc);

        }

    }

}
