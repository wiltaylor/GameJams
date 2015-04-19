using UnityEngine;
using System.Collections;

public class RestartGameController : MonoBehaviour {

    public void RestartGame()
    {
        GameController.Instance.NextLevel();
    }
}
