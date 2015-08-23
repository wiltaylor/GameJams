using System;
using UnityEngine;
using System.Collections;

public class ActionBarController : MonoBehaviour
{
    public GameObject MainBar;
    public GameObject RoomBar;
    public GameObject MinionBar;
    public GameObject SpellBar;
    public GameObject TrapBar;

    public GameObject BackButton;
    public GameObject SettingButton;
    public GameObject SettingsWindow;

    public enum ActionBars
    {
        MainBar,
        RoomBar,
        MinionBar,
        SpellBar,
        TrapBar
    }

    public void SelectMainBar()
    {
        SelectBar(ActionBars.MainBar);        
    }

    public void SelectRoomBar()
    {
        SelectBar(ActionBars.RoomBar);
    }

    public void SelectMinionBar()
    {
        SelectBar(ActionBars.MinionBar);
    }

    public void SelectSpellBar()
    {
        SelectBar(ActionBars.SpellBar);
    }

    public void SelectTrapBar()
    {
        SelectBar(ActionBars.TrapBar);
    }

    public void SelectSettings()
    {
        SettingsWindow.SetActive(true);
    }

    public void SelectBar(ActionBars bar)
    {
        MainBar.SetActive(false);
        RoomBar.SetActive(false);
        MinionBar.SetActive(false);
        SpellBar.SetActive(false);
        TrapBar.SetActive(false);
        BackButton.SetActive(true);
        SettingButton.SetActive(true);

        switch (bar)
        {
            case ActionBars.MainBar:
                MainBar.SetActive(true);
                BackButton.SetActive(false);
                break;
            case ActionBars.MinionBar:
                MinionBar.SetActive(true);
                break;
            case ActionBars.RoomBar:
                RoomBar.SetActive(true);
                break;
            case ActionBars.SpellBar:
                SpellBar.SetActive(true);
                break;
            case ActionBars.TrapBar:
                TrapBar.SetActive(true);
                break;
        }
    }

    public void ClickBuyGoldMine()
    {
        GameManager.Instance.BuyRoom(GameManager.RoomType.GoldMine);
    }

    public void ClickBuyGemMine()
    {
        GameManager.Instance.BuyRoom(GameManager.RoomType.GemMine);
    }
}
