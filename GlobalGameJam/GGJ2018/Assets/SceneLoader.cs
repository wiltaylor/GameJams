using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    // The index of the scene to load. Check that the scene that you want to load is in the Build Settings.
    [Tooltip("The index of the scene to load. Check that the scene that you want to load is in the Build Settings.")]
    public int SceneIndexToLoad = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Load the next Scene.
        if (other.gameObject.tag == "Player") SceneManager.LoadScene(SceneIndexToLoad);
    }
}
