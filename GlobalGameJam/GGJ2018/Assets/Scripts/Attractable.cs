using System;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    public enum AttractDirection
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }

    public float Speed;
    public AttractDirection Direction;

    private float _remainingMove;
    private Vector3 _attraitionDirection;
    private Vector3 _targetLocation;

    void Start()
    {
        switch (Direction)
        {
            case AttractDirection.Up:
                _attraitionDirection = Vector3.up;
                
                break;
            case AttractDirection.Down:
                _attraitionDirection = Vector3.down;
                break;
            case AttractDirection.Left:
                _attraitionDirection = Vector3.left;
                break;
            case AttractDirection.Right:
                _attraitionDirection = Vector3.right;
                break;
            case AttractDirection.Forward:
                _attraitionDirection = Vector3.forward;
                break;
            case AttractDirection.Back:
                _attraitionDirection = Vector3.back;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void Update()
    {
        if (_remainingMove > 0f)
        {
            var currentMove = _attraitionDirection * Speed * Time.deltaTime;

            _remainingMove -= Speed * Time.deltaTime;

            transform.position = transform.position + currentMove;


            if (_remainingMove < 0f)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
            }
        }
    }




    public void AddAttraction(float ammount)
    {
        if (_remainingMove < 0f)
            _remainingMove = 0f;

        _remainingMove += ammount;
    }
}
