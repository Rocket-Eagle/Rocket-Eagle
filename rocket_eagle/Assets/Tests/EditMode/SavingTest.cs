using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SavingTest
{
    /*
     * Test to handle saving of the skins that can be purchased (checks true)
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

        //cleanup
        SaveGameData.DeleteAllSkins();
    }

    /*
     * Test to handle saving of the skins that can be purchased (checks false)
     */
    [Test]
    public void SavingSkinsInverse()
    {
        //initialize test
        Skins[] skinsToSave = new Skins[3];
        skinsToSave[0] = new Skins("bird", 0, true);//this is the default skin
        skinsToSave[1] = new Skins("blueBird", 150, false);
        skinsToSave[2] = new Skins("fireBird", 200, false);

        //preform the action
        SaveGameData.SaveSkins(skinsToSave);

        //change the skin order so that they won't match
        skinsToSave = new Skins[3];
        skinsToSave[2] = new Skins("blueBird", 150, false);
        skinsToSave[0] = new Skins("fireBird", 200, false);
        skinsToSave[1] = new Skins("bird", 0, true);//this is the default skin

        //now preform the test
        Skins[] loadedSkins = SaveGameData.LoadSkins();
        for (int i = 0; i < skinsToSave.Length; i++)
        {
            //assert
            Assert.IsFalse(loadedSkins[i].Equals(skinsToSave[i]));
        }

        //cleanup
        SaveGameData.DeleteAllSkins();
    }


    /*
     * Test to make sure that the players BirdCoin saves properly (checks true)
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

        //cleanup
        SaveGameData.DeletePlayerCoin();
    }

    /*
     * Test to make sure that updating the players birdcoin works properly (checks true)
     */
    [Test]
    public void UpdatingCoin()
    {
        //initialize test
        uint playerCoin = 222;

        //first try if there is no 
        SaveGameData.SavePlayerCoin(playerCoin);

        //now preform the test
        uint loadedCoin = SaveGameData.LoadPlayerCoin();

        //assert
        Assert.IsTrue(playerCoin == loadedCoin);

        //cleanup
        SaveGameData.DeletePlayerCoin();
    }

    /*
     * Test to make sure that the saving of a selected skin works properly (checks true)
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

        //cleanup
        SaveGameData.DeleteSelectedSkin();
    }

    /*
     * Test to make sure that the saving of a selected skin works properly (checks false)
     */
    [Test]
    public void SavingSelectedSkinInverse()
    {
        //initialize test
        Skins selectedSkin = new Skins("bird", 111, false);

        //preform the action
        SaveGameData.SaveSelectedSkin(selectedSkin);

        //locally change the skin so that the price doesn't match
        selectedSkin = new Skins("bird", 11, false);

        //now preform the test
        Skins loadedSkin = SaveGameData.LoadSelectedSkin();

        //assert
        Assert.IsFalse(selectedSkin.Equals(loadedSkin));
        //cleanup
        SaveGameData.DeleteSelectedSkin();


        //now check that if the skin is different it fails
        selectedSkin = new Skins("bird", 111, false);

        //preform the action
        SaveGameData.SaveSelectedSkin(selectedSkin);

        //locally change the skin so that the price doesn't match
        selectedSkin = new Skins("fireBird", 111, false);

        //now preform the test
        loadedSkin = SaveGameData.LoadSelectedSkin();

        //assert
        Assert.IsFalse(selectedSkin.Equals(loadedSkin));
        //cleanup
        SaveGameData.DeleteSelectedSkin();


        //finally, check that if the purchase flag is different that they don't match
        selectedSkin = new Skins("bird", 111, false);

        //preform the action
        SaveGameData.SaveSelectedSkin(selectedSkin);

        //locally change the skin so that the price doesn't match
        selectedSkin = new Skins("bird", 111, true);

        //now preform the test
        loadedSkin = SaveGameData.LoadSelectedSkin();

        //assert
        Assert.IsFalse(selectedSkin.Equals(loadedSkin));
        //cleanup
        SaveGameData.DeleteSelectedSkin();
    }


}
