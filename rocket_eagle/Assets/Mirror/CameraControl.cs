using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraControl : NetworkBehaviour
{


    public Transform player;
    public Vector3 offset;

  

    public override void OnStartLocalPlayer()
    {
        player = GameObject.Find("LocalBird").transform;
    }


    void Update()
    {
        player = GameObject.Find("LocalBird").transform;
        //have a way to get out of the game
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        transform.position = new Vector3(player.position.x + offset.x,0,-10); // Camera follows the player with specified offset position
    }
}
