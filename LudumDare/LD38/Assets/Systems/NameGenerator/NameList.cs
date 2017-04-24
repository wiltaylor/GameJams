using UnityEngine;

namespace Assets.Systems.NameGenerator
{
    [CreateAssetMenu(fileName = "Namelist", menuName = "Names/Name List", order = 1)]
    public class NameList : ScriptableObject
    {
        public string Name;
        public string[] List;
    }
}
