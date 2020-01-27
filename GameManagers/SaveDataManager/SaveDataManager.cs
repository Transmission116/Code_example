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

    public void RegisterSaveEntity(ISaveEntity saveEntity)
    {
        saveEntityHolder.Add(saveEntity);
    }

    public void SaveData()
    {
        foreach (var item in saveEntityHolder)
        {
            item.SetSaveData(this);
        }
    }

    public void LoadData()
    {
        foreach (var item in saveEntityHolder)
        {
            item.GetSaveData(this);
        }
    }

    public T GetData<T>()
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
