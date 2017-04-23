using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnView : MonoBehaviour
{

    public Text TurnText;
	
	
	private void Update ()
	{
	    TurnText.text = "Turn: " + TurnService.Instance.Turn + "/100";
	}
}
