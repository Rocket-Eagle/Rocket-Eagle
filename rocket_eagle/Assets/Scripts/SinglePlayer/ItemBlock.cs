using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<BirdItem>().HeldItem == -1 && other.GetComponent<BirdItem>().CanPickup)
            {
                other.GetComponent<BirdItem>().StartPickup();
                StartCoroutine(Respawn(3f));
            }
        }
    }

    public IEnumerator Respawn(float timeToRespawn)
    {
        transform.position += new Vector3(0,100,0);
        yield return new WaitForSeconds(timeToRespawn);
        transform.position -= new Vector3(0, 100, 0);
    }
}


