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

    private Rigidbody2D _rigidbody2D;
    private Vector3 _lastPosition;
    private Animator _animator;
    private bool _Cutting = false;
    private BlockController _CuttingTarget = null;

    public UnitController(bool cutting)
    {
        _Cutting = cutting;
    }

    // Use this for initialization
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
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
        if (coll.gameObject.tag == "Ground")
        {
            if (coll.transform.position.y < transform.position.y - Height)
            {
                Debug.Log(string.Format("{0} - {1}/{2}", coll.transform.name, coll.transform.position.y, transform.position.y - Height));
                return;
            }

            var blockcontroller = coll.gameObject.GetComponent<BlockController>();

            if (blockcontroller.Targeted && HasCuttingTools)
            {
                _Cutting = true;
                _animator.SetTrigger("Cutting");
                _CuttingTarget = blockcontroller;
                return;
            }
        }

        if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Unit_Turn")
        {
            Direction = Direction == UnitFacingDirection.Right ? UnitFacingDirection.Left : UnitFacingDirection.Right;
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
        }
    }
}