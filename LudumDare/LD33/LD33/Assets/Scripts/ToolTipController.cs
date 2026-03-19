using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour
{
    public Text GemText;
    public Text GoldText;
    public Text TipText;
    public GameObject ToolTipWindow;

    public void SetToolTip(string gold, string gems, string tip)
    {
        GemText.text = gems;
        GoldText.text = gold;
        TipText.text = tip;
    }

    public void ShowTip()
    {
        ToolTipWindow.SetActive(true);
    }

    public void Hidetip()
    {
        ToolTipWindow.SetActive(false);
    }

    public void ShowGoldMineTip()
    {
        SetToolTip(GameManager.Instance.ShopMan.GoldMineGoldCost.ToString(), 
            GameManager.Instance.ShopMan.GoldMineGemCost.ToString(), 
            "Gold mines are your only source of Gold.");

        ShowTip();
    }

    public void ShowGemMineTip()
    {
        SetToolTip(GameManager.Instance.ShopMan.GemMineGoldCost.ToString(),
            GameManager.Instance.ShopMan.GemMineGemCost.ToString(),
            "Gem mines are your only source of Gems.");

        ShowTip();
    }

    public void ShowBarracksTip()
    {
        SetToolTip(GameManager.Instance.ShopMan.BarracksGoldCost.ToString(),
            GameManager.Instance.ShopMan.BarracksGemCost.ToString(),
            "The baracks let you train basic sword men.");

        ShowTip();
    }

    public void ShowLibraryTip()
    {
        SetToolTip(GameManager.Instance.ShopMan.LibraryGoldCost.ToString(),
    GameManager.Instance.ShopMan.LibraryGemCost.ToString(),
    "The Library lets you train Wizards.");

        ShowTip();
    }
}
