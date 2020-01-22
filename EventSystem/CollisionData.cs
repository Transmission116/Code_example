using UnityEngine;
using System.Collections;

public struct CollisionData {
	[SerializeField] GameObject EventObject;
    [SerializeField] string Tag;
    [SerializeField] Collision2D Collision;

    public GameObject EventObject1 { get => EventObject; set => EventObject = value; }
    public string Tag1 { get => Tag; set => Tag = value; }
    public Collision2D Collision1 { get => Collision; set => Collision = value; }
}
