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
        player = GameObject.Find("bird");
        lap = GetComponent<Text>();
        lap.text = "L A P : ";
        bird = player.GetComponent<BirdController>();

    }

    // Update is called once per frame
    void Update()
    {
        lap.text = "L A P : " + bird.lap.ToString();
    }
}
