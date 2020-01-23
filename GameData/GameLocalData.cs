

public class GameLocalData: IServiceInit, ISaveEntity
{

    public GameLocalData()
    {
        ResetData();
    }

    public Language LanguageType { get; set ; }

    public void Init()
    {
        ISaveManager saveManager = ClassContainer.GetService<ISaveManager>();
        saveManager.RegisterSaveClass(this);
    }
    public void Dispose()
    {
        
    }

    public void SetSaveData(SaveDataManager saveManager)
    {
        saveManager.SetSaveData(LanguageType);
    }
    public void GetSaveData(SaveDataManager saveManager)
    {
        LanguageType = saveManager.GetData<Language>();
    }



    public void ResetData()
    {

    }


}
