using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public float KillOnY = -100f;
    private List<SpawnController> _spawns = new List<SpawnController>();

    void Start()
    {
        if (tag != "Player")
            return;

        _spawns.AddRange(FindObjectsOfType<SpawnController>());
    }

    public void Kill()
    {
        if (tag != "Player")
            Destroy(gameObject);
        else
        {
            var spawn = _spawns.FirstOrDefault(s => s.ActiveSpawn) ?? _spawns.FirstOrDefault();

            if(spawn == null)
                Debug.LogError("NO RESPAWN POINTS IN LEVEL. CAN'T RESPAWN PLAYER");

            transform.position = spawn.transform.position;
            transform.parent = null;
        }
    }

    void Update()
    {
        if(transform.position.y <= KillOnY)
            Kill();
    }
	
}
