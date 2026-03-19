using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{

    public float ArmTimeOut = 1f;

    public void Update()
    {
        if (ArmTimeOut < 0f)
            return;

        ArmTimeOut -= Time.deltaTime;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (ArmTimeOut > 0f)
            return;

        if (other.gameObject.tag == "Car")
        {
            other.gameObject.SendMessage("OnProjectileHit");
        }

        Destroy(gameObject);
    }
}
