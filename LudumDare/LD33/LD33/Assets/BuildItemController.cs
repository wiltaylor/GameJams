using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildItemController : MonoBehaviour
{
    public float TimeToBuild = 90f;
    public BuildTypes BuildType;
    public GameObject Prefab;
    public Text CountDown;
    public GameObject Room;
    public GameObject SpawnPoint;

    public enum BuildTypes
    {
        Unit,
        Room
    }

    public void FixedUpdate()
    {
        if (TimeToBuild <= 0f)
        {
            if (BuildType == BuildTypes.Room)
            {
                RoomManager.Instance.AddRoom(Instantiate(Prefab));
            }

            if (BuildType == BuildTypes.Unit)
            {
                var obj = (GameObject)Instantiate(Prefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
                obj.SendMessage("OnSpawn", Room);
            }

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            TimeToBuild -= Time.fixedDeltaTime;
            CountDown.text = ((int)TimeToBuild).ToString() + " Seconds";
        }
    }
}
