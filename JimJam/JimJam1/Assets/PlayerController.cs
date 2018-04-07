using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float MaxSpeed = 100f;
    public float AccelerationAmmount = 5f;
    public float RotationSpeed = 10f;
    public bool FreeLookOn = false;
    public WeaponController Weapon;

    public float CurrentSpeed
    {
        get { return _rigidbody.velocity.z; }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

	void Update ()
    {
        Cursor.visible = !FreeLookOn;
        Cursor.lockState = FreeLookOn ? CursorLockMode.Locked : CursorLockMode.None;
        

        if (Input.GetAxis("Vertical") > 0.1f)
        {
            _rigidbody.velocity += Vector3.forward * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = Vector3.forward * MaxSpeed;
        }

        if (Input.GetAxis("Vertical") < -0.1f)
        {
            _rigidbody.velocity += Vector3.back * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = Vector3.back * MaxSpeed;
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.up * RotationSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.up * -RotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("FreeLook"))
        {
            FreeLookOn = !FreeLookOn;

            if (!FreeLookOn)
                Weapon.ReleaseFire();
        }

        if(Input.GetButtonDown("Break"))
            _rigidbody.velocity = Vector3.zero;
        

        if (FreeLookOn && Input.GetAxis("Mouse X") > 0.1f)
        {
             _rigidbody.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }

        if (FreeLookOn && Input.GetAxis("Mouse X") < -0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }

        if (FreeLookOn && Input.GetAxis("Mouse Y") > 0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.left * RotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);
        }

        if (FreeLookOn && Input.GetAxis("Mouse Y") < -0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.left * RotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);
        }

        if (FreeLookOn && Input.GetButtonDown("Fire"))
        {
            Weapon.Fire();
        }

        if (FreeLookOn && Input.GetButtonUp("Fire"))
        {
            Weapon.ReleaseFire();
        }
    }
}
