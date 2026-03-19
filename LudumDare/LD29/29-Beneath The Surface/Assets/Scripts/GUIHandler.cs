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
    public AudioClip GameOverSound;
    
    private PlayerHealth _healthController;
    private PlayerMovment _moveController;
    private AudioSource _music;
    private bool GameOverMode = false;

    void Start() 
    {
        _healthController = Player.GetComponent<PlayerHealth>();
        _moveController = Player.GetComponent<PlayerMovment>();
        _music = GetComponent<AudioSource>();

        if(GlobalGameSettings.Instance.MusicEnabled)
            _music.Play();
        
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
            if (!GameOverMode)
            {
                _music.Stop();
                AudioSource.PlayClipAtPoint(GameOverSound, _healthController.transform.position);               
            }

            GameOverMode = true;
            var score = (int)distance*money;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), string.Format("GAME OVER!\n\nResource Score: {0}\nDistance Traveled: {1}KM\nTotal Score: {2}\nPress Space To Restart", money, distance, score), GameOverSkin.GetStyle("label"));
        }
        else
        {
            GUI.BeginGroup(new Rect(10, 10, 256, 32));
                GUI.DrawTexture(new Rect(0, 0, 256, 32), HPBarEmpty);

                GUI.BeginGroup(new Rect(0, 0, 256f*healthNorm, 32));
                    GUI.DrawTexture(new Rect(0, 0, 256f, 32), HPBarFull);
                GUI.EndGroup();
            GUI.EndGroup();

            GUI.Label(new Rect(Screen.width - 100, 0, 200, 40), string.Format("{0} KM", distance), NormalGUISkin.GetStyle("label"));
            GUI.Label(new Rect(Screen.width / 2 - 20, 0, 200, 40), string.Format("Score: {0}", money), NormalGUISkin.GetStyle("label"));
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
