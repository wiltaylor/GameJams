using UnityEngine;

public interface IGameClickHandler
{
    string Name { get; }
    bool SupportBlocks { get; }
    bool SupportFriendlyUnit { get; }
    bool SupportEnamyUnit { get; }
    bool SupportWorld { get; }

    void ClickUnit(GameObject obj);
    void ClickBlock(GameObject block);
    void ClickWorld(Vector3 point);
}