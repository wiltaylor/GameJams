using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class FactoryController : MonoBehaviour {

    Dictionary<string, List<GameObject>> _factData = new Dictionary<string, List<GameObject>>();
    Dictionary<string, GameObject> _prefabList = new Dictionary<string, GameObject>();

    public void Reset()
    {
        try
        {
            foreach(var t in _factData.Keys)
            {
                _factData[t].Clear();
            }
        }
        catch { /* Skip errors */ }
    }

    public void RegisterType (string name, GameObject Prefab)
    {
        if (_factData.Keys.Contains(name))
            return;
        
        _factData.Add(name, new List<GameObject>());
        _prefabList.Add(name, Prefab);
    }

    public GameObject GetObject(string name)
    {
        if (!_factData.Keys.Contains(name))
        {
            throw new ApplicationException("No factory registered with this name");
        }

        foreach(var obj in _factData[name])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        var newobj = Instantiate<GameObject>(_prefabList[name]);
        newobj.SetActive(false);
        _factData[name].Add(newobj);

        return newobj;

    }
}
