using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour {

    public float MoveSpeed = 0.1f;
    public float MaxSpeed = 1f;
    public float RotateSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation);

        if (Input.GetAxis("Horizontal") > 0f)
        {
            transform.Rotate(0, 0,MaxSpeed);
            rigidbody2D.AddForce(new Vector2(MoveSpeed, 0f));
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            transform.Rotate(0, 0, -MaxSpeed);
            rigidbody2D.AddForce(new Vector2(-MoveSpeed, 0f));
        }
    }
}
