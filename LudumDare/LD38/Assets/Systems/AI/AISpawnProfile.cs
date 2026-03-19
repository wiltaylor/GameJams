using UnityEngine;

namespace Assets.Systems.AI
{
    [CreateAssetMenu(fileName = "AIProfile", menuName = "AI/Spawn Profile", order = 1)]
    public class AISpawnProfile : ScriptableObject
    {
        public string Name;
        public AISpawnSettings[] Setting;
    }
}
