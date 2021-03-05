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
    private static string SKIN_FILE = "/SavedSkins.saves";
    private static string SKIN_COUNT_FILE = "/SavedSkinsCount.saves";
    private static string PLAYER_COIN_FILE = "/SavedCoin.saves";

    /*
     * save the skins and there state
     */
    public static void SaveSkins(Skins[] allSkins)
    {
        SkinData[] allSkinData = SerializeSkins(allSkins);

        if (allSkinData == null)
        {
            Debug.LogError("allSkinData is null, aborting save");
            return;
        }

        //before saving the skins, save the count
        SaveSkinCount(allSkins.Length);

        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string skinPath = Application.persistentDataPath + SKIN_FILE;
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

        Debug.Log("Array size loaded in is:" + arraySize);

        string skinPath = Application.persistentDataPath + SKIN_FILE;
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
            Debug.LogError("Skin save file not found in:" + skinPath);
            return null;
        }
    }

    public static SkinData[] SerializeSkins(Skins[] theSkins)
    {
        if(theSkins == null)
        {
            Debug.LogError("ERROR, Skins[] is null");
            return null;
        }

        SkinData[] skinData = new SkinData[theSkins.Length];

        for(int i = 0; i < theSkins.Length; i++)
        {
            if(theSkins[i] == null)
            {
                Debug.LogError("ERROR, there is a null skin in theSkins[]");
                return null;
            }

            skinData[i] = theSkins[i].datamize();
        }

        return skinData;
    }


    /*
     * save the skins and there state
     */
    public static void SavePlayerCoin(uint coinAmount)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string coinPath = Application.persistentDataPath + PLAYER_COIN_FILE;
        FileStream stream = new FileStream(coinPath, FileMode.Create);

        formatter.Serialize(stream, coinAmount);
        stream.Close();
    }

    /*
     * returns the amount of the players coin according to what was in the file
     */
    public static uint LoadPlayerCoin()
    {
        string coinPath = Application.persistentDataPath + PLAYER_COIN_FILE;
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
            Debug.LogError("BirdCoin save file not found in:" + coinPath);
            return 0;
        }
    }


    /*
     * save the amount of skins that are in the file
     */
    public static void SaveSkinCount(int arraySize)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SKIN_COUNT_FILE;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, arraySize);
        stream.Close();
    }

    /*
     * returns the amount of the skins that should be saved in the file
     */
    public static int LoadSkinCount()
    {
        string path = Application.persistentDataPath + SKIN_COUNT_FILE;
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
            Debug.LogError("BirdCoin save file not found in:" + path);
            return 0;
        }
    }
}
