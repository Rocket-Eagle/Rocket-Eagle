using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SkinDataTest
{
    /*
     * Ensure that converting a Skin object to a SkinData object (something serializable)
     * Doesn't loose any data
     */
    [Test]
    public void SkinDatamize()
    {
        //setup the test
        string skinName = "bird";
        uint skinValue = 0;
        bool purchased = true;

        //initialize test
        Skins fullSkin = new Skins(skinName, skinValue, purchased);//this is the default skin

        //preform test
        SkinData skinData = fullSkin.datamize();

        //assert
        Assert.IsTrue(skinData.GetPreviewImageName().Equals(skinName));
        Assert.IsTrue(skinData.GetCost() == skinValue);
        Assert.IsTrue(skinData.GetIsPurchased() == purchased);

        //cleanup
    }

    /*
     * Ensure that a Skin can be properly made from a SkinData object
     */
    [Test]
    public void SkinTest()
    {
        //setup the test
        string skinName = "bird";
        uint skinValue = 0;
        bool purchased = true;

        //initialize test
        Skins fullSkin = new Skins(skinName, skinValue, purchased);//this is the default skin
        SkinData skinData = fullSkin.datamize();

        //preform test
        Skins newSkin = new Skins(skinData);

        //assert
        Assert.IsTrue(newSkin.GetPreviewImageName().Equals(skinName));
        Assert.IsTrue(newSkin.GetCost() == skinValue);
        Assert.IsTrue(newSkin.GetIsPurchased() == purchased);
        Assert.NotNull(newSkin.GetPreviewImage());

        //cleanup
    }
}
