using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    private GameObject _bossRoom;
    private GameObject _entranceRoom;
    private List<GameObject> _rooms;

    public GameObject BossRoomPrefab;
    public GameObject EnterancePrefab;
    public float RoomTileWidth = 0.32f;
    public int RoomTileCount = 16;
    public static RoomManager Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ClearLevel()
    {
        foreach (var room in GameObject.FindGameObjectsWithTag("Room"))
        {
            Destroy(room);
        }

        foreach (var unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            Destroy(unit);
        }

        foreach (var hero in GameObject.FindGameObjectsWithTag("Hero"))
        {
            Destroy(hero);
        }
    }

    public void NewLevel()
    {
        ClearLevel();

        _bossRoom = Instantiate(BossRoomPrefab);
        _entranceRoom = Instantiate(EnterancePrefab);
        _rooms = new List<GameObject>();

        LineUpRooms();
    }

    public void LineUpRooms()
    {
        _bossRoom.transform.position = Vector3.zero;

        var roomindex = 1;
        var roomoffset = RoomTileWidth*RoomTileCount;

        foreach (var room in _rooms)
        {
            room.transform.position = new Vector3(roomindex * roomoffset, 0);
            roomindex++;
        }

        _entranceRoom.transform.position = new Vector3(roomindex * roomoffset, 0);
    }

    public void AddRoom(GameObject room)
    {
        _rooms.Add(room);
        LineUpRooms();
    }
}
