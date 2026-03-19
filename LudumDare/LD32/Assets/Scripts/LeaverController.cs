using UnityEngine;
using System.Collections;

public class LeaverController : MonoBehaviour
{

    public GameObject[] TargetObjects;
    public bool Targeted;
    public Sprite SelectedImage;
    public Sprite NormalImage;
    public bool Timed = false;
    public float TimeToTrigger = 10f;
    private SpriteRenderer _spriteRenderer;
    private GameController _gameController;

    private bool _switched = false;
    private AudioSource _audio;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameController = GameController.Instance;
        _audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!Timed || _switched)
            return;

        if (TimeToTrigger <= 0f)
            Trigger();
        else
            TimeToTrigger -= Time.fixedDeltaTime;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!Targeted || _switched)
            return;

        if (!coll.transform.tag.Contains("Unit")) return;

        var unit = coll.transform.gameObject.GetComponent<UnitController>();
        if (unit.PlayerOwned)
            Trigger();
    }

    void Trigger()
    {
        foreach (var o in TargetObjects)
        {
            o.SendMessage("SwitchTriggered");
        }

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        _switched = true;
        _spriteRenderer.sprite = NormalImage;
        _audio.Play();
    }

    void OnMouseDown()
    {
        if (_switched)
            return;

        if (_gameController.UILockedOut)
            return;

        if (!Targeted)
        {
            Targeted = true;
            _spriteRenderer.sprite = SelectedImage;
        }
        else
        {
            Targeted = false;
            _spriteRenderer.sprite = NormalImage;
        }
    }


}
