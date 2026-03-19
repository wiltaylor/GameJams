using System.Linq;

namespace Assets.Systems.dx
{
    public class DialogueService
    {
        private static DialogueService _instance;
        private DialogueCollection[] _dialogueCollections;
        private DialogueCollection _currentDialogue;
        private int _currentDialogueIndex;

        public static DialogueService Instance
        {
            get { return _instance ?? (_instance = new DialogueService()); }
        }

        public void AssignDialogue(DialogueCollection[] dialogue)
        {
            _dialogueCollections = dialogue;
        }

        public void StartDialoue(string name)
        {
            _currentDialogue = _dialogueCollections.FirstOrDefault(d => d.Name == name);
            _currentDialogueIndex = 0;
        }

        public bool Active { get { return _currentDialogue != null; } }

        public DialogueCharecter CurrentCharecter
        {
            get { return _currentDialogue.Dialogue[_currentDialogueIndex].Charecter; }
        }

        public string CurrentText
        {
            get { return _currentDialogue.Dialogue[_currentDialogueIndex].Text; }
        }

        public void Next()
        {
            _currentDialogueIndex++;

            if(_currentDialogue.Dialogue.Length <= _currentDialogueIndex)
            _currentDialogue = null;
        }
    }
}
