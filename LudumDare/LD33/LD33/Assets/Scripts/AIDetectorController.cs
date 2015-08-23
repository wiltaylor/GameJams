using UnityEngine;
using System.Collections;

public class AIDetectorController : MonoBehaviour
{

    public GameObject Target;
    public float TriggerStayTimeout = 5f;
    public TargetTypes TargetType = TargetTypes.Unit;

    public enum TargetTypes
    {
        Hero,
        Unit
    }
    private float _timeout = 5f;

    public void FixedUpdate()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.fixedDeltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hero" && TargetType == TargetTypes.Unit)
            return;
        if (col.tag == "Unit" && TargetType == TargetTypes.Hero)
            return;

        if (col.gameObject == Target)
            return;

        Target.SendMessage("AITriggered", col.gameObject);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Hero" && TargetType == TargetTypes.Unit)
            return;
        if (col.tag == "Unit" && TargetType == TargetTypes.Hero)
            return;

        if (col.gameObject == Target)
            return;

        Target.SendMessage("AITargetLost", col.gameObject);
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Hero" && TargetType == TargetTypes.Unit)
            return;
        if (col.tag == "Unit" && TargetType == TargetTypes.Hero)
            return;

        if (!(_timeout <= 0f)) return;

        Target.SendMessage("AITargetUpdate", col.gameObject);
        _timeout = TriggerStayTimeout;
    }


}
