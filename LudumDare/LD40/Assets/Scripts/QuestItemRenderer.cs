using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class QuestItemRenderer : MonoBehaviour
{

    public PlayerStats Stats;
    public GameObject ImagePrefab;
    private List<Image> _images = new List<Image>();
    private int _lastcount = -1;
    
    void UpdateList()
    {
        foreach (var i in _images)
        {
            Destroy(i.gameObject);
        }

        _images.Clear();

        foreach (var img in Stats.QuestItems)
        {
            var control = Instantiate(ImagePrefab).GetComponent<Image>();
            control.sprite = img.Icon;
            control.transform.SetParent(transform);
            _images.Add(control);
        }
    }


	
	void Update ()
    {

        if (Stats.QuestItems != null && _lastcount != Stats.QuestItems.Count)
        {
            UpdateList();
            _lastcount = Stats.QuestItems.Count;
        }
    }
}
