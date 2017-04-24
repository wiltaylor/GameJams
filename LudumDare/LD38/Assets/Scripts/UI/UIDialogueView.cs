using System;
using Assets.Systems.dx;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueView : MonoBehaviour
{
    public GameObject AvatarContainer;
    public Image AvatarImage;
    public GameObject TextContainer;
    public Text Text;
    public Sprite Knight;
    public Sprite Cardinal;
    public Sprite Deamon;

    private void Update()
    {
        var dlg = DialogueService.Instance;

        if (!dlg.Active)
            return;

        switch (dlg.CurrentCharecter)
        {
            case DialogueCharecter.None:
                AvatarContainer.SetActive(false);
                break;
            case DialogueCharecter.Knight:
                AvatarContainer.SetActive(true);
                AvatarImage.sprite = Knight;
                break;
            case DialogueCharecter.Cardinal:
                AvatarContainer.SetActive(true);
                AvatarImage.sprite = Cardinal;
                break;
            case DialogueCharecter.Deamon:
                AvatarContainer.SetActive(true);
                AvatarImage.sprite = Deamon;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Text.text = dlg.CurrentText;
    }

    public void ClickNext()
    {
        DialogueService.Instance.Next();
    }
}
