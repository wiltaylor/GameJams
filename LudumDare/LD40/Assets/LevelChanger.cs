using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public void ChangeLevel(string level)
    {
        GameManger.Instance.ProgressLevel(level, true);
    }
}
