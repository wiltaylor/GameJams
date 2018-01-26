using UnityEngine;
using UnityEngine.Events;

public class PowerReceiver : MonoBehaviour
{
    public Material PoweredMaterial;
    public Material UnPoweredMaterial;

    public UnityEvent OnPowerOn;
    public UnityEvent OnPowerOff;

    private MeshRenderer _render;

    void Start()
    {
        _render = GetComponent<MeshRenderer>();
    }

    public void OnPowerChanged(bool powered)
    {
        if (powered)
        {
            OnPowerOn.Invoke();
            _render.material = PoweredMaterial;
        }
        else
        {
            OnPowerOff.Invoke();
            _render.material = UnPoweredMaterial;
        }
    }

}
