using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public Text TextBox;
    public PlayerStats Stats;

		void Update ()
	{
	    TextBox.text = "Score: " + Stats.Score;
	}
}
