using UnityEngine;
using System.Collections;

public class FinishLineMan : MonoBehaviour
{

    public int MaxLaps = 3;
    public GameObject NextNode;

    private RaceMan _raceMan;

    void Start()
    {
        _raceMan = GameObject.FindGameObjectWithTag("RaceManager").GetComponent<RaceMan>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Car")
            return;

        var rankInfo = other.GetComponent<CarRankTracker>();

        if(!rankInfo.HasPassedMidPoint)
            return;

        rankInfo.Lap++;
        rankInfo.HasPassedMidPoint = false;

        if (rankInfo.Lap == MaxLaps)
        {
            _raceMan.CarFinishedRace(rankInfo);
        }
    }
}
