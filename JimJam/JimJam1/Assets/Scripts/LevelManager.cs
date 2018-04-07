using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerController _player;
    private List<JumpGateController> _gates;

    void Start()
    {
        _player = PlayerController.Instance;
        
        _gates = new List<JumpGateController>(FindObjectsOfType<JumpGateController>());

        var playerGate = _gates.FirstOrDefault(g => string.Equals(g.TargetScene, _player.LastScene, StringComparison.CurrentCultureIgnoreCase));

        if (playerGate == null) return;

        _player.transform.position = playerGate.JumpPoint.position;
        _player.transform.rotation = playerGate.JumpPoint.rotation;
        _player.Brake();
    }

}
