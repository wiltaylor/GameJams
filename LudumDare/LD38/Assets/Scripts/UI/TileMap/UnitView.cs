using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    public int X;
    public int Y;
    public GUID Id;

    private Animator _animator;
    private SpriteRenderer _render;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
    }

    public void Fall()
    {
        if(_animator != null)
            _animator.SetTrigger("KillTile");
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

}
