using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

public class BridgeSpawner : MonoBehaviour
{

    public GameObject BridgeLeft;
    public GameObject BridgeRight;
    public GameObject BridgeMain;
    public int MaxBridgeLength = 20;

    private SpriteRenderer _sprite;

    enum BridgeDirection
    {
        Left,
        Right,
        Invalid
    }


    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    BridgeDirection GetBridgeDirection()
    {   
        var height = _sprite.bounds.size.y;
        var width = _sprite.bounds.size.x;
        var position = new Vector2(transform.position.x, transform.position.y);

        var originScan = Physics2D.RaycastAll(position, Vector2.up*-1, height);

        if(!originScan.Any(h => h.transform.tag.Contains("Ground")))
            return BridgeDirection.Invalid;

        var rightScan = Physics2D.RaycastAll(new Vector2(position.x + width, position.y), Vector2.up*-1, height);
        var leftScan = Physics2D.RaycastAll(new Vector2(position.x - width, position.y), Vector2.up * -1, height);

        if(!rightScan.Any(h => h.transform.tag.Contains("Ground")))
            return BridgeDirection.Right;

        if(!leftScan.Any(h => h.transform.tag.Contains("Ground")))
            return BridgeDirection.Left;

        return BridgeDirection.Invalid;
    }

    int GetBridgeLength(BridgeDirection direction)
    {
        var returnData = 1;
        var height = _sprite.bounds.size.y;
        var width = _sprite.bounds.size.x;
        var position = new Vector2(transform.position.x, transform.position.y);
        var startUsed = false;


        if (direction == BridgeDirection.Invalid)
            return 0;

        while (true)
        {
            var hit = Physics2D.RaycastAll(position, Vector2.up*-1, height);

            if (hit.Any(h => h.transform.tag.Contains("Ground")))
            {
                if (!startUsed)
                {
                    startUsed = true;
                }
                else
                {
                    break;
                }
            }

            returnData++;

            position = direction == BridgeDirection.Right ? new Vector2(position.x + width, position.y) : new Vector2(position.x - width, position.y);
        }

        return returnData;
    }

    void BuildItem()
    {
        
        var position = transform.position;
        var width = _sprite.bounds.size.x;
        var bridgedirection = GetBridgeDirection();
        var bridgeLength = GetBridgeLength(bridgedirection);

        if (bridgedirection == BridgeDirection.Invalid)
        {
            Destroy(gameObject);
            return;
        }

        for (var p = 0; p < bridgeLength; p++)
        {
            if (p == 0)
            {
                if (bridgedirection == BridgeDirection.Right)
                    Instantiate(BridgeLeft, position, transform.rotation);
                else
                    Instantiate(BridgeRight, position, transform.rotation);
            }
            else if (p == bridgeLength - 1)
            {
                if (bridgedirection == BridgeDirection.Right)
                    Instantiate(BridgeRight, position, transform.rotation);
                else
                    Instantiate(BridgeLeft, position, transform.rotation);
            }
            else
            {
                Instantiate(BridgeMain, position, transform.rotation);
            }

            position = bridgedirection == BridgeDirection.Right ? new Vector3(position.x + width, position.y) : new Vector3(position.x - width, position.y);
        }

        Destroy(gameObject);
    }
}
