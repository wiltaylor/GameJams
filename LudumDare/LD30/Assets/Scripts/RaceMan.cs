using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceMan : MonoBehaviour
{

    private readonly List<CarRankTracker> _cars = new List<CarRankTracker>();
    private readonly List<CarRankTracker> _finshedcars = new List<CarRankTracker>();
    private int _countDown = 3;
    private float _currentTimer = 1f;
    private readonly GUIStyle _readyStyle = new GUIStyle();
    private bool _carsActive = false;
    private bool _startNumbers = true;

    public string TrackName;
    public GameObject PlayerPrefab;
    public GameObject AIPrefab;
    public GameObject FirstNode;
    public float ReadyTimeOut = 2f;
    public AudioClip RaceBeep;
    public AudioClip GoBeep;
    
    public float TimeOutBeforeCalc = 3f;
    
    void Start ()
    {
        _readyStyle.alignment = TextAnchor.MiddleCenter;
        _readyStyle.normal.textColor = Color.red;
        _readyStyle.fontSize = 32;

        var startPoses = GameObject.FindGameObjectsWithTag("StartPosition");

        foreach (var pos in startPoses)
        {
            var info = pos.GetComponent<StartPositionMan>();

            if (info.Position == GameMan.Instance.LastPlayerRank)
            {
                var car = (GameObject)Instantiate(PlayerPrefab, pos.transform.position, pos.transform.rotation);
                _cars.Add(car.GetComponent<CarRankTracker>());
                car.GetComponent<CarController>().NavNode = FirstNode;
            }
            else
            {
                var car = (GameObject)Instantiate(AIPrefab, pos.transform.position, pos.transform.rotation);
                _cars.Add(car.GetComponent<CarRankTracker>());
                car.GetComponent<CarAI>().NavNode = FirstNode;
            }
        }

        GameMan.Instance.LastRace = TrackName;

    }
	
	void Update ()
	{

	    if (!_carsActive)
	    {
	        if (ReadyTimeOut > 0f)
	            ReadyTimeOut -= Time.deltaTime;
	        else
	        {
                if (_startNumbers)
                {
                    _startNumbers = false;
                    AudioSource.PlayClipAtPoint(RaceBeep, Vector3.zero);
                }

	            if (_currentTimer > 0)
	                _currentTimer -= Time.deltaTime;
	            else
	            {
	                _currentTimer = 1f;
	                _countDown--;
	                AudioSource.PlayClipAtPoint(_countDown > 0 ? RaceBeep : GoBeep, Vector3.zero);
	            }

	            if (_countDown < 1 && !_carsActive)
	            {
	                _carsActive = true;

	                foreach (var c in _cars)
	                {
	                    var ai = c.GetComponent<CarAI>();

	                    if (ai != null)
	                    {
	                        ai.enabled = true;
	                    }
	                    else
	                    {
	                        var player = c.GetComponent<CarController>();
	                        player.enabled = true;
	                    }
	                }

	            }
	        }
	    }



	    if (TimeOutBeforeCalc > 0f)
	    {
	        TimeOutBeforeCalc -= Time.deltaTime;
	        return;
	    }

        var ranking = _cars.OrderBy(c => c.Lap).ThenBy(c => c.NodeID).ThenByDescending(c => c.NodeDistance).ToArray();

	    var rank = _cars.Count;

	    foreach (var r in ranking)
	    {
	        r.Rank = rank;
	        rank--;
	    }
	}

    public void CarFinishedRace(CarRankTracker car)
    {
        _finshedcars.Add(car);

        if (car.IsPlayer)
        {
            GameMan.Instance.LastPlayerRank = _finshedcars.Count;
            GameMan.Instance.EndRace();

        }
    }

    public void OnGUI()
    {
        if (_carsActive)
            return;

        if (ReadyTimeOut > 0f)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "GET READY...", _readyStyle);
            return;
        }

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), _countDown.ToString(), _readyStyle);
    }
}
