using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{

    public PlayerStats Stats;
    public Image GunIcon;
    public Image AmmoIcon;
    public Text GunName;
    public Text AmmoCount;

	
	void Update ()
    {
        if (Stats.CurrentAmmoIcon == null)
            AmmoIcon.gameObject.SetActive(false);
        else
        {
            AmmoIcon.gameObject.SetActive(true);
            AmmoIcon.sprite = Stats.CurrentAmmoIcon;
        }

        if(Stats.CurrentGunIcon == null)
            GunIcon.gameObject.SetActive(false);
        else
        {
            GunIcon.gameObject.SetActive(true);
            GunIcon.sprite = Stats.CurrentGunIcon;
        }

        GunName.text = string.IsNullOrEmpty(Stats.CurrentGunName) ? "" : Stats.CurrentGunName;

        AmmoCount.text = Stats.CurrentAmmoAmmount == 0 ? "" : Stats.CurrentAmmoAmmount.ToString();
    }
}
