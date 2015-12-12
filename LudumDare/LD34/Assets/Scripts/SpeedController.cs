using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour {

    public Rigidbody Target;

    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        text.text = Mathf.Round(Target.velocity.magnitude).ToString();
	}
}
