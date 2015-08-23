using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{

    public GameObject GoldMinePrefab;
    public GameObject GemMinePrefab;
    public GameObject BarracksPrefab;
    public GameObject LibraryPrefab;

    public int GoldMineGoldCost = 1000;
    public int GemMineGoldCost = 5000;
    public int GoldMineGemCost = 0;
    public int GemMineGemCost = 0;
    public int BarracksGoldCost = 5;
    public int BarracksGemCost = 0;
    public int LibraryGoldCost = 5;
    public int LibraryGemCost = 0;

    public int ImpGoldCost = 100;
    public int ImpGemCost = 0;
    public int SwordManGoldCost = 200;
    public int SwordManGemCost = 0;
    public int WizardGoldCost = 200;
    public int WizardGemcost = 100;

    public void BuyGoldMine()
    {
        if (GameManager.Instance.Gold < GoldMineGoldCost || GameManager.Instance.Gems < GoldMineGemCost)
        {
            //not enough resoruces
            return;
        }

        GameManager.Instance.Gold -= GoldMineGoldCost;
        GameManager.Instance.Gems -= GoldMineGemCost;
        GameManager.Instance.QueueManager.Enqueue(Instantiate(GoldMinePrefab));
    }

    public void BuyGemMine()
    {
        if (GameManager.Instance.Gold < GemMineGoldCost || GameManager.Instance.Gems < GemMineGemCost)
        {
            //not enough resoruces
            return;
        }

        GameManager.Instance.Gold -= GemMineGoldCost;
        GameManager.Instance.Gems -= GemMineGemCost;
        GameManager.Instance.QueueManager.Enqueue(Instantiate(GemMinePrefab));
    }

    public void BuyBarracks()
    {
        if (GameManager.Instance.Gold < BarracksGoldCost || GameManager.Instance.Gems < BarracksGemCost)
        {
            //not enough resoruces
            return;
        }

        GameManager.Instance.Gold -= BarracksGoldCost;
        GameManager.Instance.Gems -= BarracksGemCost;
        GameManager.Instance.QueueManager.Enqueue(Instantiate(BarracksPrefab));
    }

    public void BuyLibrary()
    {
        if (GameManager.Instance.Gold < LibraryGoldCost || GameManager.Instance.Gems < LibraryGemCost)
        {
            //not enough resoruces
            return;
        }

        GameManager.Instance.Gold -= LibraryGoldCost;
        GameManager.Instance.Gems -= LibraryGemCost;
        GameManager.Instance.QueueManager.Enqueue(Instantiate(LibraryPrefab));
    }
}
