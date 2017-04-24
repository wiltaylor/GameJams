using UnityEngine;

namespace Assets.Systems.Music
{
    [CreateAssetMenu(fileName = "SFXList", menuName = "Audio/SFXList", order = 1)]
    public class SfxList : ScriptableObject
    {
        public AudioClip Attack;
        public AudioClip BuildUnit;
        public AudioClip DemonAttack;
        public AudioClip Mine;
    }
}
