using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPCControl : MonoBehaviour
{
    public GameObject player;
    public Transform[] path;
    private FSMSystem fsm;


    public void SetTransition(Transition t) { fsm.PerformTransition(t); }


    // Start is called before the first frame update
    void Start()
    {


        //Debug.Log(this.transform.position);
        //Debug.Log(this.transform.localPosition * 2f);

        //path[0].position = this.transform.localPosition + new Vector3(3f, 0f, 0f);
        //path[1].position = this.transform.localPosition + new Vector3(-3f, 0f, 0f);

        //path[0].position = new Vector3 (this.transform.position.x + 2f, this.transform.position.y, this.transform.position.z);
        //path[1].position = this.transform.position + new Vector3(-3f, 0f, 0f);

        //Debug.Log(this.transform.localPosition + new Vector3(3f, 0f, 0f));
        //Debug.Log(path[0].position);
        //Debug.Log(path[1].position);

        //Debug.Log(new Vector3(this.transform.position.x + 2f, this.transform.position.y, this.transform.position.z));


        

        MakeFSM();
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);
    }

    private void MakeFSM()
    {
        FollowPathState follow = new FollowPathState(path);
        follow.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);

        ChasePlayerState chase = new ChasePlayerState();
        chase.AddTransition(Transition.LostPlayer, StateID.FollowingPath);

        fsm = new FSMSystem();
        fsm.AddState(follow);
        fsm.AddState(chase);
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
        // If the Player passes less than 15 meters away in front of the NPC
        RaycastHit hit;
        if (Physics.Raycast(npc.transform.position, npc.transform.forward, out hit, 15F))
        {
            if (hit.transform.gameObject.tag == "Player")
                //Debug.Log("hit player");
                npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        //Debug.Log("following path");
        // Follow the path of waypoints
        // Find the direction of the current way point 
        Vector3 vel = npc.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = waypoints[currentWayPoint].position - npc.transform.position;

        if (moveDir.magnitude < 1) //so we are at this waypoint; go to the next
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length)
            {
                currentWayPoint = 0;
            }
        }
        else
        {
            vel = moveDir.normalized * 10;

            // Rotate towards the waypoint
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                      Quaternion.LookRotation(moveDir),
                                                      5 * Time.deltaTime);
            npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);

        }

        // Apply the Velocity
        npc.GetComponent<Rigidbody>().velocity = vel;
    }

} // FollowPathState

public class ChasePlayerState : FSMState
{
    public ChasePlayerState()
    {
        stateID = StateID.ChasingPlayer;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        // If the player has gone 18 meters away from the NPC, fire LostPlayer transition
        if (Vector3.Distance(npc.transform.position, player.transform.position) >= 18)
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
    }

    public override void Act(GameObject player, GameObject npc)
    {
        Debug.Log("chasing player");
        // Follow the path of waypoints
        // Find the direction of the player 		
        Vector3 vel = npc.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = player.transform.position - npc.transform.position;

        // Rotate towards the waypoint
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                  Quaternion.LookRotation(moveDir),
                                                  5 * Time.deltaTime);
        npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);

        vel = moveDir.normalized * 10;

        // Apply the new Velocity
        npc.GetComponent<Rigidbody>().velocity = vel;
    }

} // ChasePlayerState