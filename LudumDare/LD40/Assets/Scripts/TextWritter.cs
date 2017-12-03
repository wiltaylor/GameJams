using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TextWritter : MonoBehaviour
{
    public string[] Text;
    public TextMessages TextMessages;

    public void Activate()
    {
        foreach(var t in Text)
            TextMessages.AddMessage(t);
    }

}
