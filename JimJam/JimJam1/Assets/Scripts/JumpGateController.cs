using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpGateController : MonoBehaviour
{

    public string TargetScene;
    public Transform JumpPoint;
    public float Distance = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();

            if (player.isWarping)
                return;

            player.Warp(Distance);
            player.LastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadSceneAsync(TargetScene, LoadSceneMode.Single);

        }
    }
}
