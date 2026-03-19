using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWindow : MonoBehaviour
{

    public GameObject Prefab;
    private GameObject _instance;

    void OnEnable()
    {
        if (_instance != null)
            return;

        _instance = Instantiate(Prefab);
        _instance.SetActive(true);
        _instance.transform.SetParent(transform);
        _instance.transform.position = transform.position;
    }

    void OnDisable()
    {
        Destroy(_instance);
    }

    void Update()
    {
        if (_instance != null && !_instance.activeInHierarchy)
        {
            Destroy(_instance);
            gameObject.SetActive(false);
        }
    }
}
