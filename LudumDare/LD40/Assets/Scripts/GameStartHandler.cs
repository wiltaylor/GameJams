using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartHandler : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetButton("Jump"))
        {
            GameManger.Instance.ProgressLevel("Level1", false);
        }
    }
}
