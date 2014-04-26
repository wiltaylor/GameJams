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

        GUI.BeginGroup(new Rect(0, 0, 256, 32));
            GUI.Box(new Rect(0, 0, 256, 32), HPBarEmpty);

            GUI.BeginGroup(new Rect(0, 0, 256f * healthNorm, 32));
                GUI.Box(new Rect(0, 0, 256f, 32), HPBarFull);
            GUI.EndGroup();
        GUI.EndGroup();

    }
}
