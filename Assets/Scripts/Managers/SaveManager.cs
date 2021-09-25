using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager 
{
    public static string directory = "/SaveFiles/";
    public static string fileName = "SaveProfile.txt";
   public static void Save(SaveDataObject data)
   {
        string path = Application.persistentDataPath + directory;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string jsonRep = JsonUtility.ToJson(data);

        File.WriteAllText(path + fileName, jsonRep);
        

   }

    public static SaveDataObject Load()
    {
        string path = Application.persistentDataPath + directory + fileName;
        SaveDataObject loadedData =new SaveDataObject(); //instance which stores all the data loaded from the json format

        

        if (File.Exists(path))
        {
            string getJsonData = File.ReadAllText(path);
            loadedData = JsonUtility.FromJson<SaveDataObject>(getJsonData);
        }
        else
        {
            Debug.LogError("Path Doesn't Exist");
            return null;
        }

        return loadedData;
    }
}
