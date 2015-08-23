using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitTrainingWindowController : MonoBehaviour
{
    public GameObject Room;
    public Text GoldText;
    public Text GemText;
    public GameObject UnitPurchasePrefab;
    public Button BuyButton;
    public UnitTypes UnitType = UnitTypes.SwordMan;

    private UnitTrainingRoomController _controller;
    private int GoldCost = 0;
    private int GemCost = 0;

    public void Start()
    {
        if (UnitType == UnitTypes.SwordMan)
        {
            GoldCost = GameManager.Instance.ShopMan.SwordManGoldCost;
            GemCost = GameManager.Instance.ShopMan.SwordManGemCost;
        }
    }

    public enum UnitTypes
    {
        SwordMan,
        Wizard
    }

    public void SetRoom(GameObject room)
    {
        Room = room;
        _controller = room.GetComponent<UnitTrainingRoomController>();
    }

    public void FixedUpdate()
    {
        if (GameManager.Instance.Gold < GoldCost ||
            GameManager.Instance.Gems < GemCost)
            BuyButton.interactable = false;

        GemText.text = GemCost.ToString();
        GoldText.text = GoldCost.ToString();
    }

    public void BuyClicked()
    {
        if (GameManager.Instance.Gold < GoldCost ||
            GameManager.Instance.Gems < GemCost)
            return;

        var obj = Instantiate(UnitPurchasePrefab).GetComponent<BuildItemController>();
        obj.SpawnPoint = _controller.SpawnPoint;
        obj.Room = Room;
        GameManager.Instance.Gold -= GoldCost;
        GameManager.Instance.Gems -= GemCost;

        GameManager.Instance.QueueManager.Enqueue(obj.gameObject);
    }
}

