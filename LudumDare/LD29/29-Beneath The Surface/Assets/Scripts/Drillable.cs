using UnityEngine;
using System.Collections;

public class Drillable : MonoBehaviour {

    public float Health = 10f;
    public bool Invulnerable = false;
    public int MoneyOnDestruct = 0;
    public float LifeGainOnDestruct = 0;
    public bool IsBomb = false;
    public float BombCountDown = 1f;

    private bool _detenate = false;
    private GameObject _bomb;

    public void Start()
    {
        if (IsBomb)
            _bomb = transform.FindChild("Bomb").gameObject;
    }

    public void OnBomb()
    {
        if (!Invulnerable)
            Destroy(gameObject);
    }

    public void Update()
    {
        if (!_detenate)
            return;

        if (BombCountDown < 0f)
            Destroy(gameObject);
        else
            BombCountDown -= Time.deltaTime;
    }

    public void OnDrill(GameObject player)
    {
        if (Invulnerable)
            return;

        Health -= 1f;

        if (Health < 0f)
        {
            if(MoneyOnDestruct != 0)
                player.SendMessage("PickUpCash", MoneyOnDestruct);

            if(LifeGainOnDestruct != 0)
                player.SendMessage("PickUpHealth", LifeGainOnDestruct);

            if (IsBomb)
            {
                _bomb.SetActive(true);
                _detenate = true;
                return;
            }

            Destroy(gameObject);
        }
    }
}
