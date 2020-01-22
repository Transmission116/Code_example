using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainEngine : MonoBehaviour
{
    public delegate void MainAppDelegate(object param);
    public event MainAppDelegate OnLevelWasLoadedEvent;

    public event Action LateUpdateEvent;
    public event Action FixedUpdateEvent;
    public static MainEngine Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ClassManager classManager = new ClassManager();
    }

    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void Start()
    {
        ClassContainer.InitServices();
    }

    private void Update()
    {
        if (Instance == this)
            ClassContainer.Update();
    }

    private void LateUpdate()
    {
        if (Instance == this)
        {
            if (LateUpdateEvent != null)
                LateUpdateEvent();
        }
    }

    private void FixedUpdate()
    {
        if (Instance == this)
        {
            if (FixedUpdateEvent != null)
                FixedUpdateEvent();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            ClassContainer.Dispose();
    }


    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (Instance == this)
        {
            if (OnLevelWasLoadedEvent != null)
                OnLevelWasLoadedEvent(arg0.buildIndex);
        }
    }
}