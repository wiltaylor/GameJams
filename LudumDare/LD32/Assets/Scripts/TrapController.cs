using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour
{

    public bool PlaySound = false;

    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Contains("Unit"))
        {
            var unit = coll.gameObject.GetComponent<UnitController>();
            unit.Kill();

            if(PlaySound)
                _audio.Play();
        }
    } 
    
}
