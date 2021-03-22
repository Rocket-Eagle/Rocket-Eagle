using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountDown : MonoBehaviour
{

    Text time;
    public float countDown = 3.0f;
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
        //can change so countdown doesn't start until all players ready
        player = GameObject.Find("LocalBird");
        bird = player.GetComponent<BirdController>();
        if (bird.countDown >=0)
        {
            time.text = "Starting in " + bird.countDown.ToString("F2");
        }     else
        {
            time.text = "";
        }
    }
}
