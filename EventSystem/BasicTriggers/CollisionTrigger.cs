using UnityEngine;
using System.Collections;

public class CollisionTrigger : BasicTrigger {
	
	[SerializeField] string onCollisionEnter;
    [SerializeField] string onCollisionStay;
    [SerializeField] string onCollisionExit;
	
	private CollisionData collisionData;
	
	void OnCollisionEnter2D(Collision2D other) {
		ExecuteEvent (onCollisionEnter, other);
	}
	
	void OnCollisionExit2D(Collision2D other) {
		ExecuteEvent (onCollisionExit, other);
	}
	
	void OnCollisionStay2D(Collision2D other) {
		ExecuteEvent (onCollisionStay, other);
	}
	
	void ExecuteEvent(string eventName, Collision2D collision) {
		if (CallRecipient != null) {
			collisionData.EventObject1 = gameObject;
			collisionData.Tag1 = Tag;
			collisionData.Collision1 = collision;
			CallRecipient.SendMessage(eventName, collisionData, SendMessageOptions.DontRequireReceiver);
		}
	}
}
