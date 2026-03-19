using UnityEngine;
using System.Collections;

public class ParticleSystemLayerFix : MonoBehaviour
{
    public string SortingLayer = "";

	// Use this for initialization
	void Start ()
	{
	    var part = GetComponent<Renderer>();
	    part.sortingLayerName = "Unit";
	}
	
}
