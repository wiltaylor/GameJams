using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopBarController : MonoBehaviour
{

    public Text GoldText;
    public Text GemText;
    public Text DesireText;
    public Text EvilText;
    public Slider LitchHPSlider;

    public void Update()
    {
        GoldText.text = GameManager.Instance.Gold.ToString();
        GemText.text = GameManager.Instance.Gems.ToString();
        DesireText.text = GameManager.Instance.Desire.ToString();
        EvilText.text = GameManager.Instance.Evil.ToString();

        if (LitchController.Instance != null)
        {
            LitchHPSlider.maxValue = LitchController.Instance.MaxHP;
            LitchHPSlider.minValue = 0;
            LitchHPSlider.value = LitchController.Instance.HP;
            
        }
    }
}
