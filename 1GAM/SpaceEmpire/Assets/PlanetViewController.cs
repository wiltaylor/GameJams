using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlanetViewController : MonoBehaviour
{
    public Text PlanetNameText;
    public Image PlanetIcon;
    public Text EmpireTroops;
    public Text EmpireShips;
    public Text EmpireFood;
    public Text EmpireFuel;

    public Text PlanetTroops;
    public Text PlanetShips;
    public Text PlanetFood;
    public Text PlanetFuel;

    public Text PlanetGenerateTroops;
    public Text PlanetGenerateShips;
    public Text PlanetGenerateFood;
    public Text PlanetGenerateFuel;

    public Text PlanetRequiredFood;
    public Text PlanetRequiredFuel;
    public Text PlanetRequiredFuelToSpy;
    public Color RequiredEnoughColour;
    public Color NotEnoughColour;

    public Button TransferButton;
    public Button AttackButton;
    public Button FlyToButton;
    public Button SpyButton;

    

    public void SelectPlanet(GameObject target)
    {
        var controller = target.GetComponent<PlanetController>();
        PlanetNameText.text = controller.PlanetName;
        PlanetIcon.sprite = target.GetComponent<Image>().sprite;

        //Fly To Requirements
        var requiredFood = GameManager.Instance.Empire.FoodToFlyTo(target);
        var requiredFuel = GameManager.Instance.Empire.FuelToFlyTo(target);
        var requiredFuelToSpy = GameManager.Instance.Empire.FuelToSpy(target);

        PlanetRequiredFood.text = requiredFood.ToString();
        PlanetRequiredFuel.text = requiredFuel.ToString();
        PlanetRequiredFuelToSpy.text = requiredFuelToSpy.ToString();

        PlanetRequiredFood.color = requiredFood > GameManager.Instance.Empire.Food ? NotEnoughColour : RequiredEnoughColour;
        PlanetRequiredFuel.color = requiredFuel > GameManager.Instance.Empire.Fuel ? NotEnoughColour : RequiredEnoughColour;
        PlanetRequiredFuelToSpy.color = requiredFuelToSpy > GameManager.Instance.Empire.Fuel ? NotEnoughColour : RequiredEnoughColour;
        
        //Empire Stats
        EmpireTroops.text = GameManager.Instance.Empire.Troops.ToString();
        EmpireShips.text = GameManager.Instance.Empire.Ships.ToString();
        EmpireFood.text = GameManager.Instance.Empire.Food.ToString();
        EmpireFuel.text = GameManager.Instance.Empire.Fuel.ToString();

        if (GameManager.Instance.Empire.CurrentPlanet == target)
        {
            SpyButton.enabled = false;
            FlyToButton.enabled = false;

            if (controller.PlayerOwned)
            {
                TransferButton.enabled = true;
                AttackButton.enabled = false;
            }
            else
            {
                TransferButton.enabled = false;
                AttackButton.enabled = true;
            }
        }
        else
        {
            TransferButton.enabled = false;
            AttackButton.enabled = false;
            FlyToButton.enabled = true;
            
            if (controller.PlayerOwned)
                SpyButton.enabled = false;
            else
                SpyButton.enabled = !controller.SpySats;
        }

        //Planet Stats
        if (controller.PlayerOwned || controller.SpySats || GameManager.Instance.Empire.CurrentPlanet == target)
        {
            PlanetTroops.text = controller.Troops.ToString();
            PlanetShips.text = controller.Ships.ToString();
            PlanetFood.text = controller.Food.ToString();
            PlanetFuel.text = controller.Fuel.ToString();
        }
        else
        {
            PlanetTroops.text = "?";
            PlanetShips.text = "?";
            PlanetFood.text = "?";
            PlanetFuel.text = "?";
        }

        //Planet Rates
        if (controller.PlayerOwned)
        {
            PlanetGenerateTroops.text = controller.TroopProductionRate.ToString();
            PlanetGenerateShips.text = controller.ShipProductionRate.ToString();
            PlanetGenerateFood.text = controller.FoodProductionRate.ToString();
            PlanetGenerateFuel.text = controller.FuelProductionRate.ToString();
        }
        else
        {
            PlanetGenerateTroops.text = "-";
            PlanetGenerateShips.text = "-";
            PlanetGenerateFood.text = "-";
            PlanetGenerateFuel.text = "-";
        }

        if (requiredFood > GameManager.Instance.Empire.Food || requiredFuel > GameManager.Instance.Empire.Fuel) 
            FlyToButton.enabled = false;
        
        TransferButton.gameObject.SetActive(TransferButton.enabled);
        AttackButton.gameObject.SetActive(AttackButton.enabled);
        FlyToButton.gameObject.SetActive(FlyToButton.enabled);
        SpyButton.gameObject.SetActive(SpyButton.enabled);
    }
	
}
