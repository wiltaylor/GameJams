using UnityEngine;

public class PlayerEnergyController : MonoBehaviour
{
    private EnergyInput _context;

    public int Energy = 5;
    public int MaxEnergy = 5;
    public float EnergyEmissionTimeOut = 1f;

    private float _currentTimeOut = 0f;
    private OrbitController _orbitController;

    void Start()
    {
        _orbitController = GetComponentInChildren<OrbitController>();
    }

    public void Update()
    {

        if (_currentTimeOut > 0f)
        {
            _currentTimeOut -= Time.deltaTime;
            return;
        }

        if (Input.GetButton("PushEnergy"))
        {
            _currentTimeOut = EnergyEmissionTimeOut;

            if (Energy <= 0)
                return;

            if (_context == null)
                return;

            if (_context.Energy >= _context.MaxEnergy)
                return;
            
            Energy--;
            _context.AddEnergy();
            _orbitController.UseOrb(_context.transform);


        }

        if (Input.GetButton("PopEnergy"))
        {
            _currentTimeOut = EnergyEmissionTimeOut;

            if (Energy >= MaxEnergy)
                return;

            if (_context == null)
                return;

            if (_context.Energy <= 0)
                return;

            Energy++;
            _context.RemoveEnergy();

        }
    }

    public void OnContext(GameObject obj)
    {
        _context = obj.GetComponent<EnergyInput>();
    }

    public void ClearContext(GameObject obj)
    {
        if (_context == obj.GetComponent<EnergyInput>())
            _context = null;
    }

}
