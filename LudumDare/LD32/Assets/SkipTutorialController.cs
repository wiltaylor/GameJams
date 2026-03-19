using UnityEngine;
using System.Collections;

public class SkipTutorialController : MonoBehaviour {

    public void SkipTutorial()
    {
       GameController.Instance.SkipTutorial();
    }
}
