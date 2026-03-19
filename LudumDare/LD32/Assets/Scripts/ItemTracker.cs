using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemTracker : MonoBehaviour {

    public enum ItemType
    {
        Blocker,
        Dig,
        Bridge,
        Ladder
    }

    public ItemType Type;
    public Text Text;
    public CommandButtonController ButtonController;

    private GameController _gameController;
    private Button _button;
    private bool _cleared = false;

    void Start()
    {
        _gameController = GameController.Instance;
        _button = GetComponent<Button>();
    }

    void Update()
    {
        switch (Type)
        {
            case ItemType.Blocker:
                Text.text = _gameController.BlockersLeft.ToString();

                if (_gameController.BlockersLeft <= 0 && !_cleared)
                {
                    _button.interactable = false;
                    ButtonController.ClearSelection();
                    _cleared = true;
                }

                if (_cleared && _gameController.BlockersLeft > 0)
                    _cleared = false;
                break;
            case ItemType.Bridge:
                Text.text = _gameController.BridgesLeft.ToString();

                if (_gameController.BridgesLeft <= 0 && !_cleared)
                {
                    _button.interactable = false;
                    ButtonController.ClearSelection();
                    _cleared = true;
                }

                if (_cleared && _gameController.BridgesLeft > 0)
                    _cleared = false;
                break;
            case ItemType.Dig:
                Text.text = _gameController.DigLeft.ToString();
                break;
            case ItemType.Ladder:
                Text.text = _gameController.LaddersLeft.ToString();

                if (_gameController.LaddersLeft <= 0 && !_cleared)
                {
                    _button.interactable = false;
                    ButtonController.ClearSelection();
                    _cleared = true;
                }

                if (_cleared && _gameController.LaddersLeft > 0)
                    _cleared = false;
                break;
        }
    }
}
