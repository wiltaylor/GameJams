using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Systems.CommandManager;
using UnityEngine;

public class UISelectorView : MonoBehaviour
{
    public GameObject BuildingPanel;
    public GameObject UnitPanel;

	private void Update ()
    {
        switch (CommandService.Instance.SelectionState)
        {
            case CommandSelectionState.Building:
                UnitPanel.SetActive(false);
                BuildingPanel.SetActive(true);
                break;
            case CommandSelectionState.Nothing:
                UnitPanel.SetActive(true);
                BuildingPanel.SetActive(false);
                break;
            case CommandSelectionState.Unit:
                UnitPanel.SetActive(true);
                BuildingPanel.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
	}
}
