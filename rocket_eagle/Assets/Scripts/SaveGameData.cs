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

    /*
     * save the skins and there state
     */
    public static void SaveSkins(Skins[] allSkins)
    {
        //setup the stuff for the binary writer
        BinaryFormatter formatter = new BinaryFormatter();
        string skinPath = Application.persistentDataPath + "/SavedSkins.saves";
        FileStream stream = new FileStream(skinPath, FileMode.Create);

        formatter.Serialize(stream, allSkins[0]);
        stream.Close();
    }

    /*
     * 
     */
    public static Skins[] LoadSkins()
    {
        string skinPath = Application.persistentDataPath + "/SavedSkins.saves";
        if(File.Exists(skinPath))
        {
            //figure out a way to fix this?
            Skins[] allSkins = new Skins[100];

            //setup the stuff for the binary reader
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(skinPath, FileMode.Open);

            allSkins[0] = (Skins) formatter.Deserialize(stream);

            stream.Close();

            return allSkins;
        }
        else
        {
            Debug.LogError("Skin save file not found in:" + skinPath);
            return null;
        }

    }
}
