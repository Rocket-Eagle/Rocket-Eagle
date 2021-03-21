using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SingleTimer : MonoBehaviour
{

    Text time;
    public float timeElapsed = 0.0f;
    GameObject player;
    SinglePlayerController bird;
    
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
        }
        
    }
}
