using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSoundFX : MonoBehaviour
{

    public AudioSource Right;
    public AudioSource Left;

    private Animator _animator;
    private bool _alreadyDoneRight;
    private bool _alreadyDoneLeft;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
	
	void Update ()
    {
        if (_animator.GetBool("Left"))
        {
            if (!_alreadyDoneLeft)
            {
                _alreadyDoneLeft = true;
                
                if(Left != null)
                    Left.Play();
            }
        }
        else
        {
            _alreadyDoneLeft = false;
        }

        if (_animator.GetBool("Right"))
        {
            if (!_alreadyDoneRight)
            {
                _alreadyDoneRight = true;

                if (Right != null)
                    Right.Play();
            }
        }
        else
        {
            _alreadyDoneRight = false;
        }
	}
}
