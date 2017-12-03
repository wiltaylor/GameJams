using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitScreen : MonoBehaviour
{

    public PlayerStats Stats;
    public float FlashLength = 1f;
    public Image FlashImage;

    private float _flashLeft;

    void Update ()
    {
        if (_flashLeft > 0f)
        {
            _flashLeft -= Time.deltaTime;
            return;
        }

        if (Stats.BeenHit)
        {
            _flashLeft = FlashLength;
            Stats.BeenHit = false;
            FlashImage.enabled = true;
        }
        else
        {
            FlashImage.enabled = false;
        }
    }
}
