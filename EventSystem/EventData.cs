using UnityEngine;
using System.Collections;

public struct EventData
{
    [SerializeField] GameObject EventObject;
    [SerializeField] string Tag;
    [SerializeField] Vector2 DragDelta;

    public GameObject EventObject1 { get => EventObject; set => EventObject = value; }
    public string Tag1 { get => Tag; set => Tag = value; }
    public Vector2 DragDelta1 { get => DragDelta; set => DragDelta = value; }
}
