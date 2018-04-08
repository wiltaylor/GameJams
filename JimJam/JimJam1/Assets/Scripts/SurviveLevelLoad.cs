using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurviveLevelLoad : MonoBehaviour
{

    private static List<Type> _types;

	void Start ()
    {
        if(_types == null)
            _types = new List<Type>();

        if (_types.Any(t => t == this.GetType()))
        {
            Destroy(gameObject);
            return;
        }

        _types.Add(this.GetType());
		DontDestroyOnLoad(gameObject);
	}
	

}
