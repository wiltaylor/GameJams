using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptObjects/AmmoType")]
    public class AmmoType : ScriptableObject
    {
        public string Name;
        public int MaxCarry;
        public Sprite Icon;
    }
}
