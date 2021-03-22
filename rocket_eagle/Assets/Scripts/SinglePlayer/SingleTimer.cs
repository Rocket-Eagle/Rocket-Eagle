using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class SingleTimer : MonoBehaviour
{

    Text time;
    public float timeElapsed = 0.0f;
    GameObject player;
    SinglePlayerController bird;
    private bool addedBirdCoin = false;//make sure the birdcoin is only added once
    
    // Start is called before the first frame update
    void OnEnable()
    {
        player = GameObject.Find("singlebird");
        time = GetComponent<Text>();
        time.text = "T i m e : ";
        bird = player.GetComponent<SinglePlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bird.finished)
        {
            timeElapsed += Time.deltaTime;
            time.text = "T i m e : " + timeElapsed.ToString("F2");
        } //this function give a larger results (more birdcoin) for smaller time values and smaller results (less birdcoin) for large time values
        else
        {
            if (!addedBirdCoin)
            {

                Debug.Log("Time it took to for match:" + timeElapsed);
                float logVal = Mathf.Log(timeElapsed);

                //so C# is studip and doesn't handle a conversion from double to uint
                //so I have to go from double to int to uint
                double bc = Math.Pow(1.0f / 2.0f, logVal) * 400;

                int bcValue = (int)bc;

                uint bcValueActual = (uint)bcValue;

                //uint bc = 100;
                //now that we have the amount of birdcoin the user has earned, update the file that stores the amount
                SaveGameData.UpdatePlayerCoin(bcValueActual);

                //we have now added the birdcoin so make sure we don't do it again
                addedBirdCoin = true;
            }
        }

    }
}
