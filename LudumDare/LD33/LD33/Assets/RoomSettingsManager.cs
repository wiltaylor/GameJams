using UnityEngine;
using System.Collections;

public class RoomSettingsManager : MonoBehaviour
{
    public GameObject BossRoomSettings;
    public GameObject GemMineRoomSettings;
    public GameObject GoldMineRoomSettings;

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
}
