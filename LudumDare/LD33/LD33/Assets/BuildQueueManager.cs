using UnityEngine;
using System.Collections;

public class BuildQueueManager : MonoBehaviour
{
    public GameObject BuildQueue;
    public GameObject BuildQueueContent;
    public GameObject ScrollBar;
    public float Padding = 1f;
    public float SpacePerItem = 50f;
    public float UpdateFrequency = 5f;

    private float _updateCounter = 5f;

    public void Enqueue(GameObject item)
    {
        BuildQueue.SetActive(true);

        var obj = (GameObject)Instantiate(item);

        obj.transform.SetParent(BuildQueueContent.transform, false);

        ReorderQueue();
    }

    public void Start()
    {
        ReorderQueue();
    }

    public void FixedUpdate()
    {
        if (_updateCounter <= 0f)
        {
            _updateCounter = UpdateFrequency;
            ReorderQueue();
        }
        else
        {
            _updateCounter -= Time.fixedDeltaTime;
        }
    }

    public void ReorderQueue()
    {
        if (BuildQueueContent.transform.childCount == 0)
        {
            BuildQueue.SetActive(false);
            return;
        }

        BuildQueue.SetActive(true);

        var bqrect = BuildQueueContent.GetComponent<RectTransform>();
        var bqhieght = BuildQueueContent.transform.childCount*SpacePerItem;

        if (bqhieght < 200)
            bqhieght = 200;

        ScrollBar.SetActive(bqhieght > 200f);

        bqrect.sizeDelta = new Vector2(bqrect.sizeDelta.x, bqhieght);
        
        var offset = (bqrect.sizeDelta.y/2);

        for (var i = 0; i < BuildQueueContent.transform.childCount; i++)
        {
            var obj = BuildQueueContent.transform.GetChild(i).gameObject;
            var rect = obj.GetComponent<RectTransform>();
            var currentoffset = offset - (rect.sizeDelta.y/2) - ((rect.sizeDelta.y + Padding) * i);
            Debug.Log("Offset:" + currentoffset);
            rect.anchoredPosition = new Vector2(-1 * rect.sizeDelta.x, currentoffset);
        }
    }
}
