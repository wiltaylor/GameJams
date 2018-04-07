using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public PlayerController Player;
    public CargoItem CargoItem;
    public float CostPerUnit;

    public Slider QtySlider;
    public Text Qty;
    public Text NameText;
    public Image ImageHolder;
    
    
    private int _playerQty = 0;
    private int _stationQty = 0;

    void Start()
    {
        UpdateSlider();
        QtySlider.value = 0f;
    }

    void UpdateSlider()
    {
        var item = Player.CargoItems.FirstOrDefault(c => c.Item.Name == CargoItem.Name);

        _playerQty = item == null ? 0 : item.Qty;

        _stationQty = Mathf.FloorToInt(Player.Credits / CostPerUnit);

        var maxItemsPerWait = Mathf.FloorToInt(Player.FreeCargoCapacity / CargoItem.WeightPerUnit);

        if (_stationQty > maxItemsPerWait)
            _stationQty = maxItemsPerWait;
    }


    void Update()
    {
        UpdateSlider();
        QtySlider.minValue = -_playerQty;
        QtySlider.maxValue = _stationQty;

        if (QtySlider.value > QtySlider.maxValue)
            QtySlider.value = QtySlider.maxValue;

        if (QtySlider.value < QtySlider.minValue)
            QtySlider.value = QtySlider.minValue;

        ImageHolder.sprite = CargoItem.Icon;
        
        Qty.text = Mathf.FloorToInt(QtySlider.value).ToString();
    }

    public void Confirm()
    {
        var item = Player.CargoItems.FirstOrDefault(c => c.Item.Name == CargoItem.Name);

        if (item == null)
        {
            item = new ShipCargoItem {Item = CargoItem, Qty = 0};
            Player.CargoItems.Add(item);
        }

        item.Qty += Mathf.FloorToInt(QtySlider.value);
        Player.Credits -= Mathf.FloorToInt(QtySlider.value * CostPerUnit);

        Player.CleanCargo();
        UpdateSlider();
    }
}
