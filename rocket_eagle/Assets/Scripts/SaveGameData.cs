using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*
 * this is (hopefully) the class that will save all important player data
 * This will need to be updated:
 *      - at the end of a game
 *      - when an item (skin) is purchased
 */

public static class SaveGameData
{
    //the names of the files where the respective data is stored
    //NOTE: please add to the deleteAllFiles method if you are adding to this list
    private static string SELECTED_SKIN_FILE = "/SelectedSkin.saves";
    private static string SELECTED_LEVEL_FILE = "/SelectedLevel.saves";
    private static string ALL_SKINS_FILE = "/SavedSkins.saves";
    private static string SKIN_COUNT_FILE = "/SavedSkinsCount.saves";
    private static string PLAYER_COIN_FILE = "/SavedCoin.saves";
    private static string UPDATE_COIN_FILE = "/UpdateCoin.saves";//I hate that i have to do this, but this is the file that saves the amount of birdcoin that was just won through finishing a game

    //this is the path where the files above reside in the device (this works with mobile and desktop)
    private static string PATH = Application.persistentDataPath;



    /*
     * 
     * -------------------------------SKIN SAVING FUNCTIONS--------------------------------------
     * 
     * 
     */



    /*
     * save the skins and there state
     */
    public static void SaveSkins(Skins[] allSkins)
    {
        SkinData[] allSkinData = SerializeSkins(allSkins);

        if (allSkinData == null)
        {
            Debug.Log("allSkinData is null, aborting save");
            return;
        }

        //before saving the skins, save the count
        SaveSkinCount(allSkins.Length);

        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string skinPath = PATH + ALL_SKINS_FILE;
        FileStream stream = new FileStream(skinPath, FileMode.Create);

        //go through all the skins and save them
        for (int i = 0; i < allSkinData.Length; i++)
        {
            formatter.Serialize(stream, allSkinData[i]);
        }
        stream.Close();
    }


    /*
     * 
     */
    public static Skins[] LoadSkins()
    {
        //before loading the skins, load in how many skins there are 
        int arraySize = LoadSkinCount();

        string skinPath = PATH + ALL_SKINS_FILE;
        if(File.Exists(skinPath))
        {
            //figure out a way to fix this?
            Skins[] allSkins = new Skins[arraySize];

            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(skinPath, FileMode.Open);

            //load in every skin
            for (int i = 0; i < arraySize; i++)
            {
                SkinData theSkin = (SkinData)formatter.Deserialize(stream);
                allSkins[i] = new Skins(theSkin);
            }

            stream.Close();

            return allSkins;
        }
        else
        {
            Debug.Log("Skin save file not found in:" + skinPath);
            return null;
        }
    }

    /*
     * Serialize a list of skins (saving image data is both hard and redudent since we already have the images)
     * 
     */
    public static SkinData[] SerializeSkins(Skins[] theSkins)
    {
        if(theSkins == null)
        {
            Debug.Log("ERROR, Skins[] is null");
            return null;
        }

        SkinData[] skinData = new SkinData[theSkins.Length];

        for(int i = 0; i < theSkins.Length; i++)
        {
            if(theSkins[i] == null)
            {
                Debug.Log("ERROR, there is a null skin in theSkins[]");
                return null;
            }

            skinData[i] = theSkins[i].datamize();
        }

        return skinData;
    }

    /*
     * Serialize a single skin
     */
    public static SkinData SerializeSkins(Skins theSkins)
    {
        if (theSkins == null)
        {
            Debug.Log("ERROR, Skins[] is null");
            return null;
        }

        SkinData skinData = theSkins.datamize();

        return skinData;
    }

