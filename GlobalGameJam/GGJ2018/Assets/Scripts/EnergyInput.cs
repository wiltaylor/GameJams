using UnityEngine;
using UnityEngine.Events;

public class EnergyInput : MonoBehaviour
{

    public EnergyInput[] LinkedInputs;
    public Powerable[] PoweredObjects;
    public int Energy;
    public int MaxEnergy;

    private ActivatedAnimated _activatedAnimated;

    void Start()
    {
        _activatedAnimated = GetComponent<ActivatedAnimated>();
        UpdateEnergy();
    }

    private void UpdateEnergy()
    {
        foreach(var obj in PoweredObjects)
            obj.OnPowerChange(Energy);
    }

    public void AddEnergy()
    {
        Energy++;

        _activatedAnimated.state = ActivatedAnimated.ACTIVATIONSTATE.ON;

        UpdateEnergy();


        foreach (var link in LinkedInputs)
            link.AddEnergy();
    }

    public void RemoveEnergy()
    {
        Energy--;

        UpdateEnergy();


        foreach (var link in LinkedInputs)
            link.RemoveEnergy();

        if(Energy <= 0)
            _activatedAnimated.state = ActivatedAnimated.ACTIVATIONSTATE.OFF;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("OnContext", gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("ClearContext", gameObject);
        }
    }
}
