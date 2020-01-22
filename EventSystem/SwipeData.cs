using UnityEngine;
using System.Collections;

public struct SwipeData {

    [SerializeField] GameObject EventObject;
    [SerializeField] string Tag;
    [SerializeField] Direction direction;

    public GameObject EventObject1 { get => EventObject; set => EventObject = value; }
    public string Tag1 { get => Tag; set => Tag = value; }
    public Direction Direction1 { get => direction; set => direction = value; }

    public enum Direction{UP, RIGHT, DOWN, LEFT, NONE};
}
