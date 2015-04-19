using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandButtonController : MonoBehaviour
{
    public GameObject BlockerSelected;
    public GameObject DigSelected;
    public GameObject LadderSelected;
    public GameObject BridgeSelected;

    public GameObject LadderCreatePrefab;
    public GameObject BridgeCreatePrefab;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }


    public void BlockerButton()
    {
        if (GameController.Instance.CurrentClickHandler != null && GameController.Instance.CurrentClickHandler.GetType() == typeof(CreateBlockerCommand))
        {
            ClearSelection();
            return;
        }

        ClearSelection();
        GameController.Instance.CurrentClickHandler = new CreateBlockerCommand();
        BlockerSelected.SetActive(true);
    }

    public void DigClicked()
    {
        if (GameController.Instance.CurrentClickHandler != null && GameController.Instance.CurrentClickHandler.GetType() == typeof(DigBlockCommand))
        {
            ClearSelection();
            return;
        }

        ClearSelection();
        GameController.Instance.CurrentClickHandler = new DigBlockCommand();
        DigSelected.SetActive(true);
    }

    public void LadderClicked()
    {
        if (GameController.Instance.CurrentClickHandler != null && GameController.Instance.CurrentClickHandler.GetType() == typeof(CreateLadderCommand))
        {
            ClearSelection();
            return;
        }

        ClearSelection();
        GameController.Instance.CurrentClickHandler = new CreateLadderCommand(LadderCreatePrefab);
        LadderSelected.SetActive(true);
    }

    public void BridgeClicked()
    {
        if (GameController.Instance.CurrentClickHandler != null && GameController.Instance.CurrentClickHandler.GetType() == typeof(CreateBridgeCommand))
        {
            ClearSelection();
            return;
        }

        ClearSelection();
        GameController.Instance.CurrentClickHandler = new CreateBridgeCommand(BridgeCreatePrefab);
        BridgeSelected.SetActive(true);
    }

    public void ClearSelection()
    {
        GameController.Instance.CurrentClickHandler = null;
        BlockerSelected.SetActive(false);
        DigSelected.SetActive(false);
        LadderSelected.SetActive(false);
        BridgeSelected.SetActive(false);
    }

    public void MouseEnterUI()
    {
        _gameController.MouseOverUI = true;
    }

    public void MouseExitUI()
    {
        _gameController.MouseOverUI = false;
    }

}
