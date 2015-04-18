using UnityEngine;
using System.Collections;

public class CreateBlockerCommand : IGameClickHandler
{

    public string Name
    {
        get { return "Create Blocker"; }
    }

    public bool SupportBlocks
    {
        get { return false; }
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
        unit.Walking = false;
        obj.layer = 0;
        obj.tag = "Unit_Turn";
        obj.name = "Unit_Blocker";
    }

    public void ClickBlock(GameObject block)
    {
        throw new System.NotImplementedException();
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
