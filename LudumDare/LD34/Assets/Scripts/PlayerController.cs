using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject ProjectilePrefab;
    public Rigidbody ridgidbody;
    public float Speed = 10f;
    public float MaxSpeed = 20f;
    public Transform ProjectilePoint;
    public float HP = 100f;
    public float MaxHP = 100f;
    public AudioClip ShootSound;
    public AudioClip HitSound;
    
    [HideInInspector]
    public bool PlayerIsDead = false;

    private FactoryController _factory;
    private MouseLook _mouseLook;
    private Vector3 _storedVelocity = Vector3.zero;
    private AudioSource _audio;

    void Awake()
    {
        GlobalController.Instance.CurrentPlayer = this;
    }

    void Start()
    {
        _factory = GlobalController.Instance.FactoryController;
        _factory.RegisterType("PlayerProjectile", ProjectilePrefab);
        _mouseLook = GetComponent<MouseLook>();
        _audio = GetComponent<AudioSource>();
    }

    void Update ()
    {
        if (GlobalController.Instance.GamePaused)
        {

            _audio.mute = true;

            if (ridgidbody.velocity != Vector3.zero)
            {
                _storedVelocity = ridgidbody.velocity;
                ridgidbody.velocity = Vector3.zero;
            }

            return;
        }

        _audio.mute = false;

        _audio.pitch = ridgidbody.velocity.sqrMagnitude / 100f;

        if(_storedVelocity != Vector3.zero)
        {
            ridgidbody.velocity = _storedVelocity;
            _storedVelocity = Vector3.zero;
        }

        if (HP > MaxHP)
            HP = MaxHP;

        if (HP <= 0f)
        {
            PlayerIsDead = true;
            _mouseLook.MouseLocked = false;
            GlobalController.Instance.GamePaused = true;
            return;
        }
        
        if (Input.GetAxis("Vertical") > 0f & ridgidbody.velocity.magnitude < MaxSpeed)
        {
            ridgidbody.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * Speed * Time.deltaTime, ForceMode.Force);
        }
        else if(Input.GetAxis("Vertical") < 0f)
        {
            if(GlobalController.Instance.HasBreak)
                ridgidbody.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * Speed * Time.deltaTime * -1, ForceMode.Force);
        }

        if(Input.GetAxis("Horizontal") > 0f)
        {
            if (GlobalController.Instance.HasSteering)
                ridgidbody.AddForce(new Vector3(transform.right.x, 0, transform.right.z) * Speed * Time.deltaTime, ForceMode.Force);
        }
        else if(Input.GetAxis("Horizontal") < 0f)
        {
            if (GlobalController.Instance.HasSteering)
                ridgidbody.AddForce(new Vector3(transform.right.x, 0, transform.right.z) * Speed * Time.deltaTime * -1, ForceMode.Force);
        }

        if (Input.GetButtonDown("Fire"))
        {
            if (GlobalController.Instance.HasGuns)
            {
                var projectile = _factory.GetObject("PlayerProjectile");

                projectile.transform.rotation = transform.rotation;
                projectile.transform.position = ProjectilePoint.position;
                projectile.SetActive(true);
                GlobalController.Instance.AudioController.PlaySFXAt(ShootSound, transform.position, 0.8f, 1.2f);
            }
        }
	}

    void Hit(int damage)
    {
        HP -= damage;

        GlobalController.Instance.AudioController.PlaySFXAt(HitSound, transform.position);
    }
}
