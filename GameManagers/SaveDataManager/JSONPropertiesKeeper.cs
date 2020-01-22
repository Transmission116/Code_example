using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class JSONPropertiesKeeper<T>: IPropertiesKeeper<T>
{
    private static string path;
    public JSONPropertiesKeeper(string _path)
    {
        path = _path;
    }

    public T Load()
    {
        T loadObject = default;
        if (!File.Exists(path)) return loadObject;
        using (var streamReader = new StreamReader(path))
        {
            var serializedObject = streamReader.ReadToEnd();
            loadObject = JsonUtility.FromJson<T>(serializedObject);
            return loadObject;
        }
    }

    public void Save(T data)
    {
        string playerToJson = JsonUtility.ToJson(data, true);
        path = Path.Combine(path, typeof(T).ToString());
        Save(playerToJson);
    }

    private void Save(string data)
    {
        using (var stream = new FileStream(path, FileMode.Create))
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(data);
            writer.Close();
        }
    }
}
