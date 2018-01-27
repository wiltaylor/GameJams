using UnityEngine;
using UnityEngine.Events;

public class EnergyInput : MonoBehaviour
{

    public EnergyInput[] LinkedInputs;
    public Powerable[] PoweredObjects;
    public int Energy;
    public int MaxEnergy;
    public float EmissionPerLevel = 0.1f;

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

        _activatedAnimated.state = ActivatedAnimated.ACTIVATIONSTATE.TURING_ON;
       // _activatedAnimated.EmissionMax = Energy * EmissionPerLevel;

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
            _activatedAnimated.state = ActivatedAnimated.ACTIVATIONSTATE.ON;
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
