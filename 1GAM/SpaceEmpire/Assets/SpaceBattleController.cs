using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpaceBattleController : MonoBehaviour
{

    public Text PlanetName;
    public Text EmpireShipNo;
    public Text PlanetShipNo;
    public Text Banner;
    public Button AttackButton;
    public Button RetreatButton;
    public Button DeployButton;

    public float BattleDelay = 10f;
    public int MinUnits = 1;
    public int MaxUnits = 100;

    private PlanetController _controller;
    private EmpireController _empire;
    private bool _battleInProgress;
    private float _timeLeft;

    public void StartSpaceBattle(GameObject target)
    {
        _controller = target.GetComponent<PlanetController>();
        _empire = GameManager.Instance.Empire;

        PlanetName.text = _controller.PlanetName;
        UpdateStats();
    }

    public void UpdateStats()
    {
        if (_controller.Ships == 0)
        {
            AttackButton.gameObject.SetActive(false);
            RetreatButton.gameObject.SetActive(false);
            DeployButton.gameObject.SetActive(true);
        }
        else
        {
            DeployButton.gameObject.SetActive(false);

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

        if (_empire.Ships == 0)
        {
            AttackButton.gameObject.SetActive(false);
            RetreatButton.gameObject.SetActive(true);
            DeployButton.gameObject.SetActive(false);
        }

        EmpireShipNo.text = _empire.Ships.ToString();
        PlanetShipNo.text = _controller.Ships.ToString();
    }

    public void StartBattle()
    {
        _battleInProgress = true;
        _timeLeft = BattleDelay;
    }

    public void Retreate()
    {
        GameManager.Instance.Selector.RefreshDisplay();
        gameObject.SetActive(false);
    }

    public void Deploy()
    {
        GameManager.Instance.GroundBattleController.SetActive(true);
        GameManager.Instance.GroundBattleController.GetComponent<GroundBattleController>().StartGroundBattle(_controller.gameObject);
        gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (_controller.Ships <= 0)
        {
            _controller.Ships = 0;
            _battleInProgress = false;
            Banner.text = "Victory!";
        }

        if (_empire.Ships <= 0)
        {
            _empire.Ships = 0;
            _battleInProgress = false;
            Banner.text = "Defeat!";
        }

        if (_battleInProgress)
        {
            _timeLeft -= Time.fixedDeltaTime;


            if (_timeLeft <= 0)
            {
                _timeLeft = BattleDelay;

                _controller.Ships -= Random.Range(MinUnits, MaxUnits);
                _empire.Ships -= Random.Range(MinUnits, MaxUnits);
            }
        }

        UpdateStats();
    }
}
