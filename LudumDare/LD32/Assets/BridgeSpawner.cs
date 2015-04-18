using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class BridgeSpawner : MonoBehaviour
{

    public GameObject BridgeLeft;
    public GameObject BridgeRight;
    public GameObject BridgeMain;
    public int MaxBridgeLength = 20;

    private SpriteRenderer _sprite;


    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void BuildItem()
    {
        var bridgeLength = 11;
        var position = transform.position;
        var width = _sprite.bounds.size.x;
        var buildingRight = true;

        for (var p = 0; p < bridgeLength; p++)
        {
            if (p == 0)
            {
                if (buildingRight)
                    Instantiate(BridgeLeft, position, transform.rotation);
                else
                    Instantiate(BridgeRight, position, transform.rotation);
            }
            else if (p == bridgeLength - 1)
            {
                if (buildingRight)
                    Instantiate(BridgeRight, position, transform.rotation);
                else
                    Instantiate(BridgeLeft, position, transform.rotation);
            }
            else
            {
                Instantiate(BridgeMain, position, transform.rotation);
            }

            position = buildingRight ? new Vector3(position.x + width, position.y) : new Vector3(position.x - width, position.y);
        }

        Destroy(gameObject);
    }
}
