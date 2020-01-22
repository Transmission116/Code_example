using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class ClickUpTrigger : BasicTrigger, IPointerUpHandler
{
	[FormerlySerializedAs("CallName")]
	[SerializeField] string OnClick;
	public virtual void OnPointerUp(PointerEventData eventDat) {
		ExecuteEvent(OnClick);
	}
	protected override void ExecuteEvent (string eventName)
	{
		base.ExecuteEvent (eventName);
		if (CallRecipient != null) {
			CallRecipient.GetComponent<MonoBehaviour>().SendMessage("DisableHighLight", null, SendMessageOptions.DontRequireReceiver);		
		}
	}
}
