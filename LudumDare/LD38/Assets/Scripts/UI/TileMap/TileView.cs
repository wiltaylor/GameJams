using Assets.Systems.CommandManager;
using UnityEngine;

public class TileView : MonoBehaviour
{
    public int X;
    public int Y;
    public TileMapView MapView;
    public GameObject HighlightObject;

    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
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
}
