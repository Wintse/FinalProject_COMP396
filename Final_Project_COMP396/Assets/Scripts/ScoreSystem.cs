using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreSystem : NetworkBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public static int i = 0;
    [SyncVar]
    public int totalScore=i;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        TakeScore(totalScore);
    }
    [ClientRpc]
    void RpcScoree(int score)
    {
        text.text = score.ToString();
    }

    public void TakeScore(int amount)
    {
        if (!isServer)
            return;

      
        RpcScoree(amount);
    }

}
