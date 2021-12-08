using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class EnemySpawner : NetworkBehaviour
{
    public GameObject npcPrefab;
    public int numOfNPCs;

    public override void OnStartServer()
    {
        GameObject plane=GameObject.Find("Plane");
        float size = plane.transform.localScale.x * 5;
        for(int i = 0; i < numOfNPCs; i++)
        {
            float x = Random.RandomRange(-size, size);
            float z = Random.RandomRange(-size, size);

            float rot_y = Random.RandomRange(0, 180);

            var spawnPos = new Vector3(x, .5f, z);
            var spawnRot = Quaternion.Euler(0, rot_y, 0);

            var npc = Instantiate(npcPrefab, spawnPos, spawnRot);

            NetworkServer.Spawn(npc);

        }

    }

}
