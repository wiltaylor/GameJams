using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemRenderer : MonoBehaviour
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

        foreach (var wpn in Stats.Weapons)
        {
            var control = Instantiate(ImagePrefab).GetComponent<Image>();
            control.sprite = wpn.Item;
            control.transform.SetParent(transform);
            _images.Add(control);
        }
    }

    void Update ()
    {
        if (Stats.Weapons != null && _lastcount != Stats.Weapons.Count)
        {
            UpdateList();
            _lastcount = Stats.Weapons.Count;
        }

        for (int i = 0; i < Stats.Weapons.Count; i++)
        {
            if (Stats.Weapons[i] == Stats.CurrentWeapon)
            {
                _images[i].color = Color.white;
            }
            else
            {
                _images[i].color = Color.grey;
            }
        }
    }
}
