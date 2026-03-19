using UnityEngine;

public class SpaceStationController : MonoBehaviour
{

    public float FuelCostPerUnit;
    public ShopItem[] ShopItems;
    public string Name;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.CurrentSpaceStaion = this;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.CurrentSpaceStaion = null;
        }
    }
}
