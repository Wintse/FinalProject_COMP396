using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class setPatrolingStatus
{
    public static bool inPatroling = true;

}

[RequireComponent(typeof(Rigidbody))]
public class NPCControl : NetworkBehaviour
{
    public GameObject heartPrefab, gun;
    public GameObject bulletPrefab;

    public Transform bulletSpawn;
    public static float bulletSpeed = 30; // m/s
    public static float bulletLife = 2;
    //sec
    public GameObject player;
    public GameObject[] players;
    public RaycastHit hit1;
    public Transform[] path;
    private FSMSystem fsm;
    private int count = 0;
    public static Vector3 velo;



    PlayerController playerController;
    public void SetTransition(Transition t) { fsm.PerformTransition(t); }

    public void Start()
    {
        MakeFSM();
        playerController = player.GetComponent<PlayerController>();

        // Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
    }

    public void FixedUpdate()
    {
        //player = GameObject.FindWithTag("Player");
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 2)
        {
            players[0].transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.blue;
            // players[0].GetComponent<Text>().text = "You";
            players[1].transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.yellow;
            //  players[1].GetComponent<Text>().text = "Player 2";
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit1, 15F))
            {
                if (hit1.transform.gameObject.tag == "Player" && setPatrolingStatus.inPatroling && hit1.transform.GetChild(1).GetComponent<MeshRenderer>().material.color == Color.yellow)
                {
                    player = players[1];
                }
                else if (hit1.transform.gameObject.tag == "Player" && setPatrolingStatus.inPatroling && hit1.transform.GetChild(1).GetComponent<MeshRenderer>().material.color != Color.yellow)
                {
                    player = players[0];
                    //Debug.Log("Palyer 2 hit");
                }
            }
            fsm.CurrentState.Reason(player, gameObject);
            fsm.CurrentState.Act(player, gameObject);
            if (this.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
            {
                if (Time.frameCount % 20 == 0)
                {
                    CmdFire();
                }
            }

        }
    }

    private void MakeFSM()
    {
        FollowPathState follow = new FollowPathState(path);
        follow.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);
        follow.AddTransition(Transition.LostPlayer, StateID.Idle);
        ChasePlayerState chase = new ChasePlayerState();
        chase.AddTransition(Transition.LostPlayer, StateID.Idle);
        chase.AddTransition(Transition.Attack, StateID.AttackPlayer);

        IdelState idle = new IdelState(path);
        idle.AddTransition(Transition.NoPlayer, StateID.FollowingPath);

        Attack_State attack = new Attack_State();
        attack.AddTransition(Transition.LostPlayer, StateID.Idle);

        fsm = new FSMSystem();
        fsm.AddState(attack);
        fsm.AddState(idle);
        fsm.AddState(follow);
        fsm.AddState(chase);
    }

    public void CmdFire()
    {
        //throw new System.NotImplementedException();

        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, bulletLife);
    }
}


public class FollowPathState : FSMState
{
    private int currentWayPoint;
    private Transform[] waypoints;


    public FollowPathState(Transform[] wp)
    {
        waypoints = wp;
        currentWayPoint = 0;
        stateID = StateID.FollowingPath;
    }

    public override void Reason(GameObject player, GameObject npc)
    {

        RaycastHit hit;
        if (Physics.Raycast(npc.transform.position, npc.transform.forward, out hit, 15F))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                Debug.Log("HitPlayer");

                npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
            }
        }
        if (PlayerController.onTheFloor == false)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }

    }

    public override void Act(GameObject player, GameObject npc)
    {
        setPatrolingStatus.inPatroling = true;
        Vector3 vel = npc.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = waypoints[currentWayPoint].position - npc.transform.position;
        if (moveDir.magnitude < 1)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length)
            {
                currentWayPoint = 0;
            }

        }
        else
        {
            vel = moveDir.normalized * 5;

            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                      Quaternion.LookRotation(moveDir),
                                                      5 * Time.deltaTime);
            npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);

        }
        npc.GetComponent<Rigidbody>().velocity = vel;

    }

}

public class ChasePlayerState : FSMState
{
    public ChasePlayerState()
    {
        stateID = StateID.ChasingPlayer;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Vector3.Distance(npc.transform.position, player.transform.position) >= 18 || PlayerController.onTheFloor == false)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }
        else if (Vector3.Distance(npc.transform.position, player.transform.position) <= 6)
        {

            npc.GetComponent<NPCControl>().SetTransition(Transition.Attack);
        }

    }

    public override void Act(GameObject player, GameObject npc)
    {

        setPatrolingStatus.inPatroling = false;

        Vector3 vel = npc.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = player.transform.position - npc.transform.position;


        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                  Quaternion.LookRotation(moveDir),
                                                  5 * Time.deltaTime);
        npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);

        vel = moveDir.normalized * 6;
        //  Debug.Log("Chasing");
        npc.GetComponent<Rigidbody>().velocity = vel;
    }

}

public class Attack_State : FSMState
{
    bool isFiring = false;
    private int limit = 25;


    public Attack_State()
    {
        stateID = StateID.AttackPlayer;
    }
    public override void Reason(GameObject player, GameObject npc)
    {
        if (Vector3.Distance(npc.transform.position, player.transform.position) >= 18 || PlayerController.onTheFloor == false)
        {
            isFiring = false;
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }
    }
    public override void Act(GameObject player, GameObject npc)
    {
        //Debug.Log("Attacking"+ Vector3.Distance(npc.transform.position, player.transform.position));
        setPatrolingStatus.inPatroling = false;
        Vector3 vel = npc.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = player.transform.position - npc.transform.position;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                  Quaternion.LookRotation(moveDir),
                                                  5 * Time.deltaTime);
        npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);
        if (Vector3.Distance(npc.transform.position, player.transform.position) < 6)
        {
            isFiring = true;
            vel = moveDir.normalized * 0;
            npc.GetComponent<Rigidbody>().velocity = vel;

        }
        else
        {
            vel = moveDir.normalized * 6;
            npc.GetComponent<Rigidbody>().velocity = vel;
        }


    }
}
public class IdelState : FSMState
{
    private int currentWayPoint;
    private Transform[] waypoints;

    public IdelState(Transform[] wp)
    {
        waypoints = wp;
        currentWayPoint = 0;
        stateID = StateID.Idle;
    }
    public override void Reason(GameObject player, GameObject npc)
    {
        if (PlayerController.onTheFloor == true)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.NoPlayer);

        }
    }
    public override void Act(GameObject player, GameObject npc)
    {

        Vector3 vel = npc.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = waypoints[currentWayPoint].position - npc.transform.position;
        vel = moveDir.normalized * 20;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                  Quaternion.LookRotation(moveDir),
                                                  5 * Time.deltaTime);
        npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);

        npc.GetComponent<Rigidbody>().velocity = vel;
        if (moveDir.magnitude < 1)
        {
            npc.GetComponent<Rigidbody>().velocity = vel * 0;

        }
    }

} // ChasePlayerState