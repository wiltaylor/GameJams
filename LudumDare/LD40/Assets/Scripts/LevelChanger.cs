using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public float TimeOut = 0f;
    private bool _runningTimeout = false;

    private string targetLevel;

    public void ChangeLevel(string level)
    {
        targetLevel = level;
        _runningTimeout = true;
    }

    void Update()
    {
        if (_runningTimeout)
        {
            if(TimeOut <= 0f)
                GameManger.Instance.ProgressLevel(targetLevel, true);

            TimeOut -= Time.deltaTime;
        }
    }
}
