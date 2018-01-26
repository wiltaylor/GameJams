using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PushPlate : MonoBehaviour
{
    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    public string[] InteractWithTags;

    public bool Pressed;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (InteractWithTags.Contains(other.tag))
        {
            Pressed = true;
            OnPress.Invoke();
            _animator.SetBool("Pressed", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (InteractWithTags.Contains(other.tag))
        {
            Pressed = false;
            OnRelease.Invoke();
            _animator.SetBool("Pressed", false);
        }
    }
}
