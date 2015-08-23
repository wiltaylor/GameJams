using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

    public int HP = 100;
    public int MaxHP = 100;
    public GameObject DeathPrefab;
    public int DesireOnKill = 0;
    public int EvilOnKill = 0;

    public void FixedUpdate()
    {
        if (HP <= 0)
        {
            if (DeathPrefab != null)
            {
                Instantiate(DeathPrefab, transform.position, transform.rotation);
            }

            GameManager.Instance.Desire += DesireOnKill;
            GameManager.Instance.Evil += EvilOnKill;

            Destroy(gameObject);
        }
    }

    public void TakeHit(int qty)
    {
        HP -= qty;
    }
}
