using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandButtonController : MonoBehaviour
{
    public GameObject BlockerSelected;
    public GameObject DigSelected;
    public GameObject LadderSelected;
    public GameObject LadderCreatePrefab;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }


    public void BlockerButton()
    {
        ClearSelection();
        GameController.Instance.CurrentClickHandler = new CreateBlockerCommand();
        BlockerSelected.SetActive(true);
    }

    public void DigClicked()
    {
        ClearSelection();
        GameController.Instance.CurrentClickHandler = new DigBlockCommand();
        DigSelected.SetActive(true);
    }

    public void LadderClicked()
    {
        ClearSelection();
        GameController.Instance.CurrentClickHandler = new CreateLadderCommand(LadderCreatePrefab);
        LadderSelected.SetActive(true);
    }

    public void ClearSelection()
    {
        GameController.Instance.CurrentClickHandler = null;
        BlockerSelected.SetActive(false);
        DigSelected.SetActive(false);
        LadderSelected.SetActive(false);
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
