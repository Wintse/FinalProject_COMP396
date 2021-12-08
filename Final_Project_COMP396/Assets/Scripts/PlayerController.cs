using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    //bullet prefab
    [SerializeField]
    float speed = 15; // m/s
    GameObject[] playersArray;
    [SerializeField]
    float angularSpeed = 120; // deg/s  (3 sewc fopr a full turn=360 deg => 120 deg/s)

    //New as of Nov29th
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed=30; // m/s
    public float bulletLife = 2; //sec
    public bool mainPlayer;
    public Image waitingScreen;

    public override void OnStartLocalPlayer()
    {
        // base.OnStartLocalPlayer();
        //GetComponent<Renderer>().material.color = Color.blue;
        mainPlayer = true;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isLocalPlayer)
        {
            return; //we handleonly local player commands
        }

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
            CmdFire();
        }


    }

    [Command]
    private void CmdFire()
    {
        //throw new System.NotImplementedException();
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletSpeed;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, bulletLife);
        

    }
}
