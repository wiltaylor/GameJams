using System;
using UnityEngine;
using System.Collections;

public class WorkerAI : MonoBehaviour
{

    public float MoveSpeed = 1f;
    public float StopDistance = 1f;
    public float SleepTime = 1f;
    public float FleeTime = 20f;

    public ResourceRoomController CurrentRoomController;
    public float MiningTime = 10f;
    private Animator _anim;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private SpriteRenderer _renderer;
    private bool _facingleft = true;
    private Vector3 _target = Vector3.zero;
    private ImpState _currentImpState = ImpState.SeekingResource;
    private float _currentMineTime = 10f;
    private float _currentSleepTime = 0f;
    private Vector3 _lastPosition = Vector3.zero;
    private bool _clearKinematic = false;
    private float _KinematicTimer = 1f;
    private float _fleeTimer = 0f;

    public enum ImpState
    {
        SeekingResource,
        GoingDownLadder,
        GoingUpLadder,
        SeekingDropPoint,
        Mining,
        Fleeing
    }
  
	void Start ()
	{
	    _anim = GetComponent<Animator>();
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	    _collider2D = GetComponent<Collider2D>();
	    _renderer = GetComponent<SpriteRenderer>();
    }

    public void MoveToLocation(Vector3 location)
    {
        _target = location;
    }

    void ReachedDestination()
    {
        switch (_currentImpState)
        {
            case ImpState.SeekingResource:
                _currentImpState = ImpState.GoingDownLadder;
                _rigidbody2D.isKinematic = true;
                _collider2D.enabled = false;
                _target = CurrentRoomController.BottomOfLadder.transform.position;
                break;
            case ImpState.GoingDownLadder:
                _currentMineTime = MiningTime;
                _currentImpState = ImpState.Mining;
                _renderer.enabled = false;
                _target = Vector3.zero;
                break;
            case ImpState.Mining:
                _currentImpState = ImpState.GoingUpLadder;
                _rigidbody2D.isKinematic = true;
                _collider2D.enabled = false;
                _target = CurrentRoomController.TopOfLadder.transform.position;
                _renderer.enabled = true;
                break;
            case ImpState.GoingUpLadder:
                _currentImpState = ImpState.SeekingDropPoint;
                _rigidbody2D.isKinematic = false;
                _collider2D.enabled = true;
                _target = CurrentRoomController.ResourceDrop.transform.position;
                break;
            case ImpState.SeekingDropPoint:
                _currentImpState = ImpState.SeekingResource;
                _target = CurrentRoomController.ResourceEnterance.transform.position;
                if(CurrentRoomController.ResourceType == ResourceRoomController.ResourceTypes.Gold)
                    GameManager.Instance.DropGold();
                if(CurrentRoomController.ResourceType == ResourceRoomController.ResourceTypes.Gems)
                    GameManager.Instance.DropGems();
                _currentSleepTime = SleepTime;
                break;
        }
    }
	
	void FixedUpdate ()
	{
	    if (_fleeTimer > 0f)
	    {
	        _fleeTimer -= Time.fixedDeltaTime;
	    }else if (_currentImpState == ImpState.Fleeing)
	    {
	        _currentImpState = ImpState.SeekingResource;
            _target = CurrentRoomController.ResourceEnterance.transform.position;
            _rigidbody2D.isKinematic = false;
            _collider2D.enabled = true;
	        return;
	    }

        if (_currentImpState == ImpState.Mining)
	    {
	        _currentMineTime -= Time.fixedDeltaTime;

            if (_currentMineTime <= 0f)
            {
                ReachedDestination();
                return;
            }
        }

	    if (_currentSleepTime > 0f)
	    {
	        _currentSleepTime -= Time.fixedDeltaTime;
            _anim.SetFloat("Speed", 0f);
            return;
	    }

	    if (_target == Vector3.zero)
	    {
            _anim.SetFloat("Speed", 0f);
            return;
        }

	    if (Vector3.Distance(_target, transform.position) <= StopDistance)
	    {
	        ReachedDestination();
	        return;
	    }

	    var movex = transform.position.x;
	    var movey = transform.position.y;

	    if (_currentImpState == ImpState.SeekingDropPoint || _currentImpState == ImpState.SeekingResource || _currentImpState == ImpState.Fleeing)
	    {
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

            if (_target.x > transform.position.x)
	        {
	            if (_facingleft)
	            {
	                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,
	                    transform.localScale.z);

	                _facingleft = false;
	            }

	            movex = transform.position.x + (MoveSpeed*Time.fixedDeltaTime);
	        }

	        if (_target.x < transform.position.x)
	        {
	            if (!_facingleft)
	            {
	                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,
	                    transform.localScale.z);

	                _facingleft = true;
	            }

	            movex = transform.position.x - (MoveSpeed*Time.fixedDeltaTime);
	        }

            _rigidbody2D.MovePosition(new Vector2(movex, movey));
            _anim.SetFloat("Speed", MoveSpeed);
        }

	    if (_currentImpState == ImpState.GoingUpLadder || _currentImpState == ImpState.GoingDownLadder)
	    {
	        if (_target.y > transform.position.y)
	        {
	            movey = transform.position.y + (MoveSpeed * Time.fixedDeltaTime);
	        }

	        if (_target.y < transform.position.y)
	        {
                movey = transform.position.y - (MoveSpeed * Time.fixedDeltaTime);
            }

            transform.position = new Vector2(movex, movey);
            _anim.SetFloat("Speed", 0f);
        }
    }

    public void OnSpawn(GameObject room)
    {
        CurrentRoomController = room.GetComponent<ResourceRoomController>();
        //Start Imp ai:
        _currentImpState = ImpState.SeekingResource;
        _target = CurrentRoomController.ResourceEnterance.transform.position;
        transform.parent = CurrentRoomController.Units.transform;
    }

    public void TakeHit(int qty)
    {
        _currentImpState = ImpState.Fleeing;
        _target = LitchController.Instance.gameObject.transform.position;
        _fleeTimer = FleeTime;
    }
}
