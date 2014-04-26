using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour {

    public float MoveForce = 0.1f;
    public float RotateSpeed = 1f;
    public float MaxLeftAngle = 275f;
    public float MaxRightAngle = 75f;
    public float BombRadius = 0.00000001f;

    private GameObject _bomb;

	void Start ()
	{
	    _bomb = transform.FindChild("Bomb").gameObject;
	}

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0f)
        {
            transform.Rotate(0, 0,RotateSpeed);

            rigidbody2D.AddForce(new Vector2(MoveForce, 0f));

            Debug.Log(transform.eulerAngles); //90
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            transform.Rotate(0, 0, -RotateSpeed);
            rigidbody2D.AddForce(new Vector2(-MoveForce, 0f));

            Debug.Log(transform.eulerAngles); //260

        }

        if (Input.GetButton("Bomb"))
        {
            _bomb.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (transform.eulerAngles.z > MaxRightAngle && transform.eulerAngles.z < MaxLeftAngle)
        {

            if (transform.eulerAngles.z - MaxLeftAngle > MaxRightAngle - transform.eulerAngles.z)
            {
                transform.eulerAngles = new Vector3(0, 0, MaxLeftAngle);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, MaxRightAngle);
            }

            
        }
    }
}
