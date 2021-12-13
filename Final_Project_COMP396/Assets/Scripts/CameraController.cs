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
    public Camera cam;
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
        var vcam = GetComponent<CinemachineFreeLook>();
        if (this.gameObject.tag == "p1 cam")
        {
            Debug.Log("player1 cam");
            if (playersArray.Length == 2)
            {
                lookAt = playersArray[0].transform.gameObject;

                vcam.GetComponent<Cinemachine.CinemachineFreeLook>().Follow = playersArray[0].transform;
                vcam.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = playersArray[0].transform.GetChild(7).gameObject.transform;
              //  cam.rect = new Rect(0, 0.5f, 0.0f, 0);
            }

        }
        else if (this.gameObject.tag == "p2 cam")
        {
            Debug.Log("player2 cam");
            if (playersArray.Length == 2)
            {
                lookAt = playersArray[0].transform.gameObject;

                vcam.GetComponent<Cinemachine.CinemachineFreeLook>().Follow = playersArray[1].transform;
                vcam.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = playersArray[1].transform.GetChild(7).gameObject.transform;
               // cam.rect = new Rect(0, -0.5f, 0f, 0);
            }
        }
        //if (this.tag == "p1 cam")
        //{
        //    cam.rect = new Rect(0, 0.5f, 0.0f, 0);
        //}
        //else if(this.tag == "p2 cam")
        //{
        //    cam.rect = new Rect(0, -0.5f, 0.0f, 0);
        //}
        //foreach (GameObject player in playersArray)
        //{

        //    if (player.name == "Frog(Clone)")
        //    {
        //        var vcam = GetComponent<CinemachineFreeLook>();
        //        //vcam.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        //        playerController = player.GetComponent<PlayerController>();
        //        //Debug.Log(playerController.mainPlayer);
        //        //Debug.Log(playerController.mainPlayer);
        //        if (playerController.mainPlayer)
        //        {
        //            lookAt = player.transform.GetChild(7).gameObject;
        //            vcam.GetComponent<Cinemachine.CinemachineFreeLook>().Follow = player.transform;
        //            vcam.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = lookAt.transform;
        //        }
        //    }
        //}
    }
}
