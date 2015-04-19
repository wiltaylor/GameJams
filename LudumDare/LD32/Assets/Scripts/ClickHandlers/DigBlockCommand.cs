using UnityEngine;
using System.Collections;

public class DigBlockCommand : IGameClickHandler {


    public string Name
    {
        get { return "Dig"; }
    }

    public bool SupportBlocks
    {
        get { return true; }
    }

    public bool SupportFriendlyUnit
    {
        get { return true; }
    }

    public bool SupportEnamyUnit
    {
        get { return false; }
    }

    public void ClickUnit(GameObject obj)
    {
        var unit = obj.GetComponent<UnitController>();
        unit.HasCuttingTools = true;
    }

    public void ClickBlock(GameObject block)
    {
        var blk = block.GetComponent<BlockController>();
        
        if(!blk.Indestructable)
            blk.Targeted = !blk.Targeted;
    }


    public bool SupportWorld
    {
        get { return false; }
    }

    public void ClickWorld(Vector3 point)
    {
        throw new System.NotImplementedException();
    }
}
