

public interface IScreenOrientationManager
{
    void SwitchOrientation(ScreenOrientationMode mode);
    ScreenOrientationMode GetOrientation();
}