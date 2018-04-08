using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float MaxSpeedNoFuel = 10f;
    public float MaxSpeed = 100f;
    public float AccelerationAmmount = 5f;
    public float RotationSpeed = 10f;
    public bool FreeLookOn = false;
    public WeaponController Weapon;
    public string LastScene = "";
    public GameObject HudPrefab;

    public BeamWeaponController BeamWeapon;
    //Need to add rockets.
    
    private float _warpTimeOut = 0f;

    //Cameras
    public Camera MainCam;
    public Camera WarpCam;

    //Cargo
    public int Credits = 1000;
    public float Fuel = 100f;
    public float MaxFuel = 100f;
    public float FuelConsuption = 0.01f;
    public float BreakFuel = 1f;

    public float CargoCapacity = 100f;

    //Stats
    public int BeamWeaponLevel = 1;
    public int RocketWeaponLevel = 1;
    public int EngineLevel = 1;
    public int ShieldLevel = 1;
    public int CargoLevel = 1;

    public ShipUpgradeData UpgradeData;

    public List<ShipCargoItem> CargoItems;


    private static PlayerController _instance;
    private Destructable _destructable;

    public SpaceStationController CurrentSpaceStaion;

    public float FreeCargoCapacity
    {
        get
        {
            var capacity = CargoCapacity;

            foreach (var items in CargoItems)
                capacity -= items.Qty * items.Item.WeightPerUnit;
            
            return capacity;
        }
    }

    public void CleanCargo()
    {
        CargoItems.RemoveAll(c => c.Qty <= 0);
    }

    public bool SpaceStationInRange
    {
        get { return CurrentSpaceStaion != null; }
    }

    public static PlayerController Instance
    {
        get { return _instance; }
    }

    public void UpdateStats()
    {
        var beamWeaponData = UpgradeData.BeamWeapon[BeamWeaponLevel - 1];
        BeamWeapon.BeamColour = beamWeaponData.Colour;
        BeamWeapon.BeamDamage = beamWeaponData.Damage;
        BeamWeapon.distance = beamWeaponData.Range;
        BeamWeapon.BeamPunchThrough = beamWeaponData.BlastThrough;
        BeamWeapon.ResetBeam();

        //TODO: rocket 

        var engineData = UpgradeData.Engines[EngineLevel - 1];
        MaxSpeed = engineData.MaxSpeed;
        AccelerationAmmount = engineData.AccelerationAmount;
        MaxFuel = engineData.MaxFuel;
        FuelConsuption = engineData.FuelConsuption;
        BreakFuel = engineData.BreakFuel;

        var shieldData = UpgradeData.Shields[ShieldLevel - 1];
        _destructable.MaxHP = shieldData.MaxHP;
        _destructable.MaxShield = shieldData.MaxShield;
        _destructable.ShieldRechargeRate = shieldData.RechargeRate;

        CargoCapacity = UpgradeData.CargoCapacity[CargoLevel - 1].MaxCapacity;

    }

    public bool isWarping
    {
        get { return _warpTimeOut > 0f; }
    }

    public float CurrentSpeed
    {
        get { return _rigidbody.velocity.magnitude; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _instance = this;

        var hud = Instantiate(HudPrefab);
        hud.SetActive(true);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _destructable = GetComponent<Destructable>();

        UpdateStats();

        _destructable.HP = _destructable.MaxHP;
        _destructable.Shield = _destructable.MaxShield;

    }

    public void Warp(float time)
    {
        if (_warpTimeOut > 0f)
            return;

        FreeLookOn = false;

        _warpTimeOut = time;
        MainCam.enabled = false;
        WarpCam.gameObject.SetActive(true);
    }

    public void Brake()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        Fuel -= BreakFuel;
    }
	void Update ()
	{
	    if (Fuel <= 0f)
	    {
	        Fuel = 0f;
	        MaxSpeed = MaxSpeedNoFuel;
	    }

	    Cursor.visible = !FreeLookOn;
        Cursor.lockState = FreeLookOn ? CursorLockMode.Locked : CursorLockMode.None;

        if (_warpTimeOut > 0f)
        {
            _destructable.Invincible = true;
            _warpTimeOut -= Time.deltaTime;
            return;
        }

	    _destructable.Invincible = false;
        MainCam.enabled = true;
        WarpCam.gameObject.SetActive(false);
        
        if (Input.GetButtonDown("FreeLook"))
        {
            FreeLookOn = !FreeLookOn;

            if (!FreeLookOn)
                Weapon.ReleaseFire();
        }

        if (!FreeLookOn)
            return;


        if (Input.GetAxis("Vertical") > 0.1f)
        {
            _rigidbody.velocity += transform.forward * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = transform.forward * MaxSpeed;

            Fuel -= FuelConsuption * Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") < -0.1f)
        {
            _rigidbody.velocity += -transform.forward * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = -transform.forward * MaxSpeed;

            Fuel -= FuelConsuption * Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            _rigidbody.velocity += transform.right * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = transform.right * MaxSpeed;

            Fuel -= FuelConsuption * Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            _rigidbody.velocity += -transform.right * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = -transform.right * MaxSpeed;

            Fuel -= FuelConsuption * Time.deltaTime;
        }

        if (Input.GetAxis("ThrustUpDown") > 0.1f)
        {
            _rigidbody.velocity += transform.up * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = transform.up * MaxSpeed;

            Fuel -= FuelConsuption * Time.deltaTime;
        }

        if (Input.GetAxis("ThrustUpDown") < -0.1f)
        {
            _rigidbody.velocity += -transform.up * (AccelerationAmmount * Time.deltaTime);

            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = -transform.up * MaxSpeed;

            Fuel -= FuelConsuption * Time.deltaTime;
        }



        if (Input.GetButtonDown("Break"))
        {
            Brake();
        }


        if (Input.GetAxis("Mouse X") > 0.1f)
        {
             _rigidbody.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse X") < -0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse Y") > 0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.left * RotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse Y") < -0.1f)
        {
            _rigidbody.AddRelativeTorque(Vector3.left * RotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire"))
        {
            Weapon.Fire();
        }

        if (Input.GetButtonUp("Fire"))
        {
            Weapon.ReleaseFire();
        }

        if (Input.GetButtonDown("Debug"))
        {
            FreeLookOn = false;
            Warp(5f);
        }
    }

    public void Resupply()
    {
        _destructable.HP = _destructable.MaxHP;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Credits");
    }
}
