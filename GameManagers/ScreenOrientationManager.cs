using UnityEngine;
using UnityEngine.UI;

public class ScreenOrientationManager : IServiceInit, IServiceUpdate, IScreenOrientationManager
{
    private IUIManager uiManager;

    private CanvasScaler uiCanvasScaler;

    private Vector2 portraitReferenceResolution,
                    invertReferenceResolution;

    private ScreenOrientationMode currentOrientation;

    public void Dispose()
    {
    }

    public ScreenOrientationMode GetOrientation()
    {
        return currentOrientation;
    }

    public void Init()
    {
        uiManager = ClassContainer.GetService<IUIManager>();

        uiCanvasScaler = uiManager.CanvasScaler;

        portraitReferenceResolution = uiCanvasScaler.referenceResolution;
        invertReferenceResolution = new Vector2(portraitReferenceResolution.y, portraitReferenceResolution.x);

        currentOrientation = ScreenOrientationMode.Portrait;
    }

    public void Update()
    {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.P))
                SwitchOrientation(ScreenOrientationMode.Portrait);
            else if (Input.GetKeyDown(KeyCode.L))
                SwitchOrientation(ScreenOrientationMode.Landscape);
#endif
    }

    public void SwitchOrientation(ScreenOrientationMode mode)
    {
        Debug.Log(mode);
        switch (mode)
        {
            case ScreenOrientationMode.Portrait:
                uiCanvasScaler.referenceResolution = portraitReferenceResolution;
                Screen.orientation = ScreenOrientation.Portrait;
                currentOrientation = ScreenOrientationMode.Portrait;
                break;
            case ScreenOrientationMode.Landscape:
                uiCanvasScaler.referenceResolution = invertReferenceResolution;
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                currentOrientation = ScreenOrientationMode.Landscape;
                break;

            default: break;
        }
    }
}