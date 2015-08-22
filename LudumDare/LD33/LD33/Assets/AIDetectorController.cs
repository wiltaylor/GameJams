using UnityEngine;
using System.Collections;

public class AIDetectorController : MonoBehaviour
{

    public GameObject Target;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Target)
            return;

        Target.SendMessage("AITriggered", col.gameObject);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == Target)
            return;

        Target.SendMessage("AITargetLost", col.gameObject);
    }
}
