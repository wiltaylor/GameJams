using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{

    public float BombTime = 0.5f;

    private float BombCount = -1f;

    void FixedUpdate()
    {
        if (BombCount < 0f)
        {
            BombCount = BombTime;
            gameObject.SetActive(false);
        }
        else
        {
            BombCount -= Time.fixedDeltaTime;
        }
    }
	
    void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Colided with " + collider.name);
        collider.gameObject.SendMessage("OnBomb");
    }

    
}
