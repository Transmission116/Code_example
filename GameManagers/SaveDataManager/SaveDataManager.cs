using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class SaveDataManager: IServiceInit, ISaveManager
{

    private string saveFolderPath;
    private List<ISaveEntity> saveEntityHolder;

    public void Init()
    {
        
        saveEntityHolder = new List<ISaveEntity>();
        saveFolderPath = Path.Combine(Application.streamingAssetsPath, Constants.SAVE_DATA_FOLDER);
    }
    public void Dispose()
    {
        
    }

    public void SaveData()
    {
        foreach (var item in saveEntityHolder)
        {
            item.SetSaveData();
        }
    }

    public void LoadData()
    {
        foreach (var item in saveEntityHolder)
        {
            item.GetSaveData();
        }
    }

    public void AddSaveEntity(ISaveEntity saveEntity)
    {
        saveEntityHolder.Add(saveEntity);
    }

    public T GetdData<T>()
    {
        T loadObject = default;
        JSONPropertiesKeeper<T> jSONPropertiesKeeper = new JSONPropertiesKeeper<T>(saveFolderPath);
        loadObject = jSONPropertiesKeeper.Load();
        return loadObject;
    }

    public void SetSaveData<T>(T data)
    {
        JSONPropertiesKeeper<T> jSONPropertiesKeeper = new JSONPropertiesKeeper<T>(saveFolderPath);
        jSONPropertiesKeeper.Save(data);
    }
}
