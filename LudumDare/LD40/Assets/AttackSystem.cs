using UnityEngine;
using UnityEngine.Events;

public class AttackSystem : MonoBehaviour
{
    public UnityEvent OnStartAttack;
    public UnityEvent OnStopAttack;
    public UnityEvent OnUp;
    public UnityEvent OnDown;

    public bool HasCompletedDown { get; private set; }


    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        HasCompletedDown = _animator.GetBool("DownCompleted");
    }

    public void SetBool(string paramName)
    {
        _animator.SetBool(paramName, true);
    }

    public void UnSetBool(string paramName)
    {
        _animator.SetBool(paramName, false);
    }

    public void StartAttack()
    {
        OnStartAttack.Invoke();
    }

    public void StopAttack()
    {
        OnStopAttack.Invoke();
    }

    public void Up()
    {
        OnUp.Invoke();
    }

    public void Down()
    {
        OnDown.Invoke();
    }


    public bool IsReady()
    {
        return !HasCompletedDown && _animator.GetBool("isReady");
    }
}
