using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    private List<SpawnController> _spawns = new List<SpawnController>();

    public bool ActiveSpawn;
    public GameObject Bling;
    public float BlingTimeOut = 3f;

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

    public void FireBling()
    {
        Bling.SetActive(true);

        Invoke("DisableBling", BlingTimeOut);

    }

    public void DisableBling()
    {
        Bling.SetActive(false);
    }


}
