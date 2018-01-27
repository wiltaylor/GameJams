using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public float KillOnY = -100f;

    public void Kill()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if(transform.position.y <= KillOnY)
            Kill();
    }
	
}
