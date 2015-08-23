using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HowToPlayController : MonoBehaviour
{
    public int PageIndex = 0;
    public GameObject[] Pages;
    public Button NextButton;
    public Button BackButton;

    public void Update()
    {
        if (PageIndex == 0)
        {
            BackButton.interactable = false;
        }
        else
        {
            BackButton.interactable = true;
        }

        if (PageIndex == Pages.Length - 1)
        {
            NextButton.interactable = false;
        }
        else
        {
            NextButton.interactable = true;
        }
    }

    public void Show()
    {
        HidePages();
        Pages[0].SetActive(true);
    }

    void HidePages()
    {
        foreach (var t in Pages)
            t.SetActive(false);
    }

    public void NextClicked()
    {
        PageIndex++;
        HidePages();

        Pages[PageIndex].SetActive(true);
    }

    public void BackClicked()
    {
        PageIndex--;
        HidePages();

        Pages[PageIndex].SetActive(true);
    }
    public void CloseClicked()
    {
        HidePages();
        gameObject.SetActive(false);
    }
}
