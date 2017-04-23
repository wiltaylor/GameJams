using System.Collections;
using System.Collections.Generic;
using Assets.Systems.CommandManager;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitView : MonoBehaviour
{
    public Text PlayerMovesLeft;

	private void Update ()
	{
	    var unit = CommandService.Instance.SelectedUnit;

	    if (unit == null)
	        return;

	    PlayerMovesLeft.text = "Move Points: " + unit.MovePointsLeft + "/" + unit.MovePoints;

	}
}