    /*
    * save the amount of skins that are in the file
    * this is private as it gets called automatically when saving skins
    */
    private static void SaveSkinCount(int arraySize)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string path = PATH + SKIN_COUNT_FILE;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, arraySize);
        stream.Close();
    }

    /*
     * returns the amount of the skins that should be saved in the file
     * this is private as it gets called automatically when loading the skins
     */
    private static int LoadSkinCount()
    {
        string path = PATH + SKIN_COUNT_FILE;
        if (File.Exists(path))
        {
            int arraySize = 0;

            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            arraySize = (int)formatter.Deserialize(stream);

            stream.Close();

            return arraySize;
        }
        else
        {
            Debug.Log("BirdCoin save file not found in:" + path);
            return 0;
        }
    }

    /*
     * save the currently selected skin the player is using
     * This file will be read in BirdController
     */
    public static void SaveSelectedSkin(Skins theSelectedSkin)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string path = PATH + SELECTED_SKIN_FILE;
        FileStream stream = new FileStream(path, FileMode.Create);

        SkinData ss = SerializeSkins(theSelectedSkin);

        formatter.Serialize(stream, ss);
        stream.Close();
    }

    /*
     * load the currently selected skin by the player
     */
    public static Skins LoadSelectedSkin()
    {
        string path = PATH + SELECTED_SKIN_FILE;
        if (File.Exists(path))
        {
            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SkinData ss = (SkinData)formatter.Deserialize(stream);

            stream.Close();

            return new Skins(ss);
        }
        else
        {
            Debug.Log("Selected save file not found in:" + path);
            return null;
        }
    }



    /*
     * 
     * -------------------------------COIN SAVING FUNCTIONS--------------------------------------
     * 
     * 
     */



    /*
     * save the players birdcoin
     */
    public static void SavePlayerCoin(uint coinAmount)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string coinPath = PATH + PLAYER_COIN_FILE;
        FileStream stream = new FileStream(coinPath, FileMode.Create);

        formatter.Serialize(stream, coinAmount);
        stream.Close();
    }

    /*
     * returns the amount of the players coin according to what was in the file
     */
    public static uint LoadPlayerCoin()
    {
        string coinPath = PATH + PLAYER_COIN_FILE;
        if (File.Exists(coinPath))
        {
            uint coinAmount = 0;

            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(coinPath, FileMode.Open);

            coinAmount = (uint)formatter.Deserialize(stream);

            stream.Close();

            return coinAmount;
        }
        else
        {
            Debug.Log("BirdCoin save file not found in:" + coinPath);
            return 0;
        }
    }

    /*
     * save the amount of birdcoin the player just earned (from the updateBirdCoin())
     * this method should never be called outside this class (hence private)
     */
    private static void SaveUpdateCoin(uint coinAmount)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string coinPath = PATH + UPDATE_COIN_FILE;
        FileStream stream = new FileStream(coinPath, FileMode.Create);

        formatter.Serialize(stream, coinAmount);
        stream.Close();
    }

    /*
     * returns the amount of birdcoin the player won in the last game
     * 
     * the ONLY use for this function is to display to the user how much birdcoin the player just earned in the last game
     * If you are reading this and thinking it has any other use, you are wrong. use the UpdatePlayerCoin()
     */
    public static uint LoadUpdateCoin()
    {
        string coinPath = PATH + UPDATE_COIN_FILE;
        if (File.Exists(coinPath))
        {
            uint coinAmount = 0;

            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(coinPath, FileMode.Open);

            coinAmount = (uint)formatter.Deserialize(stream);

            stream.Close();

            return coinAmount;
        }
        else
        {
            Debug.Log("UpdateCoin save file not found in:" + coinPath);
            return 0;
        }
    }


    /*
     * The purpose of this function is to provide a way to update the birdcoin of the player when they finish a game
     * Since we are using multiple scenes, we cannot access the class that stores the birdcoin (PurchaseSkin.cs)
     * 
     * This function works by calling the LoadPlayerCoin() to get the current amount of birdcoin the player has
     * @param addtionalCoin is then added to that loaded amount (ideally this should be a positive number, and it is a uint)
     * Then this function calls SavePlayerCoin() to save the new amount. This means that when the player goes to
     * the customization menu the amount will be correct
     */
    public static void UpdatePlayerCoin(uint additionalCoin)
    {
        //load in the balance the player had
        uint previousBalance = LoadPlayerCoin();

        //save the added coin so that it can be used later to display it to the user
        SaveUpdateCoin(additionalCoin);

        uint newBalance = previousBalance + additionalCoin;

        //save the new balance
        SavePlayerCoin(newBalance);
    }


    /*
     * 
     * -------------------------------SELECTED LEVEL FUNCTIONS--------------------------------------
     * 
     * 
     */


    /*
     * save the level the player selected
     */
    public static void SaveSelectedLevel(string levelName)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string levelPath = PATH + SELECTED_LEVEL_FILE;
        FileStream stream = new FileStream(levelPath, FileMode.Create);

        formatter.Serialize(stream, levelName);
        stream.Close();
    }

    /*
     * returns the name of the level the player last selected, or null if the file doesn't exist
     */
    public static string LoadSelectedLevel()
    {
        string levelPath = PATH + SELECTED_LEVEL_FILE;
        if (File.Exists(levelPath))
        {
            string selectedLevel = "THIS IS NOT A GOOD STRING!";

            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(levelPath, FileMode.Open);

            selectedLevel = (string)formatter.Deserialize(stream);

            stream.Close();

            return selectedLevel;
        }
        else
        {
            Debug.Log("Selected level save file not found in:" + levelPath);
            return null;
        }
    }




    /*
     * 
     * -------------------------------DELETING SAVES FUNCTIONS--------------------------------------
     * 
     * 
     */



    /*
     * This method deletes all the saved files
     * 
     * THIS FUNCTION SHOULD ALMOST NEVER BE CALLED. IT IS BASICALLY JUST FOR TESTING PURPOSES AND POSSIBLY UNINSTALLING
     */
    public static void DeleteAllData()
    {
        Debug.Log("WARNING, REMOVING ALL SAVE DATA");
        DeleteAllSkins();
        DeletePlayerCoin();
        DeleteSelectedSkin();
        DeleteSelectedLevel();
    }

    /*
     * This method deletes the selected skin file the saved files
     * 
     * THIS FUNCTION SHOULD ALMOST NEVER BE CALLED. IT IS BASICALLY JUST FOR TESTING PURPOSES
     */
    public static void DeleteSelectedSkin()
    {
        try
        {
            File.Delete(PATH + SELECTED_SKIN_FILE);
        }
        catch (System.Exception)
        {
            Debug.Log("Selected skin file not found during delete, ignoring");
        }
        
    }

    /*
     * This method deletes the selected level file the saved files
     * 
     * THIS FUNCTION SHOULD ALMOST NEVER BE CALLED. IT IS BASICALLY JUST FOR TESTING PURPOSES
     */
    public static void DeleteSelectedLevel()
    {
        try
        {
            File.Delete(PATH + SELECTED_LEVEL_FILE);
        }
        catch (System.Exception)
        {
            Debug.Log("Selected level file not found during delete, ignoring");
        }

    }

    /*
     * This method deletes the all the skins that are save AND the savedSkin count file the saved files
     * 
     * THIS FUNCTION SHOULD ALMOST NEVER BE CALLED. IT IS BASICALLY JUST FOR TESTING PURPOSES
     */
    public static void DeleteAllSkins()
    {
        try
        {
            File.Delete(PATH + ALL_SKINS_FILE);
        }
        catch (System.Exception)
        {
            Debug.Log("All skins file not found during delete, ignoring");
        }
        try
        {
            File.Delete(PATH + SKIN_COUNT_FILE);
        }
        catch (System.Exception)
        {
            Debug.Log("skin count file not found during delete, ignoring");
        }
        
    }

    /*
     * This method deletes the saved players Birdcoin count file the saved files
     * 
     * THIS FUNCTION SHOULD ALMOST NEVER BE CALLED. IT IS BASICALLY JUST FOR TESTING PURPOSES
     */
    public static void DeletePlayerCoin()
    {
        try
        {
            File.Delete(PATH + PLAYER_COIN_FILE);
        }
        catch (System.Exception)
        {
            Debug.Log("Player coin file file not found during delete, ignoring");
        }
        try
        {
            File.Delete(PATH + UPDATE_COIN_FILE);
        }
        catch (System.Exception)
        {
            Debug.Log("Update coin file file not found during delete, ignoring");
        }
    }
}
