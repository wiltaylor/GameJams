using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkController : MonoBehaviour {

    Text text;
    public float FadeTime = 1f;
    float _CurrentFade;
    bool _Faded = false;

	void Start ()
    {
        text = GetComponent<Text>();
        _CurrentFade = 0f;
    }
	
	void Update ()
    {
        _CurrentFade -= Time.deltaTime;

        if (_CurrentFade <= 0)
        {
            _Faded = !_Faded;

            _CurrentFade = FadeTime;

            if(_Faded)
            {
                text.CrossFadeAlpha(1f, FadeTime, false);
            }
            else
            {
                text.CrossFadeAlpha(0f, FadeTime, false);
            }
        }


	    
	}
}
