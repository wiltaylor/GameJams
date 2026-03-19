using UnityEngine;
using System.Collections;

public class SpeedBoostItem : MonoBehaviour
{

    public float SpeedAdded = 2f;
    public float TimeOut = 5f;
    private bool _active = false;
    private float _timeleft = 0f;
    private GameObject _user;

    public void UseItem(GameObject user)
    {
        _user = user;
        _user.SendMessage("ModEnginePower", SpeedAdded);
        _active = true;
        _timeleft = TimeOut;
    }

    public void Update()
    {
        if (!_active)
            return;

        _timeleft -= Time.deltaTime;

        if (!(_timeleft < 0f)) return;

        _user.SendMessage("ModEnginePower", -SpeedAdded);
        Destroy(gameObject);
    }
}
