using UnityEngine;
using System.Collections;

public class PowerUpInfo : MonoBehaviour
{
    public enum ItemType
    {
        Buff,
        ProjectileFoward,
        ProjectileBack
    }

    
    public Sprite Icon;
    public string Name;
    public ItemType Type;


}
