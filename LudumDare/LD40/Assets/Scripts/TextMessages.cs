using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptObjects/TextMessages")]
    public class TextMessages : ScriptableObject
    {
        private readonly Queue<string> _messages = new Queue<string>();

        public void Reset()
        {
            _messages.Clear();
        }

        public bool MessagesWaiting()
        {
            return _messages.Count > 0;
        }

        public string ReadMessage()
        {
            return _messages.Dequeue();
        }

        public void AddMessage(string message)
        {
            _messages.Enqueue(message);
        }
    }
}
