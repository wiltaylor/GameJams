using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoBayItem : MonoBehaviour
{
    public Image Image;
    public Text Name;
    public Text Qty;

    public void Setup(Sprite image, string name, int qty)
    {
        Image.sprite = image;
        Name.text = name;
        Qty.text = qty.ToString();
    }


}
