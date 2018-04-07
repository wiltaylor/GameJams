using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{

    public Slider Speed;
    public Text Credits;
    public Slider Fuel;
    public Slider Cargo;
    public Slider HP;
    public Slider Shield;
    public Image TargetReticle;
    public PlayerController Player;
    public GameObject OnScreenIndicatorPrefab;
    public GameObject OffScreenIndicatorPrefab;
    public GameObject ComsButton;
    public GameObject FreeControlButton;
    public GameObject CargoButton;

    //Windows
    public GameObject SpaceStationWindow;
    public GameObject FuelWindow;
    public GameObject ShipUpgradeWindow;
    public GameObject ShopWindow;
    public GameObject CargoWindow;


    private Camera _camera = null;
    private Destructable _playerDestructable;

    private readonly List<TrackableObject> _trackableObjects = new List<TrackableObject>();

    void Start()
    {
        _camera = Player.GetComponentsInChildren<Camera>().First(c => c.tag == "MainCamera" );
        _playerDestructable = Player.GetComponent<Destructable>();
    }

    public void EngageFreeControl()
    {
        Player.FreeLookOn = true;
    }

    public void AddTrackerObject(GameObject obj, Color colour, string text)
    {
        var onscreenTracker = Instantiate(OnScreenIndicatorPrefab);
        onscreenTracker.transform.SetParent(transform);
        onscreenTracker.GetComponent<Image>().color = colour;

        var onscreentext = onscreenTracker.GetComponentInChildren<Text>();
        onscreentext.text = text;
        onscreentext.color = colour;

        var offscreenTracker = Instantiate(OffScreenIndicatorPrefab);
        offscreenTracker.transform.SetParent(transform);
        offscreenTracker.GetComponent<Image>().color = colour;

        var newTrackObj = new TrackableObject
        {
            Object = obj,
            OnScreenTracker = onscreenTracker,
            OffScreenTracker = offscreenTracker
        };

        _trackableObjects.Add(newTrackObj);
    }

    public void RemoveTrackerObject(GameObject obj)
    {
        _trackableObjects.RemoveAll(o => o.Object == obj);
    }
	
	void Update ()
	{
	    if (Player.FreeLookOn)
	    {
	        HideAllWindows();
        }

	    Speed.maxValue = Player.MaxSpeed;
	    Speed.minValue = 0;
	    Speed.value = Player.CurrentSpeed;

	    Fuel.maxValue = Player.MaxFuel;
	    Fuel.minValue = 0;
	    Fuel.value = Player.Fuel;

	    Cargo.maxValue = Player.CargoCapacity;
	    Cargo.minValue = 0;
	    Cargo.value = Player.FreeCargoCapacity;

	    HP.maxValue = _playerDestructable.MaxHP;
	    HP.minValue = 0;
	    HP.value = _playerDestructable.HP;

	    Shield.maxValue = _playerDestructable.MaxShield;
	    Shield.minValue = 0;
	    Shield.value = _playerDestructable.Shield;

        Credits.text = "$" + Player.Credits;
        TargetReticle.enabled = Player.FreeLookOn;
        
	    ComsButton.SetActive(Player.SpaceStationInRange);
	    FreeControlButton.SetActive(!Player.FreeLookOn);

        if (_camera == null)
            return;

	    if (!Player.isWarping)
	        RenderTrackers();
	    else
	        HideAllTrackers();

	}

    private void HideAllTrackers()
    {
        foreach (var o in _trackableObjects)
        {
            o.OffScreenTracker.SetActive(false);
            o.OnScreenTracker.SetActive(false);
        }
    }

    private void HideAllWindows()
    {
        SpaceStationWindow.SetActive(false);
        FuelWindow.SetActive(false);
        ShipUpgradeWindow.SetActive(false);
        ShopWindow.SetActive(false);
        CargoWindow.SetActive(false);
    }


    private void RenderTrackers()
    {
        foreach (var o in _trackableObjects.Where(o => o.Object == null))
        {
            Destroy(o.OffScreenTracker);
            Destroy(o.OnScreenTracker);
        }

        _trackableObjects.RemoveAll(o => o.Object == null);

        foreach (var obj in _trackableObjects)
        {
            var screenPoint = _camera.WorldToScreenPoint(obj.Object.transform.position);

            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.y > 0 && screenPoint.x < Screen.width &&
                screenPoint.y < Screen.height)
            {
                obj.OffScreenTracker.SetActive(false);
                obj.OnScreenTracker.SetActive(true);
                var rect = obj.OnScreenTracker.GetComponent<RectTransform>();
                rect.transform.position = new Vector3(Mathf.Round(screenPoint.x), Mathf.Round(screenPoint.y), 0); //round to fix jittering

                var destruct = obj.Object.GetComponent<Destructable>();
                var control = obj.OnScreenTracker.GetComponent<OnScreenTracker>();

                if (destruct != null)
                {
                    if (destruct.Shield > 0f)
                    {
                        control.Shield.gameObject.SetActive(true);
                        control.HP.gameObject.SetActive(false);
                        control.Shield.maxValue = destruct.MaxShield;
                        control.Shield.minValue = 0f;
                        control.Shield.value = destruct.Shield;
                    }
                    else
                    {
                        control.Shield.gameObject.SetActive(false);
                        control.HP.gameObject.SetActive(true);
                        control.HP.maxValue = destruct.MaxHP;
                        control.HP.minValue = 0f;
                        control.HP.value = destruct.HP;
                    }
                }
            }
            else //Offscreen
            {
                obj.OffScreenTracker.SetActive(true);
                obj.OnScreenTracker.SetActive(false);

                //To see how this code works see https://youtu.be/gAQpR1GN0Os

                if (screenPoint.z < 0)
                    screenPoint *= -1;

                var screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

                screenPoint -= screenCenter;

                var angle = Mathf.Atan2(screenPoint.y, screenPoint.x);
                angle -= 90 * Mathf.Deg2Rad;

                var cos = Mathf.Cos(angle);
                var sin = -Mathf.Sin(angle);

                screenPoint = screenCenter + new Vector3(sin * 150, cos * 150, 0);

                var m = cos / sin;

                var screenBounds = screenCenter * 0.95f;

                screenPoint = cos > 0
                    ? new Vector3(screenBounds.y / m, screenBounds.y, 0)
                    : new Vector3(-screenBounds.y / m, -screenBounds.y, 0);

                if (screenPoint.x > screenBounds.x)
                    screenPoint = new Vector3(screenBounds.x, screenBounds.x * m, 0);
                else if (screenPoint.x < -screenBounds.x)
                    screenPoint = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);

                screenPoint += screenCenter;

                var rect = obj.OffScreenTracker.GetComponent<RectTransform>();
                rect.transform.position = screenPoint;
                rect.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            }
        }
    }
}
