using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    GameObject[] playersArray;
    CinemachineFreeLook camera;
    GameObject lookAt;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {



        
    }

    // Update is called once per frame
    void Update()
    {
        

        //playerOnTop = playerController.onVertPlatform; (duplicating boolean)
        playersArray = GameObject.FindGameObjectsWithTag("Player");

        
        foreach (GameObject player in playersArray)
        {
            
            if(player.name == "Frog(Clone)")
            {
                playerController = player.GetComponent<PlayerController>();
                //Debug.Log(playerController.mainPlayer);

                var vcam = GetComponent<CinemachineFreeLook>();
                //Debug.Log(playerController.mainPlayer);
                if (playerController.mainPlayer)
                {
                    lookAt = player.transform.GetChild(7).gameObject;
                    vcam.GetComponent<Cinemachine.CinemachineFreeLook>().Follow = player.transform;
                    vcam.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = lookAt.transform;

                }
            }
            

        }

    }
}
