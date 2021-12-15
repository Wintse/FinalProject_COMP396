using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject[] allEnemies;
    public int xEnemies = 3;
    public int yEnemies = 5;
    public int zEnemies = 5;
     int level2 = 5;
    NPCController nPCController;
    string tag;
    public AudioClip collectSound;
    int count = 0;
    bool pressF;
    // Start is called before the first frame update
    void Start()
    {
        tag = this.gameObject.tag;


    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("x " + xEnemies);
        if (this.gameObject.tag == "DoorX")
        {
            DoorsMove(xEnemies);
            
        }
        // Debug.Log("y " + yEnemies);
        if (this.gameObject.tag == "DoorY")
        {
            DoorsMove(yEnemies);

        }
        //  Debug.Log("z "+ zEnemies);
        if (this.gameObject.tag == "DoorZ")
        {
            DoorsMove(zEnemies);
        }
        if(Input.GetKeyDown(KeyCode.F))
         {
            pressF = true;
        }
        if(this.gameObject.tag == "level2door")
        {
            if (CountdownTimer.playerOnPlatform && CountdownTimer.door==1)
            {
                if (pressF)
                {
                    Debug.Log("Press F");
                        Destroy(this.gameObject);
                    pressF = false;
                }
            }
        }
        if (this.gameObject.tag == "level3door")
        {
            if (CountdownTimer.playerOnPlatform && CountdownTimer.door == 2)
            {
                if (pressF)
                {
                    Debug.Log("Press F");
                    Destroy(this.gameObject);
                    pressF = false;
                }
            }
        }
        if (this.transform.position.y > 40)
        {
            Destroy(this.gameObject);
        }
    }
    void DoorsMove(int type)
    {
        if (type <= 0)
        {
            this.transform.position += new Vector3(0, 4 * Time.deltaTime, 0);
            
        }
    }
    void AddEnemiesToLists()
    {
        count = -1;
        allEnemies = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject enemy in allEnemies)
        {
            nPCController = enemy.GetComponent<NPCController>();
            //if (nPCController.enemyType == EnemyType.x) { xEnemies.Add(enemy); }
            //if (nPCController.enemyType == EnemyType.y) { yEnemies.Add(enemy); }
            //if (nPCController.enemyType == EnemyType.z) { zEnemies.Add(enemy); }
        }
    }
}
