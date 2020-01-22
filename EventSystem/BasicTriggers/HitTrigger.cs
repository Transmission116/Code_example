using UnityEngine;
using System.Collections;

public class HitTrigger : BasicTrigger{

	[SerializeField] string onTriggerEnter;
    [SerializeField] string onTriggerStay;
    [SerializeField] string onTriggerExit;

	private HitData hitData;

	void OnTriggerEnter2D(Collider2D other) {
		ExecuteEvent (onTriggerEnter, other);
	}

	void OnTriggerExit2D(Collider2D other) {
		ExecuteEvent (onTriggerExit, other);
	}

	void OnTriggerStay2D(Collider2D other) {
		ExecuteEvent (onTriggerStay, other);
	}

	void ExecuteEvent(string eventName, Collider2D collider) {
		if (CallRecipient != null) {
			hitData.EventObject1 = gameObject;
			hitData.Tag1 = Tag;
			hitData.Collider1 = collider;
			CallRecipient.SendMessage(eventName, hitData, SendMessageOptions.DontRequireReceiver);
		}
	}
}
