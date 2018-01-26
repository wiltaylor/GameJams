using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedAnimated : MonoBehaviour {
    [SerializeField]
    Material m_OffMaterial;
    [SerializeField]
    Material m_OnMaterial;
    /// <Tip>
    ///  This is used to set the max emission level of the lighted parts of the on Texture.
    /// </Tip>
    [SerializeField, Range (0f,1f)]
    float m_EmissionMax = 0.7f;

    [SerializeField]
    MeshRenderer[] renderers;

    // state of the object
    enum ACTIVATIONSTATE
    {
        OFF,
        TURNING_ON,
        ON,
        TURNING_OFF,
    }

    float m_TimeSinceLastPass;
    float m_TrasissionTime = 2;

    ACTIVATIONSTATE state = ACTIVATIONSTATE.OFF;
    // Use this for initialization
    void Start () {
        m_TimeSinceLastPass = Time.timeSinceLevelLoad;	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_TimeSinceLastPass + m_TrasissionTime < Time.time)
        {
            ToggleOnOff();
            m_TimeSinceLastPass = Time.timeSinceLevelLoad;

        }
    }

    private void ToggleOnOff()
    {
        Material material = null;
        if (state == ACTIVATIONSTATE.OFF)
        {
            material = m_OnMaterial;
            state = ACTIVATIONSTATE.ON;
        }
        else if (state == ACTIVATIONSTATE.ON)
        {
            material = m_OffMaterial;
            state = ACTIVATIONSTATE.OFF;
        }


        foreach (MeshRenderer mr in renderers)
        {

                if (material != null)
                    mr.materials[0] = new Material(material);
                else
                    Debug.LogWarning("No Material assisgned");
        }
    }
}
