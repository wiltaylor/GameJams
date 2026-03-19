using UnityEngine;

public class NamedSingleton : MonoBehaviour
{

    public string Name;

    void Awake()
    {
        foreach (var obj in FindObjectsOfType<NamedSingleton>())
        {
            if(obj == this)
                continue;

            if (obj.Name == this.Name)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
    }

}
