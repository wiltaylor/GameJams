using UnityEngine;
using System.Collections;

public class RoomSettingsManager : MonoBehaviour
{
    public GameObject BossRoomSettings;
    public GameObject GemMineRoomSettings;
    public GameObject GoldMineRoomSettings;
    public GameObject BarracksRoomSettings;
    public GameObject LibraryRoomSettings;

    public void OpenRoomSettings(GameObject room)
    {
        if (room.name.StartsWith("BossRoom"))
        {
            BossRoomSettings.SetActive(true);
        }

        if (room.name.StartsWith("GemMineRoom"))
        {
            GemMineRoomSettings.SetActive(true);
            var ctrl = GemMineRoomSettings.GetComponent<ResourceWindowController>();
            ctrl.SetRoom(room);
        }

        if (room.name.StartsWith("GoldMineRoom"))
        {
            GoldMineRoomSettings.SetActive(true);
            var ctrl = GoldMineRoomSettings.GetComponent<ResourceWindowController>();
            ctrl.SetRoom(room);
        }

        if (room.name.StartsWith("Barracks"))
        {
            BarracksRoomSettings.SetActive(true);
            var ctrl = BarracksRoomSettings.GetComponent<UnitTrainingWindowController>();
            ctrl.SetRoom(room);
        }

        if (room.name.StartsWith("Library"))
        {
            LibraryRoomSettings.SetActive(true);
            var ctrl = LibraryRoomSettings.GetComponent<UnitTrainingWindowController>();
            ctrl.SetRoom(room);
        }
    }

    public void CloseBossRoomWindow()
    {
        BossRoomSettings.SetActive(false);
    }

    public void CloseGemMineRoomWindow()
    {
        GemMineRoomSettings.SetActive(false);
    }

    public void CloseGoldMineRoomWindow()
    {
        GoldMineRoomSettings.SetActive(false);
    }

    public void CloseBarracksWindow()
    {
        BarracksRoomSettings.SetActive(false);
    }

    public void CloseLibraryWindow()
    {
        LibraryRoomSettings.SetActive(false);
    }
}
