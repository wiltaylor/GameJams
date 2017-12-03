using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float CurrentHP;
    public float MaxHP;
    public Sprite CurrentGunIcon;
    public string CurrentGunName;
    public Sprite CurrentAmmoIcon;
    public int CurrentAmmoAmmount;
    public List<QuestItem> QuestItems;
    public List<WeaponItem> Weapons;
    public WeaponItem CurrentWeapon;
    public int Score;
}

