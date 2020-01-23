using System;
public interface ISaveManager
{
    void RegisterSaveClass(ISaveEntity saveEntity);
    void SaveData();
    void LoadData();
}