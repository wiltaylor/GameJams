using UnityEngine;
using System.Collections;

public class EntryRoomController : MonoBehaviour
{
    public GameObject HeroPrefab;
    public GameObject SpawnPoint;
    public float HeroCountDown = 100f;
    public float CurrentCountDown = 100f;

    void FixedUpdate()
    {
        CurrentCountDown -= Time.fixedDeltaTime;

        if (CurrentCountDown <= 0f)
        {
            CurrentCountDown = HeroCountDown;
            Instantiate(HeroPrefab, SpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
