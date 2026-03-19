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
    public AudioSource CutSound;
    public float CutPitchMax = 1.5f;
    public float CutPitchMin = 0.5f;

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
        if (_gameController.UILockedOut)
        {
            _rigidbody2D.isKinematic = true;
            return;
        }
            

        if (_Cutting)
        {
            if (_CuttingTarget == null)
            {
                _Cutting = false;
                _animator.SetTrigger("StopCutting");
            }
            else
            {
                if (!_CuttingTarget.Targeted)
                {
                    _Cutting = false;
                    _animator.SetTrigger("StopCutting");
                    return;
                }
                else
                {
                    _CuttingTarget.Health -= CuttingDamage * Time.fixedDeltaTime;
                    return;                    
                }
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
            if (coll.transform.tag.Contains("Unit"))
            {
                var unit = coll.gameObject.GetComponent<UnitController>();

                if (!unit.PlayerOwned) return;

                _animator.SetTrigger("Attack");
                unit.Kill();

                Direction = coll.transform.position.x > transform.position.x ? UnitFacingDirection.Right : UnitFacingDirection.Left;

                return;
            }
        }

        if (coll.gameObject.tag.Contains("Ground") || coll.gameObject.tag == "Usable")
        {
            var blockcontroller = coll.gameObject.GetComponent<BlockController>();

            if (blockcontroller.Targeted && HasCuttingTools || blockcontroller.Targeted && coll.gameObject.tag == "Usable")
            {
                _Cutting = true;
                _animator.SetTrigger("Cutting");
                _CuttingTarget = blockcontroller;
                return;
            }

            if (coll.transform.position.y < transform.position.y - Height)
            {
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

    public void PlayCutSound()
    {
        if (CutSound != null)
        {
            CutSound.pitch = UnityEngine.Random.Range(CutPitchMin, CutPitchMax);
            CutSound.Play();
        }
    }
}