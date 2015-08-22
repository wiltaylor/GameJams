using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopBarController : MonoBehaviour
{

    public Text GoldText;
    public Text FoodText;
    public Text GemText;
    public Text DesireText;
    public Text EvilText;

    public void Update()
    {
        GoldText.text = GameManager.Instance.Gold.ToString();
        FoodText.text = GameManager.Instance.Food.ToString();
        GemText.text = GameManager.Instance.Gems.ToString();
        DesireText.text = GameManager.Instance.Desire.ToString();
        EvilText.text = GameManager.Instance.Evil.ToString();
    }
}
