using UnityEngine;
using System.Collections;

public class BlastSpotController : MonoBehaviour {


    public float Duration = 1f;
    private float _countdown = 0f;

    void Start ()
    {
        OnEnable();
    }

    void OnEnable()
    {
        _countdown = Duration;
    }
	
	void Update ()
    {
        _countdown -= Time.deltaTime;


        if(_countdown < 0f)
        {
            _countdown = Duration;
            gameObject.SetActive(false);
        }
    }
}
