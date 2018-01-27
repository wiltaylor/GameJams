using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedAnimated : MonoBehaviour {
    /// <Tip>
    ///  This is used to set the max emission level of the lighted parts of the on Texture.
    /// </Tip>
    [SerializeField, Range (0.1f,2f)]
    public float EmissionMax = 0.1f;
    [SerializeField]
    public Color m_OnColour;
    [SerializeField]
    public Color m_OffColour;

    MeshRenderer[] m_Renderers;
    [SerializeField, Range(0.2f, 4f)]
    float m_TrasissionTime = 2;
    // state of the object
    public enum ACTIVATIONSTATE
    {
        OFF,
        ON,
        TURING_ON,
        TURING_OFF,

    }

    float m_TimeSinceLastPass;


    public ACTIVATIONSTATE state = ACTIVATIONSTATE.OFF;
    // Use this for initialization
    void Start () {
        m_TimeSinceLastPass = Time.timeSinceLevelLoad;	
	}
	
	// Update is called once per frame
	void Update () {
        if(state == ACTIVATIONSTATE.TURING_ON || state == ACTIVATIONSTATE.TURING_OFF) ToggleOnOff();

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ActivatedBox")
        {
            ActivatedAnimated _state = col.gameObject.GetComponent<ActivatedAnimated>();
            if (_state.state == ACTIVATIONSTATE.OFF) _state.state = ACTIVATIONSTATE.TURING_ON;
        }
    }
    private void OnTiggerExit(Collider col)
    {
        if (col.gameObject.tag == "ActivatedBox")
        {
            ActivatedAnimated _state = col.gameObject.GetComponent<ActivatedAnimated>();
           if (_state.state == ACTIVATIONSTATE.OFF) _state.state = ACTIVATIONSTATE.TURING_ON;
        }
    }



    public void ToggleOnOff()
    {
        if (m_Renderers == null)
        {
            m_Renderers = GetComponentsInChildren<MeshRenderer>();
        }


        Color finalColour = SetColor();

        foreach (MeshRenderer mr in m_Renderers)
        {
            if (mr.gameObject.tag != "Top" || mr.gameObject.tag != "Bottom") mr.material.SetColor("_EmissionColor", finalColour);
        }
        if (state == ACTIVATIONSTATE.TURING_ON)
        {
            state = ACTIVATIONSTATE.ON;
        }
        else if (state == ACTIVATIONSTATE.TURING_OFF)
        {
            state = ACTIVATIONSTATE.OFF;
        }
    }

    private Color SetColor()
    {
        float emissionPoint = 0.1f;
        Color colour = m_OffColour;


        if (state == ACTIVATIONSTATE.TURING_ON)
        {
            emissionPoint = EmissionMax;
            colour = m_OnColour;
        }
        else if (state == ACTIVATIONSTATE.TURING_OFF)
        {
            emissionPoint = 0.1f;
            colour = m_OffColour;
        }

        float emission = Mathf.PingPong(Time.time, emissionPoint);

        return colour * Mathf.LinearToGammaSpace(emission);
    }
}
