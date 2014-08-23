using UnityEngine;
using System.Collections;

public class SlowZone : MonoBehaviour
{
    public float Drag = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        var car = other.GetComponent<CarController>();

        if (car == null)
            return;

        var body = other.GetComponent<Rigidbody2D>();

        body.drag += Drag;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var car = other.GetComponent<CarController>();

        if (car == null)
            return;

        var body = other.GetComponent<Rigidbody2D>();

        body.drag -= Drag;
    }
}
