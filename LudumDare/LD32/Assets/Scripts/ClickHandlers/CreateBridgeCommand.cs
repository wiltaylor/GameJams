using UnityEngine;
using System.Collections;

public class CreateBridgeCommand : IGameClickHandler
{

    private readonly GameObject _bridgePrefab;

    public CreateBridgeCommand(GameObject prefab)
    {
        _bridgePrefab = prefab;
    }

    public string Name
    {
        get { return "Create Birdge"; }
    }

    public bool SupportBlocks
    {
        get { return false; }
    }

    public bool SupportFriendlyUnit
    {
        get { return false; }
    }

    public bool SupportEnamyUnit
    {
        get { return false; }
    }

    public bool SupportWorld
    {
        get {return true; }
    }

    public void ClickUnit(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public void ClickBlock(GameObject block)
    {
        throw new System.NotImplementedException();
    }

    public void ClickWorld(Vector3 point)
    {
        Object.Instantiate(_bridgePrefab, point, Quaternion.identity);
    }
}
