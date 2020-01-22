using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUIManager
{
    Canvas Canvas { get; }
    CanvasScaler CanvasScaler { get;}
    void SetPage<T>(bool hideAll = false) where T : IUIViewBase;
    void DrawPopup<T>(object message = null, bool setMainPriority = false) where T : IUIPopup;
    void HidePopup<T>() where T : IUIPopup;
    IUIPopup GetPopup<T>() where T : IUIPopup;
    IUIViewBase GetPage<T>() where T : IUIViewBase;

    void HideAllPages();
}