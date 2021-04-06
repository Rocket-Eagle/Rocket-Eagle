using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

/*
 * Class that holds a skin (yes, only one) and the data that goes along with it
 */

public class Skins
{
    //ALL SKIN IMAGES MUST RESIDE IN: Assets/Resources/Skins
    private const string SKIN_PATH = "Skins/";

    //It sounds like you can't save Images, so we save the skin data (with the image name as the id) and then save that to use
    private Sprite previewImage;

    //save the data needed
    private string imageName;
    private uint cost;
    private bool isPurchased;

    /*
     * construct a skin using a SkinData Object
     */
    public Skins(SkinData skinData)
    {
        imageName = skinData.GetPreviewImageName();
        cost = skinData.GetCost();
        isPurchased = skinData.GetIsPurchased();

        //manually load in the image 
        loadSkinImage(imageName);
    }

    /*
     * constructor to load in the skin data and then manually load the image
     */
    public Skins(string theImageName, uint theCost, bool isPurchased)
    {
        imageName = theImageName;
        cost = theCost;
        this.isPurchased = isPurchased;

        //manually load in the image 
        loadSkinImage(imageName);
    }

    public override string ToString()
    {
        return imageName;
    }

    /*
     * constructor to load in the skin data using an image
     */
    public Skins(Sprite thePreviewImage, uint theCost, bool isPurchased)
    {
        previewImage = thePreviewImage;
        imageName = thePreviewImage.name;
        cost = theCost;
        this.isPurchased = isPurchased;
    }

    private void loadSkinImage(string theImageName)
    {
        //Assets/Resources/Skins/bird.png
        //ALL SKIN IMAGES MUST RESIDE IN: Assets/Resources/Skins
        string skinPath = SKIN_PATH + theImageName;

        //HOLY COW! THIS IS SO DUMB. IF YOU TRY TO DO THIS WITH AN IMAGE, YOU DON'T GET ANY COMPILE ERRORS AND THE ONLY RUNTIME ERROR YOU GET IS A NULL
        //THIS IS SO DUMB
        UnityEngine.Object theImage = Resources.Load(skinPath, typeof(Sprite));
        
        if (theImage == null)
        {
            Debug.Log("DIDN'T FIND IMAGE");
        }
        previewImage = (theImage as Sprite);
    }

    /*
     * datamizes the skins
     * that is, only basic primatives can be stored in a binary file (easily) so we have to take the
     * data that makes up a skin and convert it to primatives so we can save the data
     */
    public SkinData datamize()
    {
        return new SkinData(this);
    }

    /*
     * preform a 'purchase' (unlock the skin so that it can be selected)
     * this function does not check that there is enough BirdCoin to unlock
     * the skin since this class does not have access to the wallet
     */
    public void unlockSkin()
    {
        isPurchased = true;
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

    public Sprite GetPreviewImage()
    {
        return previewImage;
    }

    public bool Equals(Skins skin1)
    {
        if(skin1.imageName.Equals(imageName) && skin1.isPurchased == isPurchased && skin1.cost == cost)
        {
            return true;
        }

        return false;
    }
}
