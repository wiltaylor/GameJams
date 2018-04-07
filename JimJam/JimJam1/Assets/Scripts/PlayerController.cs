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

        if (Input.GetButtonDown("FreeLook"))
        {
            FreeLookOn = !FreeLookOn;

            if (!FreeLookOn)
                Weapon.ReleaseFire();
        }

        if (!FreeLookOn)
            return;


        if (Input.GetAxis("Vertical") > 0.1f)
        {
            _rigidbody.velocity += transform.forward * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = transform.forward * MaxSpeed;
        }

        if (Input.GetAxis("Vertical") < -0.1f)
        {
            _rigidbody.velocity += -transform.forward * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = -transform.forward * MaxSpeed;
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            _rigidbody.velocity += transform.right * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = transform.right * MaxSpeed;
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            _rigidbody.velocity += -transform.right * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = -transform.right * MaxSpeed;
        }

        if (Input.GetAxis("ThrustUpDown") > 0.1f)
        {
            _rigidbody.velocity += transform.up * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = transform.up * MaxSpeed;
        }

        if (Input.GetAxis("ThrustUpDown") < -0.1f)
        {
            _rigidbody.velocity += -transform.up * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = -transform.up * MaxSpeed;
        }



        if (Input.GetButtonDown("Break"))
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }


        if (Input.GetAxis("Mouse X") > 0.1f)
        {
             _rigidbody.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse X") < -0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse Y") > 0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.left * RotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse Y") < -0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.left * RotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire"))
        {
            Weapon.Fire();
        }

        if (Input.GetButtonUp("Fire"))
        {
            Weapon.ReleaseFire();
        }
    }
}
