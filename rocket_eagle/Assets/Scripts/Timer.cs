using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{

    Text time;
    public float timeElapsed = 0.0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        time = GetComponent<Text>();
        time.text = "T i m e : ";
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        time.text = "T i m e : " + timeElapsed.ToString("F2");
    }
}
