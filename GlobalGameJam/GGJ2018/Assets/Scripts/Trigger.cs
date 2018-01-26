using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    public string[] InteractWithTags;

    public bool Pressed;
    public float PressTimeOut = 5f;
    private float _currentPressTimeout = 0f;


    void Update()
    {
        if (_currentPressTimeout > 0f)
        {
            _currentPressTimeout -= Time.deltaTime;

            if (_currentPressTimeout < 0f)
            {
                Pressed = false;
                OnRelease.Invoke();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (InteractWithTags.Contains(other.tag))
        {
            Pressed = true;
            OnPress.Invoke();
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (InteractWithTags.Contains(other.tag))
        {
            _currentPressTimeout = PressTimeOut;
        }
    }
}
