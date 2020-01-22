using System;
using System.ComponentModel;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly object _lock = new object();
    private static bool applicationIsQuitting;

    private static T instance;

    public static T Instance {
        get {
            if (applicationIsQuitting) {
                Debug.LogWarning(
                    "[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock) {
                if (instance == null) {
                    instance = (T) FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1) {
                        Debug.LogError(
                            "[Singleton] Something went really wrong - there should never be more than 1 singleton!");
                        return instance;
                    }

                    if (instance == null) {
                        GameObject singleton = null;

                        if (Attribute.IsDefined(typeof(T), typeof(PrefabAttribute))) {
                            var attribute =
                                (PrefabAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(PrefabAttribute));
                            var prefabName = attribute.Name;
                            try {
                                if (!string.IsNullOrEmpty(prefabName))
                                    singleton = (GameObject) Instantiate(Resources.Load(prefabName, typeof(GameObject)));
                            }
                            catch (Exception e) {
                                Debug.LogError(
                                    "[Singleton] Could not instantiate prefab even though prefab attribute was set: " +
                                    e.Message + "\n" + e.StackTrace);
                            }
                        }

                        if (singleton == null) singleton = new GameObject();

                        instance = singleton.GetComponent<T>() ?? singleton.AddComponent<T>();
                        singleton.name = typeof(T) + " (Singleton)";
                        DontDestroyOnLoad(singleton);

                        (instance as Singleton<T>).OnInstanceCreated();

                        Debug.Log(
                            "[Singleton] An instance of " + typeof(T) + " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    } else {
                        Debug.Log("[Singleton] Using instance already created: " + instance.gameObject.name);
                    }
                }

                return instance;
            }
        }
    }


    public virtual void OnDestroy() {
        applicationIsQuitting = true;
    }

    protected virtual void OnInstanceCreated() {}
}