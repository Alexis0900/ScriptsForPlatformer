
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 

public static class SaveSystem
{
    
    public static void SavePlayer(PlayerCombatController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(filePath, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Saved file at " + filePath);
    }



    public static PlayerData LoadPlayer ()
    {
        string filePath = Application.persistentDataPath + "/player.data";

        if(File.Exists(filePath)){
            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else {

            Debug.LogError("Save file not found in " + filePath);
            return null;
        }

    }


    public static void SaveLevel(LevelManager level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/level.data";
        FileStream stream = new FileStream(filePath, FileMode.Create);

        LevelData data = new LevelData(level);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Saved file at " + filePath);
    }


    public static LevelData LoadLevel ()
    {
        string filePath = Application.persistentDataPath + "/level.data";

        if(File.Exists(filePath)){
            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else {

            Debug.LogError("Save file not found in " + filePath);
            return null;
        }

    }


}
