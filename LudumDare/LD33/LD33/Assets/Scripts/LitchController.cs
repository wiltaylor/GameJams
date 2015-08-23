using UnityEngine;
using System.Collections;

public class LitchController : MonoBehaviour {

    private Animator _anim;
    public static LitchController Instance;
    public GameObject ProjectilePrefab;
    public GameObject ProjectileSpawnPoint;
    public float FightingCoolDown = 5f;
    public float CombatTimeout = 10f;
    public GameObject DeathPrefab;

    public int HP = 100;
    public int MaxHP = 100;

    private LitchStates _currentState = LitchStates.Waiting;
    private GameObject _fightingTaret;
    private float _currentFightCoolDown = 0f;
    private float _combatTimeout = 10f;
    private bool _ShootProjectile = false;
    private AudioSource _audio;

    public enum LitchStates
    {
        Waiting,
        Attacking
    }

    public void FixedUpdate()
    {
        if (_combatTimeout > 0f)
        {
            _combatTimeout -= Time.fixedDeltaTime;
        }
        else
        {
            _currentState = LitchStates.Waiting;
        }

        if (_currentState == LitchStates.Attacking)
        {
            if (_currentFightCoolDown <= 0f)
            {
                _currentFightCoolDown = FightingCoolDown;
                CastSpell();
                _ShootProjectile = true;
            }
            else
            {
                _currentFightCoolDown -= Time.fixedDeltaTime;
            }
        }
    }

    public void OnCastUp()
    {
        if (!_ShootProjectile)
            return;

        var projectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.transform.position, Quaternion.identity);
        _ShootProjectile = false;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _anim = gameObject.GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    public void CastSpell()
    {
        _anim.SetTrigger("Attack");
    }

    public void TakeHit(int qty)
    {
        HP -= qty;

        _audio.Play();

        if (HP <= 0)
        {
            if(DeathPrefab != null)
                Instantiate(DeathPrefab, transform.position, transform.rotation);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            GameManager.Instance.GameOver();
        }
    }

    public void AITriggered(GameObject obj)
    {
        if (obj.tag != "Hero")
            return;

        _currentState = LitchStates.Attacking;
    }

    public void AITargetLost(GameObject obj)
    {
        if (obj.tag != "Hero")
            return;

        _currentState = LitchStates.Waiting;
    }

    public void AITargetUpdate(GameObject obj)
    {
        if (obj.tag != "Hero")
            return;

        _currentState = LitchStates.Attacking;
        _combatTimeout = CombatTimeout;
    }

    public void OnSpawn(GameObject room)
    {
        
    }
}
