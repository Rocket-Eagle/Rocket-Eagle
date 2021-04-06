using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdItemMultiplayer : MonoBehaviour
{
    private BirdController Bird;
    private ItemHandler Handle;
    public float DelayBeforeItemPickup = 1;

    public int HeldItem;

    public bool CanPickup;
    private bool UseItem;

    public Item ItmUse;
    void Start()
    {
        ResetItem();
    }

    // Update is called once per frame
    void Update()
    {
        Handle = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemHandler>();
        Bird = GetComponent<BirdController>();
        UseItem = Input.GetMouseButtonDown(1);
        if (UseItem && HeldItem != -1)
        {
            ActivateItem();
        }
    }

    public void ResetItem()
    {
        HeldItem = -1;
        CanPickup = true;
    }

    public void StartPickup()
    {
        StartCoroutine(PickUp());
    }

    public IEnumerator PickUp()
    {
        if (HeldItem == -1 && CanPickup)
        {
            CanPickup = false;
            yield return new WaitForSeconds(DelayBeforeItemPickup);
            int ItemRand = Random.Range(0, Handle.AllItems.Length);

            ItmUse = Handle.AllItems[ItemRand];

            HeldItem = ItemRand;
            if (HeldItem == 2)
            {
                StartCoroutine(Danger());
            }
        }
    } 

    public void ActivateItem()
    {
        if (HeldItem == 0)
        {
            Bird.Boost();
            Bird.PlayAudioClip(Handle.AllItems[0].audioClip);
        }
        else if (HeldItem == 1)
        {
            Bird.Ghost();
            Bird.PlayAudioClip(Handle.AllItems[1].audioClip);
        }
        if (HeldItem != 2)
        {
            ResetItem();
            
        }        
    }

    public IEnumerator Danger()
    {
        yield return new WaitForSeconds(4);
        Bird.PlayAudioClip(Handle.AllItems[2].audioClip);
        Bird.Restart();
        ResetItem();
    }
}
