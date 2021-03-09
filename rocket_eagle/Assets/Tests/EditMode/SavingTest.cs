using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SavingTest
{
    /*
     * Test to make sure that the number of skins are saved correctly
     */
    [Test]
    public void SavingSkinsCount()
    {
        //initialize test
        Skins[] skinsToSave = new Skins[3];
        skinsToSave[0] = new Skins("bird", 0, true);//this is the default skin
        skinsToSave[1] = new Skins("blueBird", 150, false);
        skinsToSave[2] = new Skins("fireBird", 200, false);

        //preform the action
        SaveGameData.SaveSkinCount(skinsToSave.Length);

        //now preform the test
        int loadedSkins = SaveGameData.LoadSkinCount();
        //assert
        Assert.IsTrue(loadedSkins == skinsToSave.Length);
    }

    /*
     * Test to handle saving of the skins that can be purchased
     */
    [Test]
    public void SavingSkins()
    {
        //initialize test
        Skins[] skinsToSave = new Skins[3];
        skinsToSave[0] = new Skins("bird", 0, true);//this is the default skin
        skinsToSave[1] = new Skins("blueBird", 150, false);
        skinsToSave[2] = new Skins("fireBird", 200, false);

        //preform the action
        SaveGameData.SaveSkins(skinsToSave);

        //now preform the test
        Skins[] loadedSkins = SaveGameData.LoadSkins();
        for(int i = 0; i < skinsToSave.Length; i++)
        {
            //assert
            Assert.IsTrue(loadedSkins[i].Equals(skinsToSave[i]));
        }
    }


    /*
     * Test to make sure that the players BirdCoin saves properly
     */
    [Test]
    public void SavingCoin()
    {
        //initialize test
        uint playerCoin = 222;

        //preform the action
        SaveGameData.SavePlayerCoin(playerCoin);

        //now preform the test
        uint loadedCoin = SaveGameData.LoadPlayerCoin();

        //assert
        Assert.IsTrue(playerCoin == loadedCoin);
    }


    /*
     * Test to make sure that the saving of a selected skin works properly
     */
    [Test]
    public void SavingSelectedSkin()
    {
        //initialize test
        Skins selectedSkin = new Skins("bird", 111, false);

        //preform the action
        SaveGameData.SaveSelectedSkin(selectedSkin);

        //now preform the test
        Skins loadedSkin = SaveGameData.LoadSelectedSkin();

        //assert
        Assert.IsTrue(selectedSkin.Equals(loadedSkin));
    }
}
