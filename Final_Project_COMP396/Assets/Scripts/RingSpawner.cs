using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RingSpawner : NetworkBehaviour
{
    // Start is called before the first frame update

    public GameObject ringPrefab;
    public int numOfRIngs;
    public Transform[] spawnX = new Transform[3];


    public override void OnStartServer()
    {


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


            var ring = Instantiate(ringPrefab, spawnX[spawnCounter].transform.position, spawnRot);
            spawnCounter++;

            NetworkServer.Spawn(ring);
        }

    }
}
