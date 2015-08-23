using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public Text MusicButtonText;
    public Text SoundFXButtonText;
    public AudioMixer Mixer;

    public void CloseClicked()
    {
        gameObject.SetActive(false);
    }

    public void MusicClicked()
    {
        var vol = 0f;

        Mixer.GetFloat("MusicVol", out vol);

        if (vol == -80)
        {
            Mixer.SetFloat("MusicVol", 0);
            MusicButtonText.text = "Music: On";
        }
        else
        {
            Mixer.SetFloat("MusicVol", -80f);
            MusicButtonText.text = "Music: Off";
        }
    }

    public void SoundFXClicked()
    {
        var vol = 0f;

        Mixer.GetFloat("SFXVol", out vol);

        if (vol == -80)
        {
            Mixer.SetFloat("SFXVol", 0);
            SoundFXButtonText.text = "SFX: On";
        }
        else
        {
            Mixer.SetFloat("SFXVol", -80f);
            SoundFXButtonText.text = "SFX: Off";
        }
    }

    public void RestartGameClicked()
    {
        Debug.Log("Restart game clicked");
    }
}
