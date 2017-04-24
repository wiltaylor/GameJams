using System.Linq;

namespace Assets.Systems.dx
{
    public class DialogueService
    {
        private static DialogueService _instance;
        private DialogueCollection[] _dialogueCollections;
        private DialogueCollection _currentDialogue;
        private int _currentDialogueIndex;

        public DialogueService Instance
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

        public bool MoreDialogue()
        {
            if (_currentDialogue != null && _currentDialogueIndex >= _currentDialogue.Dialogue.Length)
                _currentDialogue = null;

            return _currentDialogue != null;
        }

        public DialogueCharecter CurrentCharecter
        {
            get { return _currentDialogue.Dialogue[_currentDialogueIndex].Charecter; }
        }

        public string CurrentText
        {
            get { return _currentDialogue.Dialogue[_currentDialogueIndex].Text; }
        }
    }
}
