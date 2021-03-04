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
    private static string PLAYER_COIN_FILE = "/SavedCoin.saves";

    /*
     * save the skins and there state
     */
    public static void SaveSkins(Skins[] allSkins)
    {
        SkinData[] allSkinData = SerializeSkins(allSkins);
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string skinPath = Application.persistentDataPath + SKIN_FILE;
        FileStream stream = new FileStream(skinPath, FileMode.Create);

        formatter.Serialize(stream, allSkinData[0]);
        stream.Close();
    }

    /*
     * 
     */
    public static Skins[] LoadSkins()
    {
        string skinPath = Application.persistentDataPath + SKIN_FILE;
        if(File.Exists(skinPath))
        {
            //figure out a way to fix this?
            Skins[] allSkins = new Skins[100];

            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(skinPath, FileMode.Open);

            allSkins[0] = new Skins((SkinData) formatter.Deserialize(stream));

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
        SkinData[] skinData = new SkinData[theSkins.Length];

        for(int i = 0; i < theSkins.Length; i++)
        {
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
}
