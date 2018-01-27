using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    private List<SpawnController> _spawns = new List<SpawnController>();

    public bool ActiveSpawn;

	void Start ()
	{
	    _spawns.AddRange(FindObjectsOfType<SpawnController>());
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActiveSpawn = true;

            foreach(var spawn in _spawns)
                if (spawn != this)
                    spawn.ActiveSpawn = false;
        }
    }
}
