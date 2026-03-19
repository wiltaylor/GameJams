using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthController : MonoBehaviour {

    public Slider HPSlider;
	
	void Update ()
    {
        if(GlobalController.Instance.CurrentPlayer != null)
        {
            HPSlider.maxValue = GlobalController.Instance.CurrentPlayer.MaxHP;
            HPSlider.value = GlobalController.Instance.CurrentPlayer.HP;
        }
        
    }
}
