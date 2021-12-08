using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    public float levelTime = 50f;
    bool timerRunning = true;
    GameObject[] playersArray;
    public Image waitingScreen;
    public Image victoryScreen;
    public PlayerController playerController;

    public Text countdownText;
    //Start is called before the first frame update
    void Start()
    {
        currentTime = levelTime;
        countdownText.text = currentTime.ToString("0");
        victoryScreen.enabled = false;
    }

    //Update is called once per frame
    void Update()
    {
        playersArray = GameObject.FindGameObjectsWithTag("Player");
        
        if (playersArray.Length == 1)
        {
            Time.timeScale = 0;
            waitingScreen.enabled = true;
            //Debug.Log("HERRREEE");
            //this.transform.position = new Vector3(this.transform.position.x,this.transform.position.;
        }
        if (playersArray.Length > 1 && timerRunning)
        {
            Time.timeScale = 1;
            waitingScreen.enabled = false;
            currentTime -= 1 * Time.deltaTime;
            countdownText.GetComponent<Text>().text = currentTime.ToString("0");
        }
        if (currentTime < 0)
        {
            timerRunning = false;
            countdownText.text = "Game Over";
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Triggered");
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player");
            timerRunning = false;
            countdownText.text = currentTime.ToString("0") + " You Win";
            victoryScreen.enabled = true;
        }
    }
}
