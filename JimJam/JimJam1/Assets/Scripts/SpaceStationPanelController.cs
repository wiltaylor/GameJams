using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceStationPanelController : MonoBehaviour
{


    public PlayerController Player;
    public Button FuelButton;
    public Text WelcomeText;

    void Update()
    {
        FuelButton.enabled = !(Player.Fuel >= Player.MaxFuel);
        WelcomeText.text = "Welcome to the " + Player.CurrentSpaceStaion.Name +
                           " Station. Please select from the following services.";

    }

	
}
