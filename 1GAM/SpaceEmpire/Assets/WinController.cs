using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{

    public Text YearText;

    public void StartWinController()
    {
        YearText.text = string.Format("{0} AE", GameManager.Instance.Year);
    }

    public void StartAgain()
    {
        GameManager.Instance.gameObject.GetComponent<LevelGenerator>().GenerateLevel();
        gameObject.SetActive(false);
    }
}
