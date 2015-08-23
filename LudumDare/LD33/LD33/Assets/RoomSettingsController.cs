using UnityEngine;
using System.Collections;

public class RoomSettingsController : MonoBehaviour
{
    public GameObject RoomObject;

    public void OnMouseDown()
    {
        GameManager.Instance.SettingManager.OpenRoomSettings(RoomObject);
    }

}
