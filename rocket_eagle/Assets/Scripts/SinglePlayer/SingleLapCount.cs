using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SingleLapCount : MonoBehaviour
{
    // Start is called before the first frame update

    Text lap;
    public int currentLap = 1;
    GameObject player;
    SinglePlayerController bird;
    void Start()
    {
        player = GameObject.Find("singlebird");
        lap = GetComponent<Text>();
        lap.text = "L A P : ";
        bird = player.GetComponent<SinglePlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        lap.text = "L A P : " + bird.lap.ToString();
    }
}
