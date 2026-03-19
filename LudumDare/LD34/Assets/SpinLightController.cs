using UnityEngine;
using System.Collections;

public class SpinLightController : MonoBehaviour {

    public float Speed = 1f;

	void Update ()
    {
        transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0));
	}
}
