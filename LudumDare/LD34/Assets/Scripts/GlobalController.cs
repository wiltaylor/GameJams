using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance { private set; get;}

    [HideInInspector]
    public AudioController AudioController;

    [HideInInspector]
    public FactoryController FactoryController;

    [HideInInspector]
    public PlayerController CurrentPlayer;

    public bool HasBreak = false;
    public bool HasSteering = false;
    public bool HasGuns = false;

    public bool GamePaused = false;

    private Queue<string> _messageQueue = new Queue<string>();

    public void AddMessage(string text)
    {
        _messageQueue.Enqueue(text);
    }

    public void ClearMessages()
    {
        _messageQueue.Clear();
    }

    public string ReadMessage()
    {
        if (_messageQueue.Count == 0)
            return null;

        return _messageQueue.Dequeue();
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Instance = this;

        AudioController = GetSubItem<AudioController>();
        FactoryController = GetSubItem<FactoryController>();
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string name)
    {
        GamePaused = false;
        FactoryController.Reset();
        AudioController.Reset();
        ClearMessages();
        SceneManager.LoadScene(name);
    }

    T GetSubItem<T>()
    {
        var ret = GetComponent<T>();

        if (ret == null)
        {
            ret = GetComponentInChildren<T>();
        }

        return ret;
    }
}
