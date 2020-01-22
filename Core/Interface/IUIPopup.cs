using UnityEngine;

public interface IUIPopup
{
    GameObject Self { get; }

    void Init();
    void Show();
    void Show(object data);
    void Hide();
    void Dispose();
    void SetMainPriority();
}