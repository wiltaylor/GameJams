using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class UnitController : MonoBehaviour
{


    public enum UnitFacingDirection
    {
        Left,
        Right
    }

    public UnitFacingDirection Direction;
    public float MoveSpeed = 1f;
    public float FreeForce = 5f;
    public bool Walking = true;
    public bool HasCuttingTools = true;
    public float CuttingDamage = 1f;
    public float Height = 0.10f;
    public bool PlayerOwned = true;
    public GameObject BloodPrefab;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _lastPosition;
    private Animator _animator;
    private bool _Cutting = false;
    private BlockController _CuttingTarget = null;
    private GameController _gameController;
    private int _onLadder = 0;

    // Use this for initialization
    private void Start()
    {
        _gameController = GameController.Instance;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (PlayerOwned)
            _gameController.PlayerUnits++;
        else
            _gameController.EnamyUnits++;

    }

    private void FixedUpdate()
    {
        if (transform.position.y < _gameController.YCutOff)
            Destroy(gameObject);


        if (_Cutting)
        {
            if (_CuttingTarget == null)
            {
                _Cutting = false;
                _animator.SetTrigger("StopCutting");
            }
            else
            {
                _CuttingTarget.Health -= CuttingDamage*Time.fixedDeltaTime;
                return;
            }
        }

        if (_onLadder > 0)
            return;

        if (!Walking)
        {
            _animator.SetBool("Blocking", true);
            return;
        }

        if (_lastPosition != null && transform.position == _lastPosition)
        {
            _rigidbody2D.AddForce(transform.up*FreeForce, ForceMode2D.Impulse);
        }

        _lastPosition = transform.position;

        if (Direction == UnitFacingDirection.Right)
            _rigidbody2D.AddForce(transform.right * MoveSpeed * Time.fixedDeltaTime  , ForceMode2D.Force);
        else
            _rigidbody2D.AddForce(transform.right * MoveSpeed * Time.fixedDeltaTime * -1, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (Direction == UnitFacingDirection.Left)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,
                    transform.localScale.z);
            }
        }
        else
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                    transform.localScale.z);
            }
        }

        if (_onLadder > 0)
            return;

        if (!PlayerOwned)
        {
            if (coll.transform.tag == "Unit")
            {
                var unit = coll.gameObject.GetComponent<UnitController>();

                if (!unit.PlayerOwned) return;

                _animator.SetTrigger("Attack");
                unit.Kill();

                Direction = coll.transform.position.x > transform.position.x ? UnitFacingDirection.Right : UnitFacingDirection.Left;

                return;
            }
        }

        if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Usable")
        {
            if (coll.transform.position.y < transform.position.y - Height)
            {
                return;
            }

            var blockcontroller = coll.gameObject.GetComponent<BlockController>();

            if (blockcontroller.Targeted && HasCuttingTools || blockcontroller.Targeted && coll.gameObject.tag == "Usable")
            {
                _Cutting = true;
                _animator.SetTrigger("Cutting");
                _CuttingTarget = blockcontroller;
                return;
            }
        }

        if (coll.gameObject.tag != "Ground" && coll.gameObject.tag != "Unit_Turn" && coll.gameObject.tag != "Usable")
            return;

        Direction = Direction == UnitFacingDirection.Right ? UnitFacingDirection.Left : UnitFacingDirection.Right;
        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }

    void OnMouseDown()
    {
        if (_gameController.CurrentClickHandler == null)
            return;

        if (PlayerOwned && _gameController.CurrentClickHandler.SupportFriendlyUnit)
            _gameController.CurrentClickHandler.ClickUnit(gameObject);

        if (!PlayerOwned && _gameController.CurrentClickHandler.SupportEnamyUnit)
            _gameController.CurrentClickHandler.ClickUnit(gameObject);

    }

    public void Kill()
    {
        Instantiate(BloodPrefab, transform.position, transform.rotation);
        

        if (PlayerOwned)
            _gameController.PlayerUnits--;
        else
            _gameController.EnamyUnits--;

        Destroy(gameObject);
    }

    void EnterLadder()
    {
        _onLadder++;
    }

    void ExitLadder()
    {
        _onLadder--;
    }
}