using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptObjects/QuestItem")]
    public class QuestItem : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
    }
}
