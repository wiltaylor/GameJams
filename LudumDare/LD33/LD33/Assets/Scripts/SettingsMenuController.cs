using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public Text MusicButtonText;
    public Text SoundFXButtonText;

    public void CloseClicked()
    {
        gameObject.SetActive(false);
    }

    public void MusicClicked()
    {
        MusicButtonText.text = MusicButtonText.text.Contains("On") ? " Music: Off" : " Music: On";
    }

    public void SoundFXClicked()
    {
        SoundFXButtonText.text = SoundFXButtonText.text.Contains("On") ? " SoundFX: Off" : " SoundFX: On";
    }

    public void RestartGameClicked()
    {
        Debug.Log("Restart game clicked");
    }
}
