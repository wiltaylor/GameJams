using System;
using UnityEngine;
using System.Collections;

public class ActionBarController : MonoBehaviour
{
    public GameObject MainBar;
    public GameObject RoomBar;
    public GameObject SpellBar;

    public GameObject BackButton;
    public GameObject SettingButton;
    public GameObject SettingsWindow;

    public enum ActionBars
    {
        MainBar,
        RoomBar,
        SpellBar,
    }

    public void SelectMainBar()
    {
        SelectBar(ActionBars.MainBar);        
    }

    public void SelectRoomBar()
    {
        SelectBar(ActionBars.RoomBar);
    }

    public void SelectSpellBar()
    {
        SelectBar(ActionBars.SpellBar);
    }


    public void SelectSettings()
    {
        SettingsWindow.SetActive(true);
    }

    public void SelectBar(ActionBars bar)
    {
        MainBar.SetActive(false);
        RoomBar.SetActive(false);
        SpellBar.SetActive(false);
        BackButton.SetActive(true);
        SettingButton.SetActive(true);

        switch (bar)
        {
            case ActionBars.MainBar:
                MainBar.SetActive(true);
                BackButton.SetActive(false);
                break;
            case ActionBars.RoomBar:
                RoomBar.SetActive(true);
                break;
            case ActionBars.SpellBar:
                SpellBar.SetActive(true);
                break;
        }
    }

    public void ClickBuyGoldMine()
    {
        GameManager.Instance.ShopMan.BuyGoldMine();
    }

    public void ClickBuyGemMine()
    {
        GameManager.Instance.ShopMan.BuyGemMine();
    }

    public void ClickBuyBarracks()
    {
        GameManager.Instance.ShopMan.BuyBarracks();
    }
}
