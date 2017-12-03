using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUpdater : MonoBehaviour
{

    public Text TextBox;
    public PlayerStats Stats;
	
	void Update ()
	{
	    TextBox.text = "HP: " + Stats.CurrentHP / Stats.MaxHP * 100f + "%";
	}
}
