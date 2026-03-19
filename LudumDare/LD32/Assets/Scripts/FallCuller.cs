using UnityEngine;
using System.Collections;

public class FallCuller : MonoBehaviour {

    public enum ObjectType
    {
        Block,
        Unit,
        Other
    }

    public ObjectType Type =ObjectType.Other;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }

    void FixedUpdate()
    {
        if (!(transform.position.y < _gameController.YCutOff)) return;

        if(Type == ObjectType.Other)
            Destroy(gameObject);

        if(Type == ObjectType.Unit)
            gameObject.SendMessage("Kill");

        if (Type != ObjectType.Block) return;

        var blk = GetComponent<BlockController>();
        blk.Health = 0;
    }
}
