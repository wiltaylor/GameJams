using System.Collections;
using System.Collections.Generic;
using Assets.Systems.CommandManager;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingView : MonoBehaviour
{
    public Text PopulationText;
    public Text OwnerText;
	
	void Update ()
	{
	    var building = CommandService.Instance.SelectedBuilding;

	    if (building == null)
	        return;

	    PopulationText.text = "Population: " + building.Population;
	    OwnerText.text = "You Controll: " + building.PlayerOwned;
	}
}
