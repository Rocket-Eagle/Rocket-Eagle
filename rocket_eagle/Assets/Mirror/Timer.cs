using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{

    Text time;
    public float timeElapsed = 0.0f;
    GameObject player;
    BirdController bird;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("LocalBird");
        bird = player.GetComponent<BirdController>();
        if (!bird.finished)
        {
            timeElapsed += Time.deltaTime;
            time.text = "T i m e : " + timeElapsed.ToString("F2");
        }
        else
        {
            //the game is finished so take the current time and use it to give the player birdcoin
             
            //this function give a larger results (more birdcoin) for smaller time values and smaller results (less birdcoin) for large time values
            uint bc =(uint) Mathf.Pow(1.0f / 2.0f, Mathf.Log(timeElapsed)) * 300;

            //now that we have the amount of birdcoin the user has earned, update the file that stores the amount
            SaveGameData.UpdatePlayerCoin(bc);
        }
        
    }
}
