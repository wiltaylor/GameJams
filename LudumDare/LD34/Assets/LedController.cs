using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LedController : MonoBehaviour {

    public Color OkColour;
    public Color FailColour;
    public bool Ok;

    public Text MessageText;
    public Image LedImage;

	void Update ()
    {
        if (Ok)
        {
            MessageText.color = OkColour;
            LedImage.color = OkColour;
        }
        else
        {
            MessageText.color = FailColour;
            LedImage.color = FailColour;

        }
    }
}
