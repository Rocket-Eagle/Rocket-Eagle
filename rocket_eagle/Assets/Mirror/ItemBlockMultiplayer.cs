using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockMultiplayer : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

            if (other.GetComponent<BirdItemMultiplayer>().HeldItem == -1 && other.GetComponent<BirdItemMultiplayer>().CanPickup)
            {
                other.GetComponent<BirdItemMultiplayer>().StartPickup();
                StartCoroutine(Respawn(3f));

                //play sound
                audioSource.PlayOneShot(audioSource.clip);
                //audioSource.Play();
            }
    }

    public IEnumerator Respawn(float timeToRespawn)
    {
        transform.position += new Vector3(0,100,0);
        yield return new WaitForSeconds(timeToRespawn);
        transform.position -= new Vector3(0, 100, 0);
    }
}


