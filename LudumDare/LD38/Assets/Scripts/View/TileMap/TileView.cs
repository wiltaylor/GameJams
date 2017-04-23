using Assets.Systems.CommandManager;
using UnityEngine;

public class TileView : MonoBehaviour
{
    public int X;
    public int Y;
    public TileMapView MapView;

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
}
