using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceMan : MonoBehaviour
{

    private readonly List<CarRankTracker> _cars = new List<CarRankTracker>();
    private readonly List<CarRankTracker> _finshedcars = new List<CarRankTracker>();

    public string TrackName;
    public GameObject PlayerPrefab;
    public GameObject AIPrefab;
    public GameObject FirstNode;

    public float TimeOutBeforeCalc = 3f;
    
    void Start ()
    {
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
}
