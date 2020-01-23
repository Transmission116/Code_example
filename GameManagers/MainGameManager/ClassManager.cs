public class ClassManager : ClassContainer
{
    internal ClassManager()
    {

        //TODO Add Not Behavior Clases
        ProfileAssistant.DebugLog("Create Contructor");        

        TimerManager timerManager = new TimerManager();
        ScreenOrientationManager screenOrientationManager = new ScreenOrientationManager();
        InputManager inputManager = new InputManager();
        SaveDataManager saveDataManager = new SaveDataManager();
        GameLocalData gameLocalData = new GameLocalData();
        AddService<ITimerManager>(timerManager);
        AddService<IScreenOrientationManager>(screenOrientationManager);
        AddService<IInputManager>(inputManager);
        AddService<ISaveManager>(saveDataManager);
        AddService<GameLocalData>(gameLocalData);

        AddUpdateService<ITimerManager>(timerManager);
        AddUpdateService<IScreenOrientationManager>(screenOrientationManager);
        AddUpdateService<IInputManager>(inputManager);

    }


}