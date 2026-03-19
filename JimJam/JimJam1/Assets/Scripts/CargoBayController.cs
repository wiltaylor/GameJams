using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoBayController : MonoBehaviour
{
    public PlayerController Player;
    public GameObject Content;
    public GameObject CargoItemPrefab;
    public Text Capacity;

    void OnEnable()
    {
        Player = PlayerController.Instance;
        //Clean out previous store items.
        foreach (var obj in Content.transform)
            Destroy(Content);

        foreach (var item in Player.CargoItems)
        {
            var prefab = Instantiate(CargoItemPrefab);
            prefab.transform.SetParent(Content.transform);
            prefab.SetActive(true);

            var controller = prefab.GetComponent<CargoBayItem>();
            controller.Setup(item.Item.Icon, item.Item.Name, item.Qty);
        }

        Capacity.text = Player.FreeCargoCapacity + "/" + Player.CargoCapacity;
    }

}
