using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildItemController : MonoBehaviour
{
    public float TimeToBuild = 90f;
    public BuildTypes BuildType;
    public GameObject Prefab;
    public Text CountDown;

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

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            TimeToBuild -= Time.fixedDeltaTime;
            CountDown.text = TimeToBuild.ToString();
        }
    }
}
