using System;
using UnityEngine;
using System.Collections;

public class MeeleUnitAI : MonoBehaviour {

    public float MoveSpeed = 1f;
    public float StopDistance = 0.2f;
    public float FightingCoolDown = 3f;
    public GameObject MeeleAttackPrefab;
    public GameObject AttackPoint;

    private bool _facingleft = true;
    private Animator _anim;
    private Rigidbody2D _rigidbody2D;
    private GameObject _target;
    private UnitStates _currentState = UnitStates.Idle;
    private Vector3 _lastPosition = Vector3.zero;
    private bool _clearKinematic = false;
    private float _KinematicTimer = 1f;
    private float _currentFightingCoolDown = 0f;

    public enum UnitStates
    {
        Idle,
        MovingToTarget,
        Fighting,
        Terror
    }

    public void Start()
    {
        _anim = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    public void FixedUpdate()
    {
        if (_currentState == UnitStates.Idle)
        {
            if (_facingleft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                    transform.localScale.z);

                _facingleft = false;
            }

            _anim.SetFloat("Speed", 0f);
            _currentFightingCoolDown = 0;
        }

        if (_currentState == UnitStates.Fighting)
        {
            _anim.SetFloat("Speed", 0f); 
            if (_target == null)
            {
                _currentState = UnitStates.Idle;
                return;
            }
                
            if (_currentFightingCoolDown <= 0f)
            {
                _currentFightingCoolDown = FightingCoolDown;
                var obj = (GameObject)Instantiate(MeeleAttackPrefab, AttackPoint.transform.position, AttackPoint.transform.rotation);

                if (!_facingleft)
                {
                    obj.transform.localScale = new Vector3(obj.transform.localScale.x * -1, obj.transform.localScale.y,
                        obj.transform.localScale.z);
                }
            }
            else
            {
                _currentFightingCoolDown -= Time.fixedDeltaTime;
            }
        }

        if (_currentState == UnitStates.MovingToTarget)
        {
            if (_target == null)
            {
                _currentState = UnitStates.Idle;
                return;
            }

            var movex = transform.position.x;
            var movey = transform.position.y;

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

            if (_lastPosition != Vector3.zero && _lastPosition == transform.position)
            {
                _rigidbody2D.isKinematic = true;
                _clearKinematic = true;
                _KinematicTimer = 1f;
            }

            _lastPosition = transform.position;

            if (_target.transform.position.x > transform.position.x)
            {
                if (_facingleft)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                        transform.localScale.z);

                    _facingleft = false;
                }

                movex = transform.position.x + (MoveSpeed * Time.fixedDeltaTime);
            }

            if (_target.transform.position.x < transform.position.x)
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
            _anim.SetFloat("Speed", MoveSpeed);
        }

    }

    public void OnSpawn(GameObject room)
    {

    }

    public void AITriggered(GameObject obj)
    {
        if (_currentState == UnitStates.Fighting)
            return;
        _currentState = UnitStates.MovingToTarget;
        _target = obj;
    }

    public void AITargetLost(GameObject obj)
    {
        if (_currentState == UnitStates.Fighting)
            return;

        _currentState = UnitStates.Idle;
        _target = null;
    }

    public void AITargetUpdate(GameObject obj)
    {
        if (_currentState == UnitStates.Fighting)
            return;

        _currentState = UnitStates.MovingToTarget;
        _target = obj;
    }

    public void MeeleTriggered(GameObject obj)
    {
        _currentState = UnitStates.Fighting;
    }

    public void MeeleTargetLost(GameObject obj)
    {
        _currentState = UnitStates.Idle;
    }

    public void MeeleTargetUpdate(GameObject obj)
    {
        _currentState = UnitStates.Fighting;
    }
}
