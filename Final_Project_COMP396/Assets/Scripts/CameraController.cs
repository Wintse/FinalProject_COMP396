using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    GameObject[] playersArray;
    CinemachineFreeLook camera;
    GameObject lookAt;
    public GameObject endPlatform;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        var vcam = GetComponent<CinemachineFreeLook>();
        //vcam.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        vcam.GetComponent<Cinemachine.CinemachineFreeLook>().Follow = endPlatform.transform;
        vcam.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = endPlatform.transform;


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
                var vcam = GetComponent<CinemachineFreeLook>();
                //vcam.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
                playerController = player.GetComponent<PlayerController>();
                //Debug.Log(playerController.mainPlayer);

                
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
