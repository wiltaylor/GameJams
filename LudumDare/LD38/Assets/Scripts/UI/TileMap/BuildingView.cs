using UnityEngine;

public class BuildingView : MonoBehaviour
{

    public int X;
    public int Y;
    public TileMapView MapView;
    public Sprite Unclaimed;
    public Sprite Claimed;

    private Animator _animator;
    private SpriteRenderer _render;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
    }

    public void SetOwnership(bool player)
    {
        if(_render == null)
            _render = GetComponent<SpriteRenderer>();

        _render.sprite = player ? Claimed : Unclaimed;
    }

    public void Destruct()
    {
        _animator.SetTrigger("KillTile");
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
