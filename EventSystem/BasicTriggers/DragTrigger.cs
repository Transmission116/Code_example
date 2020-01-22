using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;

public class DragTrigger : BasicTrigger, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] string onBeginDrag;
    [SerializeField] string onDrag;
    [SerializeField] string onEndDrag;

    [SerializeField] float FromX = -2.4f;
    [SerializeField] float ToX = 2.4f;
    [SerializeField] float FromY = -4f;
    [SerializeField] float ToY = 4f;

    [HideInInspector]
    protected Vector3 Delta;

    public virtual void OnDrag(PointerEventData data)
    {
        Vector2 moveDelta = data.delta / (Screen.height / (Camera.main.orthographicSize * 2));
        Vector3 objPosition = gameObject.transform.position;

        objPosition.x = FromX.Equals(ToX) ? transform.position.x : Math.Min(Math.Max(FromX, transform.position.x + moveDelta.x), ToX);
        objPosition.y = FromY.Equals(ToY) ? transform.position.y : Math.Min(Math.Max(FromY, transform.position.y + moveDelta.y), ToY);

        Delta = objPosition - gameObject.transform.position;

        gameObject.transform.position = objPosition;

        eventData.DragDelta1 = Delta;
        ExecuteEvent(onDrag);
    }

    public virtual void OnBeginDrag(PointerEventData data)
    {
        ExecuteEvent(onBeginDrag);
    }

    public virtual void OnEndDrag(PointerEventData data)
    {
        ExecuteEvent(onEndDrag);
    }

    protected override void ExecuteEvent(string eventName)
    {
        base.ExecuteEvent(eventName);
        if (CallRecipient != null)
        {
            CallRecipient.GetComponent<MonoBehaviour>().SendMessage("DisableHighLight", null, SendMessageOptions.DontRequireReceiver);
        }
    }

}
