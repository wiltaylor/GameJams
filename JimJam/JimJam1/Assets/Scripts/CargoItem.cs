
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CargoItem", menuName = "Scripts/Cargo Item", order = 1)]
public class CargoItem : ScriptableObject
{
    public string Name;
    public float WeightPerUnit;
    public Sprite Icon;
}

[Serializable]
public class ShopItem
{
    public CargoItem Item;
    public int Cost;
}

[Serializable]
public class ShipCargoItem
{
    public CargoItem Item;
    public int Qty;
}

