using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LapCount : MonoBehaviour
{
    // Start is called before the first frame update

    Text lap;
    public int currentLap = 1;
    GameObject player;
    BirdController bird;
    void Start()
    {
        lap = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("LocalBird");
        bird = player.GetComponent<BirdController>();
        lap.text = "L A P : " + bird.lap.ToString();
    }
}
