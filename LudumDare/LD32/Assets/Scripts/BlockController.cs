using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public bool Indestructable = false;
    public bool Targeted = false;
    public float Health = 10;
    public GameObject DirtCloud;
    public bool DestroyOnFinish = true;
    public Sprite BlockBackground;
    public bool LeaveBackground = true;

    private GameObject _selectedGraphic;

    void Start()
    {
        if (transform.childCount == 0)
            return;

        _selectedGraphic = transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        if(Targeted && _selectedGraphic != null)
            _selectedGraphic.SetActive(true);

        if (Health <= 0f)
        {
            if(DirtCloud != null) 
                Instantiate(DirtCloud, transform.position, transform.rotation);

            if (DestroyOnFinish)
            {
                if (BlockBackground != null && LeaveBackground)
                {
                    var obj = (GameObject) Instantiate(new GameObject(), transform.position, transform.rotation);
                    var render = obj.AddComponent<SpriteRenderer>();
                    render.sprite = BlockBackground;
                    render.sortingLayerName = "Background";
                    render.sortingOrder = -1000;
                        
                    obj.name = "background_block";
                    obj.SetActive(true);
                }

                Destroy(gameObject);
            }
            else
            {
                gameObject.SendMessage("BuildItem");
            }
        }
    }
    
    void OnMouseDown()
    {
        var gc = GameController.Instance;

        if (gc.CurrentClickHandler == null)
            return;

        if (gc.CurrentClickHandler.SupportBlocks)
            gc.CurrentClickHandler.ClickBlock(gameObject);

    }
}
