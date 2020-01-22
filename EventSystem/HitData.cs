using UnityEngine;
using System.Collections;

public struct HitData {
    [SerializeField] GameObject EventObject;
    [SerializeField] string Tag;
    [SerializeField] Collider2D Collider;

    public GameObject EventObject1 { get => EventObject; set => EventObject = value; }
    public string Tag1 { get => Tag; set => Tag = value; }
    public Collider2D Collider1 { get => Collider; set => Collider = value; }
}
