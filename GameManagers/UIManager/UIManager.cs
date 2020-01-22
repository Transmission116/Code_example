using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
public class UIManager : MonoBehaviour, IServiceInit, IUIManager
{
    [SerializeField] List<UIViewBase> uiViewBase;
    [SerializeField] List<UIPopupBase> uiPopups;
    [SerializeField] Canvas uICanvas;
    [SerializeField] CanvasScaler canvasScaler;

    private Stack<UIViewBase> activeUIViewStack;

    public UIViewBase CurrentPage { get; private set; }
    public List<UIViewBase> UiViewBase { get => uiViewBase;}
  
    public CanvasScaler CanvasScaler { get => canvasScaler; }
    public Canvas Canvas { get => uICanvas; }

    void Awake()
    {
        ProfileAssistant.DebugLog("UI Awske");
    }
    private void OnEnable()
    {
        ProfileAssistant.DebugLog("UI eneble");
        ClassContainer.AddService<IUIManager>(this);
    }
    public void Init()
    {
        uiViewBase = new List<UIViewBase>();
        activeUIViewStack = new Stack<UIViewBase>();
        uiPopups = new List<UIPopupBase>();
        //TODO Add PopupClases if need
        foreach (var page in uiViewBase)
            page.Init();
        foreach (var popup in uiPopups)
            popup.Init();
    }

    public void Dispose()
    {
        foreach (var page in uiViewBase)
            page.Dispose();

        foreach (var popup in uiPopups)
            popup.Dispose();
    }



    public void SetPage<T>(bool hideAll = false) where T : IUIViewBase
    {
        if (hideAll)
        {
            HideAllPages();
        }
        foreach (var view in uiViewBase)
        {
            if (view is T)
            {
                CurrentPage = view;
                break;
            }
        }
        CurrentPage.Show();
        activeUIViewStack.Push(CurrentPage);
    }

    public void HideActiveView()
    {
        if (activeUIViewStack.Count != 0)
            activeUIViewStack.Pop().Hide();
    }

    public void DrawPopup<T>(object message = null, bool setMainPriority = false) where T : IUIPopup
    {
        IUIPopup popup = null;
        foreach (var popupItem in uiPopups)
        {
            if (popupItem is T)
            {
                popup = popupItem;
                break;
            }
        }

        if (setMainPriority)
            popup.SetMainPriority();

        if (message == null)
            popup.Show();
        else
            popup.Show(message);
    }

    public void HidePopup<T>() where T : IUIPopup
    {
        foreach (var popup in uiPopups)
        {
            if (popup is T)
            {
                popup.Hide();
                break;
            }
        }
    }

    public void HideAllPages()
    {
        foreach (var view in activeUIViewStack)
        {
            view.Hide();
        }
    }

    public IUIPopup GetPopup<T>() where T : IUIPopup
    {
        IUIPopup popup = null;
        foreach (var _popup in uiPopups)
        {
            if (_popup is T)
            {
                popup = _popup;
                break;
            }
        }
        return popup;
    }

    public IUIViewBase GetPage<T>() where T : IUIViewBase
    {
        IUIViewBase view = null;
        foreach (var item in uiViewBase)
        {
            if (item is T)
            {
                view = item;
                break;
            }
        }
        return view;
    }
}