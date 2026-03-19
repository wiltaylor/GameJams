using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public string Name;

    public void LoadLevel()
    {
        SceneManager.LoadScene(Name);
    }

}
