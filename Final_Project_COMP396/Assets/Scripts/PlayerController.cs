using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public Text text;
    public int totalRIng = 0;
    //bullet prefab
    [SerializeField]
    float speed = 15; // m/s
    GameObject[] playersArray;
    [SerializeField]
    float angularSpeed = 120; // deg/s  (3 sewc fopr a full turn=360 deg => 120 deg/s)
    public static bool onTheFloor = true;
    //New as of Nov29th
    public GameObject bulletPrefab, head;
    public Transform bulletSpawn;
    public float bulletSpeed = 30; // m/s
    public float bulletLife = 2; //sec
    public bool mainPlayer;
    public Image waitingScreen;
    public static bool isHost;
    public static Vector3 pos;
    public AudioClip collectSound;
    public GameObject collectEffect;
    GameObject player2;
    public override void OnStartLocalPlayer()
    {
        // base.OnStartLocalPlayer();
        head.GetComponent<MeshRenderer>().material.color = Color.blue;
        mainPlayer = true;
        pos = this.transform.position;
       gameObject.transform.GetChild(8).tag = "Local";

    }
    // Start is called before the first frame update
    void Start()
    {
        if (NetworkServer.connections.Count > 0)
        {
            Debug.Log("This is the host.");
            isHost = true;

            //this.gameObject.tag = "Player1";
        }
        else
        {
            isHost = false;

            Debug.Log("This is a client.");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isLocalPlayer)
        {
            return; //we handleonly local player commands
        }
        text.text = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSystem>().text.text;
        player2 = GameObject.FindGameObjectWithTag("player2");
        player2 = player2.transform.parent.gameObject;
        player2.GetComponent<PlayerController>().text.text = this.text.text;
        //local player; handle inputs
        var h = Input.GetAxis("Horizontal"); // h is in [-1,1]
        var v = Input.GetAxis("Vertical");   // v is in [-1,1]
        transform.Rotate(0, h * Time.deltaTime * angularSpeed, 0);//abrupt rotation
        transform.Translate(0, 0, v * Time.deltaTime * speed);
        //float f = Input.GetAxis("Fire1");
        //if (f > 0.9) { 
        //   print("f=" + f);
        //}
        //if (Input.GetAxis("Fire1"))
        //{

        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collectSound)
                AudioSource.PlayClipAtPoint(collectSound, bulletSpawn.position);
            CmdFire();
          
        }
    }
 
    [Command]
    private void CmdFire()
    {
        //throw new System.NotImplementedException();
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        
        bullet.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletSpeed;
        
       
        // NetworkServer.SpawnWithClientAuthority(bullet, connectionToClient);
        NetworkServer.Spawn(bullet);

        Destroy(bullet, bulletLife);
    }
}
