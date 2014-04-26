using System;
using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour
{

    public GameObject Player;
    public Texture2D HPBarEmpty;
    public Texture2D HPBarFull;
    public GUISkin Skin;

    private PlayerHealth _healthController;

    void Start() 
    {
        _healthController = Player.GetComponent<PlayerHealth>();
    }

    void OnGUI()
    {
        var healthNorm = _healthController.Health/_healthController.MaxHealth;

        if (healthNorm < 0f)
            healthNorm = 0f;

        if (healthNorm == 0f)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "GAME OVER!", Skin.GetStyle("label"));
        }
        else
        {
            GUI.BeginGroup(new Rect(0, 0, 256, 32));
                GUI.Box(new Rect(0, 0, 256, 32), HPBarEmpty);

                GUI.BeginGroup(new Rect(0, 0, 256f*healthNorm, 32));
                    GUI.Box(new Rect(0, 0, 256f, 32), HPBarFull);
                GUI.EndGroup();
            GUI.EndGroup();
        }

    }
}
