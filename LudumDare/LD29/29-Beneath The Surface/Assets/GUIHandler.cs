using System;
using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour
{

    public GameObject Player;
    public Texture2D HPBarEmpty;
    public Texture2D HPBarFull;
    public GUISkin GameOverSkin;
    public GUISkin NormalGUISkin;


    private PlayerHealth _healthController;
    private PlayerMovment _moveController;
    private bool GameOverMode = false;

    void Start() 
    {
        _healthController = Player.GetComponent<PlayerHealth>();
        _moveController = Player.GetComponent<PlayerMovment>();
    }

    void OnGUI()
    {
        var healthNorm = _healthController.Health/_healthController.MaxHealth;
        var money = _healthController.Money;
        var distance = Math.Round(_moveController.DistanceTraveled / 5, 2);

        if (healthNorm < 0f)
            healthNorm = 0f;

        if (healthNorm == 0f)
        {
            GameOverMode = true;
            var score = distance*money;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), string.Format("GAME OVER!\n SCORE: {0}", score), GameOverSkin.GetStyle("label"));
        }
        else
        {
            GUI.BeginGroup(new Rect(0, 0, 256, 32));
                GUI.Box(new Rect(0, 0, 256, 32), HPBarEmpty);

                GUI.BeginGroup(new Rect(0, 0, 256f*healthNorm, 32));
                    GUI.Box(new Rect(0, 0, 256f, 32), HPBarFull);
                GUI.EndGroup();
            GUI.EndGroup();

            GUI.Label(new Rect(Screen.width - 200, 0, 200, 40), string.Format("{0} KM - ${1}", distance, money), NormalGUISkin.GetStyle("label"));
            
        }
    }

    void Update()
    {
        if (GameOverMode)
        {
            if (Input.GetButton("Bomb"))
            {
                Application.LoadLevel("MainGame");
            }
        }
    }
}
