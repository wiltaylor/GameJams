using UnityEngine;
using System.Collections;

public class ResourceRoomController : MonoBehaviour
{
    public GameObject ResourceDrop;
    public GameObject ResourceEnterance;
    public GameObject BottomOfLadder;
    public GameObject TopOfLadder;
    public ResourceTypes ResourceType;

    public enum ResourceTypes
    {
        Gold,
        Gems
    }
}
