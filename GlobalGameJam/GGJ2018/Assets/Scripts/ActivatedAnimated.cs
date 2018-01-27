using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedAnimated : MonoBehaviour {
    /// <Tip>
    ///  This is used to set the max emission level of the lighted parts of the on Texture.
    /// </Tip>
    [SerializeField, Range(0.1f, 2f)]
    float m_EmissionMax = 0.1f;
    [SerializeField]
    public Color m_OnColour;
    [SerializeField]
    public Color m_OffColour;
    [SerializeField]
    MeshRenderer[] renderers;
    [SerializeField, Range(0.2f, 4f)]
    float m_TrasissionTime = 2;
    // state of the object
    public enum ACTIVATIONSTATE
    {
        OFF,
        ON,
    }

    float m_TimeSinceLastPass;


    public ACTIVATIONSTATE state = ACTIVATIONSTATE.OFF;
    // Use this for initialization
    void Start() {
        m_TimeSinceLastPass = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    { 

    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleOnOff();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleOnOff();
        }
    
    }
    */
    public void ToggleOnOff()
    {
        Color finalColour = SetColor();

        foreach (MeshRenderer mr in renderers)
        {
            mr.material.SetColor("_EmissionColor", finalColour);
        }
        if (state == ACTIVATIONSTATE.OFF)
        {
            state = ACTIVATIONSTATE.ON;
        }
        else if (state == ACTIVATIONSTATE.ON)
        {
            state = ACTIVATIONSTATE.OFF;
        }
    }

    private Color SetColor()
    {
        float emissionPoint = 0.1f;
        Color colour = m_OffColour;


        if (state == ACTIVATIONSTATE.OFF)
        {
            emissionPoint = m_EmissionMax;
            colour = m_OnColour;
        }
        else if (state == ACTIVATIONSTATE.ON)
        {
            emissionPoint = 0.1f;
            colour = m_OffColour;
        }

        float emission = Mathf.PingPong(Time.time, emissionPoint);

        return colour * Mathf.LinearToGammaSpace(emission);
    }
}
