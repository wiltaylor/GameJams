using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Activation;

public class CreateLadderCommand : IGameClickHandler
{

    private readonly GameObject _ladderPrefab;

    public CreateLadderCommand(GameObject prefab)
    {
        _ladderPrefab = prefab;
    }

    public string Name
    {
        get { return "Create Ladder"; }
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
        get { return true; }
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
        Object.Instantiate(_ladderPrefab, point, Quaternion.identity);
    }
}
