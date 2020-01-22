using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace GameKit
{
    public class EnforceTypeAttribute : PropertyAttribute
    {
        public System.Type type;

        public EnforceTypeAttribute(System.Type _enforcedType)
        {
            type = _enforcedType;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EnforceTypeAttribute))]
    public class PrettyListDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            EnforceTypeAttribute _propAttribute = attribute as EnforceTypeAttribute;
            EditorGUI.BeginProperty(_position, _label, _property);

            MonoBehaviour _obj = EditorGUI.ObjectField(_position, _property.objectReferenceValue, typeof(MonoBehaviour), true) as MonoBehaviour;
            if (_obj != null && _propAttribute.type.IsAssignableFrom(_obj.GetType()) && !EditorGUI.showMixedValue)
            {
                _property.objectReferenceValue = _obj as MonoBehaviour;
            }
            EditorGUI.EndProperty();
        }
    }
#endif 


   
}