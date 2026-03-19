using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour {

    public float MessageDelay = 5f;

    private float _currentDelay = 0f;
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (_currentDelay <= 0f)
        {
            _text.text = "";

            var newMessage = GlobalController.Instance.ReadMessage();

            if (newMessage == null)
                return;

            _text.text = newMessage;
            _currentDelay = MessageDelay;
        }
        else
        {
            _currentDelay -= Time.deltaTime;
        }
	}
}
