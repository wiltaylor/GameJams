using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransferController : MonoBehaviour
{
    public Slider TroopSlider;
    public Slider ShipsSlider;
    public Slider FoodSlider;
    public Slider FuelSlider;

    public Text EmpireTroops;
    public Text EmpireShips;
    public Text EmpireFood;
    public Text EmpireFuel;

    public Text PlanetTroops;
    public Text PlanetShips;
    public Text PlanetFood;
    public Text PlanetFuel;

    public Text PlanetName;

    private PlanetController _controller;

    public void UpdateValues()
    {
        EmpireTroops.text = TroopSlider.value.ToString();
        EmpireShips.text = ShipsSlider.value.ToString();
        EmpireFood.text = FoodSlider.value.ToString();
        EmpireFuel.text = FuelSlider.value.ToString();

        PlanetTroops.text = (TroopSlider.maxValue - TroopSlider.value).ToString();
        PlanetShips.text = (ShipsSlider.maxValue - ShipsSlider.value).ToString();
        PlanetFood.text = (FoodSlider.maxValue - FoodSlider.value).ToString();
        PlanetFuel.text = (FuelSlider.maxValue - FuelSlider.value).ToString();
    }

    public void StartTransfer(GameObject target)
    {
        _controller = target.GetComponent<PlanetController>();
        var empire = GameManager.Instance.Empire;

        PlanetName.text = _controller.PlanetName;

        TroopSlider.maxValue = empire.Troops + _controller.Troops;
        ShipsSlider.maxValue = empire.Ships + _controller.Ships;
        FoodSlider.maxValue = empire.Food + _controller.Food;
        FuelSlider.maxValue = empire.Fuel + _controller.Fuel;

        TroopSlider.minValue = 0;
        ShipsSlider.minValue = 0;
        FoodSlider.minValue = 0;
        FuelSlider.minValue = 0;

        TroopSlider.value = empire.Troops;
        ShipsSlider.value = empire.Ships;
        FoodSlider.value = empire.Food;
        FuelSlider.value = empire.Fuel;

        UpdateValues();
    }

    public void CommitTransfer()
    {
        var empire = GameManager.Instance.Empire;

        empire.Troops = (int) TroopSlider.value;
        empire.Ships = (int) ShipsSlider.value;
        empire.Food = (int) FoodSlider.value;
        empire.Fuel = (int) FuelSlider.value;

        _controller.Troops = (int) (TroopSlider.maxValue - TroopSlider.value);
        _controller.Ships = (int) (ShipsSlider.maxValue - ShipsSlider.value);
        _controller.Food = (int) (FoodSlider.maxValue - FoodSlider.value);
        _controller.Fuel = (int) (FuelSlider.maxValue - FuelSlider.value);

        GameManager.Instance.Selector.RefreshDisplay();
        gameObject.SetActive(false);

    }

    public void TransferAll()
    {
        var empire = GameManager.Instance.Empire;

        empire.Troops = (int)TroopSlider.maxValue;
        empire.Ships = (int)ShipsSlider.maxValue;
        empire.Food = (int)FoodSlider.maxValue;
        empire.Fuel = (int)FuelSlider.maxValue;

        _controller.Troops = 0;
        _controller.Ships = 0;
        _controller.Food = 0;
        _controller.Fuel = 0;

        GameManager.Instance.Selector.RefreshDisplay();
        gameObject.SetActive(false);
    }

public void CancelTransfer()
    {
        GameManager.Instance.Selector.RefreshDisplay();
        gameObject.SetActive(false);
    }
}
