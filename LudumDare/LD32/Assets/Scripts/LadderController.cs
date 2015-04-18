using UnityEngine;
using System.Collections;

public class LadderController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Unit")
        {
            coll.gameObject.SendMessage("EnterLadder");
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Unit")
        {
            coll.gameObject.SendMessage("ExitLadder");
        }
    }

}
