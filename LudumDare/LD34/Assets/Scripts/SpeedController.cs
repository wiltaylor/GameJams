using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour {

    public GameObject GameOverScreen;

    private Rigidbody _target;

    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (GlobalController.Instance.CurrentPlayer == null)
            return;

        if(_target == null)
            _target = GlobalController.Instance.CurrentPlayer.GetComponent<Rigidbody>();

        if (GlobalController.Instance.CurrentPlayer.PlayerIsDead)
        {
            GameOverScreen.SetActive(true);
        }

        text.text = Mathf.Round(_target.velocity.sqrMagnitude).ToString();
    }
}
