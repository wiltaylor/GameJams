using System;
using UnityEngine;


[CreateAssetMenu(fileName = "ShipUpgrades", menuName = "Scripts/ShipUpgrades", order = 1)]
public class ShipUpgradeData : ScriptableObject
{
    public BeamWeaponLevel[] BeamWeapon;
    public RocketWeaponLevel[] RocketWeapon;
    public EngineLevel[] Engines;
    public ShieldLevel[] Shields;
    public CargoLevel[] CargoCapacity;
}

[Serializable]
public class CargoLevel
{
    public int Cost;
    public float MaxCapacity;
}

[Serializable]
public class BeamWeaponLevel
{
    public int Cost;
    public float Range;
    public float Damage;
    public bool BlastThrough;
    public Color Colour;
}

[Serializable]
public class RocketWeaponLevel
{
    public int Cost;
    public float Range;
    public float Damage;
    public float Radius;
    public int MaxCapacity;
}

[Serializable]
public class EngineLevel
{
    public int Cost;
    public float MaxSpeed;
    public float AccelerationAmount;
    public float MaxFuel;
    public float FuelConsuption;
    public float BreakFuel;
}

[Serializable]
public class ShieldLevel
{
    public int Cost;
    public float MaxShield;
    public float RechargeRate;
    public float MaxHP;
}
