using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;

public class SwipeTrigger : BasicTrigger, IDragHandler, IBeginDragHandler, IEndDragHandler{

	[SerializeField] string OnSwipe;
	private SwipeData swipeData;

	protected Vector2 beginDragPosition;

	private float swipeThreshold = 0.5f;

	public virtual void OnDrag(PointerEventData data){
	
	}
	
	public virtual void OnBeginDrag(PointerEventData data){
		beginDragPosition = data.position;
	}
	
	public virtual void OnEndDrag(PointerEventData data){
		Vector3 delta = (data.position - beginDragPosition)/(Screen.height / (Camera.main.orthographicSize*2));
		if (BiggerThenThreshold (delta)) {
			ExecuteEvent(OnSwipe, GetDirection(delta));		
		}
	}
	

	private bool BiggerThenThreshold(Vector2 delta){
		return (Mathf.Abs(delta.x)>=swipeThreshold || Math.Abs(delta.y)>=swipeThreshold);	
	}

	protected SwipeData.Direction GetDirection(Vector2 delta){
		if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) {
			if(delta.x > 0) return SwipeData.Direction.RIGHT;
			else return SwipeData.Direction.LEFT;
		} else {
			if(delta.y > 0) return SwipeData.Direction.UP;
			else return SwipeData.Direction.DOWN;
		}
	}
	
	protected virtual void ExecuteEvent (string eventName, SwipeData.Direction direction)
	{
		if (CallRecipient != null) {
			swipeData.EventObject1 = gameObject;
			swipeData.Tag1 = Tag;
			swipeData.Direction1 = direction;
			CallRecipient.GetComponent<MonoBehaviour>().SendMessage(eventName, swipeData, SendMessageOptions.DontRequireReceiver);
		}
		if (CallRecipient != null) {
			CallRecipient.GetComponent<MonoBehaviour>().SendMessage("DisableHighLight", null, SendMessageOptions.DontRequireReceiver);		
		}
	}
}
