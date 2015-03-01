using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GroundBattleController : MonoBehaviour
{

    public Text PlanetName;
    public Text EmpireTroopNo;
    public Text PlanetTroopNo;
    public Text Banner;
    public Button AttackButton;
    public Button RetreatButton;
    public Button VictoryButton;

    public float BattleDelay = 10f;
    public int MinUnits = 1;
    public int MaxUnits = 100;

    private PlanetController _controller;
    private EmpireController _empire;
    private bool _battleInProgress;
    private float _timeLeft;

    public void StartGroundBattle(GameObject target)
    {
        _controller = target.GetComponent<PlanetController>();
        _empire = GameManager.Instance.Empire;

        PlanetName.text = _controller.PlanetName;
        UpdateStats();
    }

    public void UpdateStats()
    {
        if (_controller.Troops == 0)
        {
            AttackButton.gameObject.SetActive(false);
            RetreatButton.gameObject.SetActive(false);
            VictoryButton.gameObject.SetActive(true);
        }
        else
        {
            VictoryButton.gameObject.SetActive(false);

            if (_battleInProgress)
            {
                AttackButton.gameObject.SetActive(false);
                RetreatButton.gameObject.SetActive(true);
            }
            else
            {
                AttackButton.gameObject.SetActive(true);
                RetreatButton.gameObject.SetActive(true);
            }
        }

        if (_empire.Troops == 0)
        {
            AttackButton.gameObject.SetActive(false);
            RetreatButton.gameObject.SetActive(true);
            VictoryButton.gameObject.SetActive(false);
        }

        EmpireTroopNo.text = _empire.Troops.ToString();
        PlanetTroopNo.text = _controller.Troops.ToString();
    }

    public void StartBattle()
    {
        _battleInProgress = true;
        _timeLeft = BattleDelay;
    }

    public void Retreate()
    {
        if (_controller.PlayerOwned)
        {
            _controller.UpdateOwner();
        }

        GameManager.Instance.Selector.RefreshDisplay();
        gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (_controller.Troops <= 0)
        {
            _controller.Troops = 0;
            _battleInProgress = false;
            Banner.text = "Victory!";
            _controller.PlayerOwned = true;
        }

        if (_empire.Troops <= 0)
        {
            _empire.Troops = 0;
            _battleInProgress = false;
            Banner.text = "Defeat!";
        }

        if (_battleInProgress)
        {
            _timeLeft -= Time.fixedDeltaTime;


            if (_timeLeft <= 0)
            {
                _timeLeft = BattleDelay;

                _controller.Troops -= Random.Range(MinUnits, MaxUnits);
                _empire.Troops -= Random.Range(MinUnits, MaxUnits);
            }
        }

        UpdateStats();
    }
}
