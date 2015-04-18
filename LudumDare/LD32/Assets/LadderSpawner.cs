using UnityEngine;
using System.Collections;

public class LadderSpawner : MonoBehaviour
{
    public int LadderLength = 1;
    public GameObject LadderPrefab;
    public GameObject LadderEndPrefab;
    public float LadderBlockHeight = 0.32f;

    void BuildItem()
    {
        LadderLength = CalculateLadderLength();
        var position = transform.position;

        for (var p = 0; p < LadderLength; p++)
        {
            if (p == LadderLength - 1 || p == 0)
            {
                Instantiate(LadderEndPrefab, position, transform.rotation);
            }
            else
            {
                Instantiate(LadderPrefab, position, transform.rotation);
            }

            position = new Vector3(position.x, position.y + LadderBlockHeight, position.z);
        }

        Destroy(gameObject);
    }

    int CalculateLadderLength()
    {
        var returnData = 1;
        var position = new Vector2(transform.position.x, transform.position.y);
        var sprite = GetComponent<SpriteRenderer>();
        var height = sprite.bounds.size.y;
        var width = sprite.bounds.size.x;

  
        while (true)
        {
            var upScan = Physics2D.RaycastAll(position, Vector2.up, height);

            foreach (var i in upScan)
            {
                if (i.transform.tag.Contains("Ground"))
                    break;
            }

            position = new Vector2(position.x, position.y + height);

            var leftScan = Physics2D.RaycastAll(position, Vector2.right*-1, width * 2);
            var rightScan = Physics2D.RaycastAll(position, Vector2.right, width * 2);
            var hit = false;

            foreach (var i in leftScan)
            {
                if (i.transform.tag.Contains("Ground"))
                {
                    returnData++;
                    continue;
                }
            }

            foreach (var i in rightScan)
            {
                if (i.transform.tag.Contains("Ground"))
                {
                    returnData++;
                    continue;
                }
            }

            break;
        }

        return returnData;
    }

}
