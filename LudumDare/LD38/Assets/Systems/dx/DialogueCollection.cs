using System;
using UnityEngine;

namespace Assets.Systems.dx
{
    public enum DialogueCharecter
    {
        None,
        Knight,
        Cardinal,
        Deamon
    }

    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Entry", order = 1)]
    public class DialogueCollection : ScriptableObject
    {
        public string Name;
        public DialogueEntry[] Dialogue;
    }

    [Serializable]
    public class DialogueEntry
    {
        public DialogueCharecter Charecter;
        public string Text;
    }
}
