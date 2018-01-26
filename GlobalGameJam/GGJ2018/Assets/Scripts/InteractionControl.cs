using UnityEngine;

public class InteractionControl : MonoBehaviour
{
    private GameObject _context;


    public void Update()
    {
        if (Input.GetButton("Use"))
        {
            if(_context != null)
                _context.SendMessage("OnUse");
        }
    }

    public void OnContext(GameObject obj)
    {
        _context = obj;
    }

    public void ClearContext(GameObject obj)
    {
        if (_context == obj)
            _context = null;
    }

}
