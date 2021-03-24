using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class SkinData
{
    //save the data needed
    private string imageName;
    private uint cost;
    private bool isPurchased;

    public SkinData(Skins theSkin)
    {
        imageName = theSkin.GetPreviewImageName();
        cost = theSkin.GetCost();
        isPurchased = theSkin.GetIsPurchased();
    }

    public uint GetCost()
    {
        return cost;
    }

    /*
     * returns the isPurchased variable
     */
    public bool GetIsPurchased()
    {
        return isPurchased;
    }

    public string GetPreviewImageName()
    {
        return imageName;
    }
}
