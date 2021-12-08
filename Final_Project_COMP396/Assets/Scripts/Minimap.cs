using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject mainPlayer;
    GameObject[] playersArray;
    public PlayerController playerController;

    void Start()
    {

    }
    void LateUpdate()
    {
        playersArray = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playersArray)
        {
            if (player.name == "Frog(Clone)")
            {
                mainPlayer = player;
                Vector3 newPosition = mainPlayer.transform.position;
                newPosition.y = transform.position.y;
                transform.position = newPosition;

                transform.rotation = Quaternion.Euler(90f, mainPlayer.transform.eulerAngles.y, 0f);

            }
        }

    }
}
