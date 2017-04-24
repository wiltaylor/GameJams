using System;
using Assets.Systems.CommandManager;
using Assets.Systems.dx;
using UnityEngine;

public class UISelectorView : MonoBehaviour
{
    public GameObject BuildingPanel;
    public GameObject UnitPanel;
    public GameObject MenuPanel;
    public GameObject DialoguePanel;

	private void Update ()
    {
        if (DialogueService.Instance.Active)
        {
            DialoguePanel.SetActive(true);
            UnitPanel.SetActive(false);
            BuildingPanel.SetActive(false);
            return;
        }

        DialoguePanel.SetActive(false);

        switch (CommandService.Instance.SelectionState)
        {
            case CommandSelectionState.Building:
                UnitPanel.SetActive(false);
                BuildingPanel.SetActive(true);
                break;
            case CommandSelectionState.Nothing:
                UnitPanel.SetActive(false);
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

    public void OpenMenu()
    {
        MenuPanel.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        MenuPanel.gameObject.SetActive(false);
    }
}
