using UnityEngine;
using System.Collections;

public class BlockDestroyer : MonoBehaviour
{

    public float TimeToDestruction = 0f;

    private BlockController _blockController;
    void Start()
    {
        _blockController = GetComponent<BlockController>();
    }

    void SwitchTriggered()
    {
        _blockController.Health = 0;
    }
}
