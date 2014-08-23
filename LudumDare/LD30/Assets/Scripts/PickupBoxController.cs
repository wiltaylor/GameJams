using UnityEngine;
using System.Collections;

public class PickupBoxController : MonoBehaviour
{

    public GameObject[] AvailablePowerUps;
    public float TimeOut = 20f;
    
    private SpriteRenderer _sprite;
    private float _timeLeft;
    
	void Start ()
	{
	    _sprite = GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
        if (_sprite.enabled)
            return;

	    _timeLeft -= Time.deltaTime;

        if(_timeLeft < 0f)
            _sprite.enabled = true;
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!_sprite.enabled)
            return;

        var collector = other.GetComponent<PowerUpCollector>();

        if(collector == null)
            return;

        var powerUpIndex = Random.Range(0, AvailablePowerUps.GetLowerBound(0));

        collector.GetItem((GameObject)Instantiate(AvailablePowerUps[powerUpIndex]));

        _sprite.enabled = false;
        _timeLeft = TimeOut;
    }
}
