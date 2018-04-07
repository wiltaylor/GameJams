using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public PlayerController Player;
    public GameObject Content;
    public GameObject ShopItemPrefab;
    public float YOffset = 100f;

    void OnEnable()
    {
        Player = PlayerController.Instance;

        //Clean out previous store items.
        foreach(var obj in Content.transform)
            Destroy(Content);

        foreach (var item in Player.CurrentSpaceStaion.ShopItems)
        {
            var prefab = Instantiate(ShopItemPrefab);

            prefab.transform.SetParent(Content.transform);

            prefab.SetActive(true);

            var controller = prefab.GetComponent<ShopItemController>();

            controller.Player = Player;
            controller.CargoItem = item.Item;
            controller.CostPerUnit = item.Cost;
            
        }
    }


}
