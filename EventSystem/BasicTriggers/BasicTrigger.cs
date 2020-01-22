using System.Reflection;
using UnityEngine;

public class BasicTrigger : MonoBehaviour {

    [SerializeField] protected GameObject CallRecipient;
    [SerializeField] protected string Tag;

    protected EventData eventData;

    protected virtual void ExecuteEvent(string eventName) {
        if (CallRecipient != null) {
            eventData.EventObject1 = gameObject;
            eventData.Tag1 = Tag;
			CallRecipient.GetComponent<MonoBehaviour>().SendMessage(eventName, eventData, SendMessageOptions.DontRequireReceiver);
        }
    }
}