using UnityEngine;
using System.Collections;
using System.Linq;

public class LadderSpawner : MonoBehaviour
{
    public GameObject LadderPrefab;
    public GameObject LadderEndPrefab;
    public int MaxLadderHeight = 20;

    private SpriteRenderer _sprite;
    

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void BuildItem()
    {
        var ladderLength = CalculateLadderLength();
        var position = transform.position;
        var height = _sprite.bounds.size.y;

        for (var p = 0; p < ladderLength; p++)
        {
            if (p == ladderLength - 1 || p == 0)
            {
                Instantiate(LadderEndPrefab, position, transform.rotation);
            }
            else
            {
                Instantiate(LadderPrefab, position, transform.rotation);
            }

            position = new Vector3(position.x, position.y + height, position.z);
        }

        Destroy(gameObject);
    }

    int CalculateLadderLength()
    {
        var returnData = 1;
        var position = new Vector2(transform.position.x, transform.position.y);
        var height = _sprite.bounds.size.y;
        var width = _sprite.bounds.size.x;

  
        while (true)
        {
            if (returnData >= MaxLadderHeight)
                return MaxLadderHeight;

            var upScan = Physics2D.RaycastAll(position, Vector2.up, height);

            if (upScan.Any(i => i.transform.tag.Contains("Ground")))
                break;

            position = new Vector2(position.x, position.y + height);

            var leftScan = Physics2D.RaycastAll(position, Vector2.right*-1, width * 2);
            var rightScan = Physics2D.RaycastAll(position, Vector2.right, width * 2);

            if (leftScan.Any(i => i.transform.tag.Contains("Ground")))
            {
                returnData++;
                continue;
            }

            if (rightScan.Any(i => i.transform.tag.Contains("Ground")))
            {
                returnData++;
                continue;
            }
            
            break;
        }

        return returnData;
    }

}
