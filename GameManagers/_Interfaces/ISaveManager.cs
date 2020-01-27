using System;
public interface ISaveManager
{
    void RegisterSaveEntity(ISaveEntity saveEntity);
    void SaveData();
    void LoadData();
}