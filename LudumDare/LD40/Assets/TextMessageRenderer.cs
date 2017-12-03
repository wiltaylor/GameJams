using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class TextMessageRenderer : MonoBehaviour
{

    public TextMessages TextMessages;
    public Text TextBox;
    public float MessageStayTime = 5f;

    private float _currentMessageTime = 0f;
	
	// Update is called once per frame
	void Update ()
	{
	    if (_currentMessageTime > 0f)
	    {
	        _currentMessageTime -= Time.deltaTime;
	        return;
	    }

	    if (TextMessages.MessagesWaiting())
	    {
	        _currentMessageTime = MessageStayTime;
	        TextBox.text = TextMessages.ReadMessage();
	    }
	    else
	    {
	        TextBox.text = "";
	    }
	}
}
