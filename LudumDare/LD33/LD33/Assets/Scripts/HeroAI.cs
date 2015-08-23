using UnityEngine;
using System.Collections;

public class HeroAI : MonoBehaviour
{
    public float Terror = 0;
    public float MaxTerror = 10;
    public float MoveSpeed = 1f;
    public float StopDistance = 1f;
    public float FightingCoolDown = 10f;
    public GameObject ProjectilePrefab;
    public GameObject ProjectileSpawnPoint;
    public float CombatTimeout = 5f;

    private Animator _animator;
    private Vector3 _target = Vector3.zero;
    private HeroState _currentHeroState = HeroState.LookingForLitch;
    private bool _facingleft = true;
    private Rigidbody2D _rigidbody2D;
    private GameObject _fightingTaret;
    private float _currentFightCoolDown = 0f;
    private Vector3 _lastPosition = Vector3.zero;
    private bool _clearKinematic = false;
    private float _KinematicTimer = 1f;
    private float _combatTimeout = 10f;

    public enum HeroState
    {
        LookingForLitch,
        Fighting,
        Fleeing,
        Resting
    }

    void Start()
    {
        _currentHeroState = HeroState.LookingForLitch;
        _target = LitchController.Instance.transform.position;

        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ReachedDestination()
    {
        _currentHeroState = HeroState.Resting;
        _target = Vector3.zero;
    }

    public void FixedUpdate()
    {
        if (_combatTimeout > 0f)
        {
            _combatTimeout -= Time.fixedDeltaTime;
        }
        else
        {
            _currentHeroState = HeroState.LookingForLitch;
            _currentFightCoolDown = 0f;
        }

        if (_clearKinematic)
        {
            if (_KinematicTimer > 0f)
                _KinematicTimer -= Time.fixedDeltaTime;
            else
            {
                _rigidbody2D.isKinematic = false;
                _clearKinematic = false;
                _KinematicTimer = 1f;
            }
        }
            

        if (_animator == null)
            return;

        if (_currentHeroState == HeroState.Fighting)
        {
            _animator.SetFloat("Speed", 0);
            if (_currentFightCoolDown <= 0f)
            {
                _currentFightCoolDown = FightingCoolDown;

                if (_facingleft)
                {
                    var projectile = (GameObject)Instantiate(ProjectilePrefab, ProjectileSpawnPoint.transform.position, Quaternion.identity);
                    projectile.GetComponent<ProjectileController>().Velocity *= -1;
                }
                else
                {
                    var projectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.transform.position, Quaternion.identity);
                }

            }
            else
            {
                _currentFightCoolDown -= Time.fixedDeltaTime;
            }
        }

        if (_target == Vector3.zero)
        {
            _animator.SetFloat("Speed", 0f);
            return;
        }

        if (Vector3.Distance(_target, transform.position) <= StopDistance)
        {
            ReachedDestination();
            return;
        }

        var movex = transform.position.x;
        var movey = transform.position.y;

        if (_currentHeroState == HeroState.Fleeing || _currentHeroState == HeroState.LookingForLitch)
        {
            if (_lastPosition != Vector3.zero && _lastPosition == transform.position)
            {
                _rigidbody2D.isKinematic = true;
                _clearKinematic = true;
                _KinematicTimer = 1f;
            }

            _lastPosition = transform.position;

            if (_target.x > transform.position.x)
            {
                if (_facingleft)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                        transform.localScale.z);

                    _facingleft = false;
                }

                movex = transform.position.x + (MoveSpeed * Time.fixedDeltaTime);
            }

            if (_target.x < transform.position.x)
            {
                if (!_facingleft)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                        transform.localScale.z);

                    _facingleft = true;
                }

                movex = transform.position.x - (MoveSpeed * Time.fixedDeltaTime);
            }

            _rigidbody2D.MovePosition(new Vector2(movex, movey));
            _animator.SetFloat("Speed", MoveSpeed);
        }

    }

    public void AITriggered(GameObject obj)
    {
        if (obj.tag == "Unit")
        {
            _currentHeroState = HeroState.Fighting;
            _fightingTaret = obj;
        }
    }

    public void AITargetLost(GameObject obj)
    {
        _currentHeroState = HeroState.LookingForLitch;
        _target = LitchController.Instance.transform.position;
        _currentFightCoolDown = 0f;
    }

    public void AITargetUpdate(GameObject obj)
    {
        if (obj.tag == "Unit")
        {
            _currentHeroState = HeroState.Fighting;
            _combatTimeout = CombatTimeout;
        }
    }

}
