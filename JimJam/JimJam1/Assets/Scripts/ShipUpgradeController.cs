using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeController : MonoBehaviour
{

    public PlayerController Player;

    //Buttons
    public Button BeamButton;
    public Button RocketButton;
    public Button EngineButton;
    public Button ShieldButton;
    public Button CargoButton;

    //Levels
    public Text BeamText;
    public Text RocketText;
    public Text EngineText;
    public Text ShieldText;
    public Text CargoText;

    void Start()
    {
        Player = PlayerController.Instance;
    }

	void Update ()
	{
	    BeamButton.enabled = Player.BeamWeaponLevel < Player.UpgradeData.BeamWeapon.Length;
	    RocketButton.enabled = Player.RocketWeaponLevel < Player.UpgradeData.RocketWeapon.Length;
	    EngineButton.enabled = Player.EngineLevel < Player.UpgradeData.Engines.Length;
	    ShieldButton.enabled = Player.ShieldLevel < Player.UpgradeData.Shields.Length;
	    CargoButton.enabled = Player.CargoLevel < Player.UpgradeData.CargoCapacity.Length;

	    BeamText.text = Player.BeamWeaponLevel >= Player.UpgradeData.BeamWeapon.Length
	        ? "Level MAX"
	        : "Level " + (Player.BeamWeaponLevel + 1) + " - $" + Player.UpgradeData.BeamWeapon[Player.BeamWeaponLevel].Cost;

	    RocketText.text = Player.RocketWeaponLevel >= Player.UpgradeData.RocketWeapon.Length
	        ? "Level MAX"
	        : "Level " + (Player.RocketWeaponLevel + 1) + " - $" + Player.UpgradeData.RocketWeapon[Player.RocketWeaponLevel].Cost;

	    EngineText.text = Player.EngineLevel >= Player.UpgradeData.Engines.Length
	        ? "Level MAX"
	        : "Level " + (Player.EngineLevel + 1) + " - $" + Player.UpgradeData.Engines[Player.EngineLevel].Cost;

	    ShieldText.text = Player.ShieldLevel >= Player.UpgradeData.Shields.Length
	        ? "Level MAX"
	        : "Level " + (Player.ShieldLevel + 1) + " - $" + Player.UpgradeData.Shields[Player.ShieldLevel].Cost;

	    CargoText.text = Player.CargoLevel >= Player.UpgradeData.CargoCapacity.Length
	        ? "Level MAX"
	        : "Level " + (Player.CargoLevel + 1) + " - $" + Player.UpgradeData.CargoCapacity[Player.CargoLevel].Cost;
    }

    public void UpdateBeam()
    {
        Player.Credits -= Player.UpgradeData.BeamWeapon[Player.BeamWeaponLevel].Cost;
        Player.BeamWeaponLevel++;
        Player.UpdateStats();
        Player.Resupply();
    }

    public void UpdateRocket()
    {
        Player.Credits -= Player.UpgradeData.RocketWeapon[Player.RocketWeaponLevel].Cost;
        Player.RocketWeaponLevel++;
        Player.UpdateStats();
        Player.Resupply();
    }

    public void UpdateEngine()
    {
        Player.Credits -= Player.UpgradeData.Engines[Player.EngineLevel].Cost;
        Player.EngineLevel++;
        Player.UpdateStats();
        Player.Resupply();
    }

    public void UpdateShield()
    {
        Player.Credits -= Player.UpgradeData.Shields[Player.ShieldLevel].Cost;
        Player.ShieldLevel++;
        Player.UpdateStats();
        Player.Resupply();
    }

    public void UpdateCargo()
    {
        Player.Credits -= Player.UpgradeData.CargoCapacity[Player.CargoLevel].Cost;
        Player.CargoLevel++;
        Player.UpdateStats();
        Player.Resupply();
    }
}
