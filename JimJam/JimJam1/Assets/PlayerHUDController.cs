using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{

    public Text Speed;
    public Image TargetReticle;
    public PlayerController Player;
	
	void Update ()
	{
	    Speed.text = "Speed: " + Player.CurrentSpeed;
        TargetReticle.enabled = Player.FreeLookOn;
	}
}
