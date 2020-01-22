using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProfileAssistant {

    public static string PropertiesPath;
    void Initialize()
    {
        PropertiesPath = Application.persistentDataPath;
    }

#if UNITY_EDITOR
    [MenuItem("Utilites/Data/Delete PlayerPrefs")]
    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        DirectoryInfo dataDir = new DirectoryInfo(Application.persistentDataPath);
        dataDir.Delete(true);
        DebugLog("Delete Player Data Successful");
    }

#endif
    #region Resources Data Load

    public static T LoadObjFromRes<T>(string path) where T : UnityEngine.Object 
    {
        var obj = Resources.Load<T>(path);

        return obj as T;
    }


    public static void LoadListFromRes<T>(ref List<T> ml_List, string path) where T : UnityEngine.Object
    {
        T[] obj = Resources.LoadAll<T>(path);

        for (int i = 0; i < obj.Length; i++)
        {
            ml_List.Add(obj[i]);
        }
    }
    #endregion
    #region JSON Data defenition

    public static void SaveArrayFile<T>(T[] array,string fileName)
    {
        string playerToJson = JsonHelper.ToJson(array,true);
        Save(playerToJson, fileName);
    }


    public static void SaveToFile<T>(object obj, string filename)
    {
        string playerToJson = JsonUtility.ToJson(obj,true);
        Save(playerToJson, filename);
    }


    public static T[] LoadArrayFile<T>(string fileName)
    {
        if(Load(fileName) == null)return null;
        return JsonHelper.FromJson<T>(Load(fileName));
    }

    public static object LoadDataFromFile<T>(string fileName)
    {
        if (Load(fileName) == null) return null; 
        return JsonUtility.FromJson<T>(Load(fileName));
    }

    private static string Load(string fileName)
    {
        string path = PropertiesPath + "/" + fileName;
        if (!File.Exists(path)) return null;

        using (var stream = new FileStream(path, FileMode.Open))
        {
            StreamReader reader = new StreamReader(stream);
            var data =  reader.ReadToEnd();
            reader.Close(); 
            return data;
        }
    }

    private static void Save(string data, string fileName)
    {
        PropertiesPath = Application.persistentDataPath;
        string path = PropertiesPath +"/"+ fileName+".txt";
        Debug.Log(path);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(data);
            writer.Close();
        }
    }
    #endregion


    #region PlayerPrefs Utility defenition

    public static void SaveInt(string key, int val)
    {
        PlayerPrefs.SetInt(key,val);
    }

    public static int LoadInt(string key, int defaultVal = 0)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, defaultVal);
        }
        return PlayerPrefs.GetInt(key);
    }


    public static void SaveFloat(string key, float val)
    {
        PlayerPrefs.SetFloat(key,val);
    }

    public static float LoadFloat(string key, float defaultVal = 0f)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, defaultVal);
        }
        return PlayerPrefs.GetFloat(key);
    }

    public static void SaveString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
    }

    public static string LoadString(string key, string defaultVal = "")
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, defaultVal);
        }
        return PlayerPrefs.GetString(key);

    }

    public static void SaveBool(string key, bool val)
    {
        PlayerPrefs.SetInt(key, val?1:0);
    }

    public static bool LoadBool(string key, bool defaultVal = false)
    {

        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, defaultVal?1:0);
        }
        return PlayerPrefs.GetInt(key)==1;
    }

    public static void SaveDate(string key, DateTime dateTime)
    {
        PlayerPrefs.SetString(key,dateTime.ToString("G"));
    }
    public static DateTime LoadDate(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, DateTime.Now.ToString("G"));
        }
        return DateTime.Parse(PlayerPrefs.GetString(key));
    }


    #endregion

    #region Log Utility defenition
    public static void DebugLog(object message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#else
            SaveLog(message);
#endif
    }

    public static void DebugError(object message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
#else
            SaveLog("Error: " + message);
#endif
    }

    public static void DebugWarning(object message)
    {
#if UNITY_EDITOR
        Debug.LogWarning(message);
#else
            SaveLog("Warning: " + message);
#endif
    }
    private static void AddLog(object message)
    {
        File.AppendAllText(Path.Combine(Application.persistentDataPath, Application.identifier+"_GameLogs.txt"), message.ToString() + "\n");
    }
    #endregion

}
