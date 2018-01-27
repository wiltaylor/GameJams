using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerSliderController : MonoBehaviour
{
    public AudioMixer Mixer;


    public string VolumeTarget;

    private Slider _slider;

    void Start()
    {
        float vol;
        _slider = GetComponent<Slider>();
        Mixer.GetFloat(VolumeTarget, out vol);
        _slider.value = vol;
    }

    public void UpdateAudio()
    {
        Mixer.SetFloat(VolumeTarget, _slider.value);
    }

}
