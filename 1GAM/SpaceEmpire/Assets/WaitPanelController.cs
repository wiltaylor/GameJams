using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitPanelController : MonoBehaviour
{

    public Text WaitValue;
    public Slider WaitSlider;

    public void StartWaitPanel()
    {
        WaitSlider.value = 0;
        UpdateValues();
    }

    public void Ok()
    {
        GameManager.Instance.PassTime((int)WaitSlider.value);
        gameObject.SetActive(false);
        GameManager.Instance.Selector.RefreshDisplay();
    }

    public void UpdateValues()
    {
        WaitValue.text = WaitSlider.value.ToString();
    }
}
