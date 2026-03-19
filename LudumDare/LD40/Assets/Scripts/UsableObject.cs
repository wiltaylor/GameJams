using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class UsableObject : MonoBehaviour
{
    public List<QuestItem> RequiredItems;
    public UnityEvent OnUse;
    public UnityEvent OnTimeout;
    public UnityEvent OnFailItemCheck;

    public float TimeOut;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTimeOut()
    {
        OnTimeout.Invoke();
        Debug.Log("Called it1");
    }

    public void Use(List<QuestItem> items)
    {
        if (_animator != null)
        {
            if(!_animator.GetBool("isReady"))
            {
                return;
            }
        }

        if (RequiredItems != null)
        {
            var itemsFound = 0;
            foreach (var item in RequiredItems)
            {
                if (items.Contains(item))
                    itemsFound++;
            }

            if (itemsFound < RequiredItems.Count)
            {
                OnFailItemCheck.Invoke();

                return;
            }
        }

        OnUse.Invoke();

        if (TimeOut > 0.01f)
        {
            Invoke("OnTimeOut", TimeOut);
        }
    }
}

