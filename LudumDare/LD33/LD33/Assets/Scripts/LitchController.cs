using UnityEngine;
using System.Collections;

public class LitchController : MonoBehaviour {

    private Animator _anim;
    public static LitchController Instance;

    public int HP = 100;
    public int MaxHP = 100;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _anim = gameObject.GetComponent<Animator>();
    }

    public void CastSpell()
    {
        _anim.SetTrigger("Attack");
    }

    public void TakeHit(int ammount)
    {
        HP -= ammount;
    }
}
