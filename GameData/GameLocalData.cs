

public class GameLocalData: IServiceInit, ISaveEntity
{
    public GameLocalData()
    {
        ResetData();
    }

    public Language Language { get; set ; }

    public void Init()
    {
        ClassContainer.GetService<SaveDataManager>().AddSaveEntity(this);
    }
    public void Dispose()
    {
        
    }

    public void SetSaveData()
    {
        ClassContainer.GetService<SaveDataManager>().SetSaveData(Language);
    }
    public void GetSaveData()
    {
        Language = ClassContainer.GetService<SaveDataManager>().GetdData<Language>();
    }



    public void ResetData()
    {

    }


}
