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
        get { return false; }
    }

    public bool SupportEnamyUnit
    {
        get { return false; }
    }

    public void ClickUnit(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public void ClickBlock(GameObject block)
    {
        if (GameController.Instance.MouseOverUI)
            return;

        var blk = block.GetComponent<BlockController>();

        if (blk.Indestructable) return;

        if (GameController.Instance.DigLeft <= 0 && !blk.Targeted)
            return;

        if (blk.Targeted)
            GameController.Instance.DigLeft++;
        else
            GameController.Instance.DigLeft--;

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
