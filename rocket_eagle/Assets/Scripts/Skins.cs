using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

/*
 * Class that holds a skin (yes, only one) and the data that goes along with it
 */

[Serializable]
public class Skins
{
    //ALL SKIN IMAGES MUST RESIDE IN: Assets/Resources/Skins
    private const string SKIN_PATH = "Resources/Skins/";

    //It sounds like you can't save Images, so we save the skin data (with the image name as the id) and then save that to use
    private Image previewImage;

    //save the data needed
    private string imageName;
    private uint cost;
    private bool isPurchased;

    /*
     * constructor to load in the skin data and then manually load the image
     */
    public Skins(string theImageName, uint theCost, bool isPurchased)
    {
        imageName = theImageName;
        cost = theCost;
        this.isPurchased = isPurchased;

        //manually load in the image 
        //ALL SKIN IMAGES MUST RESIDE IN: Assets/Resources/Skins
        string skinPath = SKIN_PATH + theImageName;
        UnityEngine.Object theImage = Resources.Load(skinPath, typeof(Sprite));
        previewImage = theImage as Image;
    }

    /*
     * constructor to load in the skin data using an image
     */
    public Skins(Image thePreviewImage, uint theCost, bool isPurchased)
    {
        previewImage = thePreviewImage;
        imageName = thePreviewImage.name;
        cost = theCost;
        this.isPurchased = isPurchased;
    }

    public uint GetCost()
    {
        return cost;
    }

    public bool GetIsPurchased()
    {
        return isPurchased;
    }

    public string GetPreviewImageName()
    {
        return imageName;
    }

    public UnityEngine.Object GetPreviewImage()
    {
        return previewImage;
    }
}
