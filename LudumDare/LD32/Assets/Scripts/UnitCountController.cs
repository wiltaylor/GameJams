using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitCountController : MonoBehaviour
{
    public enum UnitSide
    {
        Player,
        Enemy
    }


    public Text Textbox;
    public UnitSide Side;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }

    void Update()
    {
        Textbox.text = Side == UnitSide.Player ? _gameController.PlayerUnits.ToString() : _gameController.EnamyUnits.ToString();
    }
}
