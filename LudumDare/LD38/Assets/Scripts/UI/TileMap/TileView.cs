using Assets.Systems.CommandManager;
using UnityEngine;

public class TileView : MonoBehaviour
{
    public int X;
    public int Y;
    public TileMapView MapView;
    public GameObject HighlightObject;
    public Sprite TileImage;
    public Sprite DarkImage;

    private Animator _animator;
    private SpriteRenderer _render; 

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
    }

    public void Destruct()
    {
        _animator.SetTrigger("KillTile");
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    public void Highlight(bool active)
    {
        HighlightObject.SetActive(active);
    }

    public void SetVisability(bool visable)
    {
        _render.sprite = visable ? TileImage : DarkImage;
    }
}
