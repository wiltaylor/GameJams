using UnityEngine;
using System.Collections;

public class ProjectileHandler : MonoBehaviour
{

    public float Duration = 10f;
    public int Damage = 10;
    public float Speed = 100f;
    public GameObject BlastSpotPrefab;
    public string ProjectileName;
    public AudioClip HitSound;

    private float _countdown = 0f;
    private new Rigidbody rigidbody;
    private FactoryController _factory;

    public void Start()
    {
        _factory = GlobalController.Instance.FactoryController;

        if(BlastSpotPrefab != null)
        {
            _factory.RegisterType(ProjectileName + "_blastspot", BlastSpotPrefab);
        }

        _countdown = Duration;
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if(rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();

        _countdown = Duration;
        rigidbody.velocity = Vector3.zero;
    }

    void Update()
    {
        if(GlobalController.Instance.GamePaused)
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }

        _countdown -= Time.deltaTime;

        rigidbody.AddForce(transform.forward * Speed * Time.deltaTime, ForceMode.Force);


        if (_countdown <= 0f)
        {
            Destruct();
        }
    }

    void Destruct()
    {
        _countdown = Duration;
        gameObject.SetActive(false);

        if(HitSound != null)
        {
            GlobalController.Instance.AudioController.PlaySFXAt(HitSound, transform.position, 0.8f, 1.2f);
        }

        if (BlastSpotPrefab != null)
        {
            var blastspot = _factory.GetObject(ProjectileName + "_blastspot");
            blastspot.transform.rotation = transform.rotation;
            blastspot.transform.position = transform.position;
            blastspot.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "AI")
            collision.gameObject.SendMessage("Hit", Damage);
        Destruct();
    }
}