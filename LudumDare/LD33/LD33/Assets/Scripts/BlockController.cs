using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour
{
    public Sprite RegularBlock;
    public Sprite MossBlock;
    public bool Moss;
    public bool Blocking;

    private bool _moss;
    private SpriteRenderer _renderer;

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        SetBlockMoss(Moss);
    }

    public void SetBlockMoss(bool active)
    {
        _renderer.sprite = active ? MossBlock : RegularBlock;
    }
}
