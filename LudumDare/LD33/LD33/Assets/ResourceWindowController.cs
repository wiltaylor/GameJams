using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceWindowController : MonoBehaviour
{
    public GameObject Room;
    public Text GoldText;
    public Text GemText;
    public Text QtyText;
    public GameObject UnitPurchasePrefab;
    public Button BuyButton;

    private ResourceRoomController _controller;

    public void SetRoom(GameObject room)
    {
        Room = room;
        _controller = room.GetComponent<ResourceRoomController>();
    }

    public void FixedUpdate()
    {
        var maxWorkers = _controller.MaxWorkers;
        var curWorkers = _controller.GetCurrentWorkers();

        BuyButton.interactable = curWorkers < maxWorkers;

        if (GameManager.Instance.Gold < GameManager.Instance.ShopMan.ImpGemCost ||
            GameManager.Instance.Gems < GameManager.Instance.ShopMan.ImpGemCost)
            BuyButton.interactable = false;

        GemText.text = GameManager.Instance.ShopMan.ImpGemCost.ToString();
        GoldText.text = GameManager.Instance.ShopMan.ImpGoldCost.ToString();
        QtyText.text = curWorkers + " / " + maxWorkers;
    } 

    public void BuyClicked()
    {
        if (GameManager.Instance.Gold < GameManager.Instance.ShopMan.ImpGoldCost ||
            GameManager.Instance.Gems < GameManager.Instance.ShopMan.ImpGemCost)
            return;

        var obj = Instantiate(UnitPurchasePrefab).GetComponent<BuildItemController>();
        obj.SpawnPoint = _controller.SpawnPoint;
        obj.Room = Room;
        
        GameManager.Instance.QueueManager.Enqueue(obj.gameObject);
    }

}
