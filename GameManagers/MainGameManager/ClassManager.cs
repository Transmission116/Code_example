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
        AddService<ITimerManager>(timerManager);
        AddService<IScreenOrientationManager>(screenOrientationManager);
        AddService<IInputManager>(inputManager);
        AddService<ISaveManager>(saveDataManager);

        AddUpdateService<ITimerManager>(timerManager);
        AddUpdateService<IScreenOrientationManager>(screenOrientationManager);
        AddUpdateService<IInputManager>(inputManager);
    }


}